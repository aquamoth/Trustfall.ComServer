using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Trustfall.ComServer
{
	sealed public class ComServer
	{
		#region Singleton Pattern

		private ComServer() { }

		private static ComServer _instance = new ComServer();
		public static ComServer Instance
		{
			get { return _instance; }
		}

		#endregion

		private List<uint> _registeredComObjectCookies = new List<uint>();
		private int _lockCounter = 0;
		private Timer _gcTimer = null;

		public event EventHandler AllObjectsUnlocked;
		private void OnAllObjectsUnlocked(EventArgs e)
		{
			if (AllObjectsUnlocked != null)
				AllObjectsUnlocked(this, e);
		}

		public void Register<T>(IClassFactory classFactory, CLSCTX context) 
			where T : new()
		{
			//Verify that the user hasn't already called Run()
			if (_gcTimer != null)
				throw new ApplicationException("The COM server is already running!");

			// Register the class object
			uint objectCookie;
			var clsid = Marshal.GenerateGuidForType(typeof(T));
			int hResult = COMNative.CoRegisterClassObject(
				ref clsid,                          // CLSID to be registered
				classFactory,                       // Class factory
				context,                            // Context to run
				REGCLS.MULTIPLEUSE | REGCLS.SUSPENDED,
				out objectCookie);

			if (hResult != 0)
			{
				throw new ApplicationException(
					"CoRegisterClassObject failed w/err 0x" + hResult.ToString("X"));
			}

			_registeredComObjectCookies.Add(objectCookie);
		}

		public void Run()
		{
			if (!_registeredComObjectCookies.Any())
				throw new ApplicationException("No COM objects have yet been registered!");
			if (_gcTimer != null)
				throw new ApplicationException("The COM server is already running!");

			// Inform the SCM about all the registered classes, and begins 
			// letting activation requests into the server process.
			var hResult = COMNative.CoResumeClassObjects();
			if (hResult != 0)
			{
				unregisterClasses(); // Revoke the registration of registered classes on failure
				throw new ApplicationException(
					"CoResumeClassObjects failed w/err 0x" + hResult.ToString("X"));
			}

			//
			// Initialize member variables.
			// 

			//// Records the count of the active COM objects in the server. 
			//// When _nLockCnt drops to zero, the server can be shut down.
			//_lockCounter = 0;

			// TODO: Start the GC timer to trigger GC every 5 seconds.
			_gcTimer = new Timer(new TimerCallback((obj) => 
			{
				Trace.TraceInformation("Garbage Collecting");
				GC.Collect(); 
			}), null, 5000, 5000);
		}

		public void Shutdown()
		{
			unregisterClasses();

			//
			// Perform the cleanup.
			// 

			// TODO: Dispose the GC timer.
			if (_gcTimer != null)
			{
				_gcTimer.Dispose();
				_gcTimer = null;
			}

			// Wait for any threads to finish.
			Thread.Sleep(1000);
		}

		/// <summary>
		/// Increase the lock count
		/// </summary>
		/// <returns>The new lock count after the increment</returns>
		/// <remarks>The method is thread-safe.</remarks>
		public int Lock()
		{
			return Interlocked.Increment(ref _lockCounter);
		}

		/// <summary>
		/// Decrease the lock count. When the lock count drops to zero, post 
		/// the WM_QUIT message to the message loop in the main thread to 
		/// shut down the COM server.
		/// </summary>
		/// <returns>The new lock count after the increment</returns>
		public int Unlock()
		{
			int nRet = Interlocked.Decrement(ref _lockCounter);

			// Inform when the lock drops to zero, so the server can be terminated if this is an EXE server.
			if (nRet == 0)
			{
				OnAllObjectsUnlocked(EventArgs.Empty);
			}

			return nRet;
		}

		/// <summary>
		/// Unregisters all COM classes that were previously registered by the Register() method
		/// </summary>
		void unregisterClasses()
		{
			foreach (var cookie in _registeredComObjectCookies)
			{
				try
				{
					COMNative.CoRevokeClassObject(cookie);
				}
				catch (Exception ex)
				{
					//TODO: This should never happen and mustn't stop unregistration of other classes
					System.Diagnostics.Trace.TraceError(ex.ToString());
				}
			}
			_registeredComObjectCookies.Clear();
		}
	}
}
