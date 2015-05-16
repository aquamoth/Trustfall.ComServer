using System;
using System.Diagnostics;
using Trustfall.ComServer;

namespace Demo.Server
{
	static class Program
	{
		static void Main()
		{
			Trace.TraceInformation("Demo.Server starting.");
			registerClasses();

			ComServer.Instance.Run();

			RunMessageLoop();

			Trace.TraceInformation("Demo.Server stopping.");
			ComServer.Instance.Shutdown();
		}

		private static void registerClasses()
		{
			Trace.TraceInformation("Registering COM classes");

			var helloWorldFactory = new ClassFactory<HelloWorld, Demo.Interfaces.IHelloWorld>();
			ComServer.Instance.Register<HelloWorld>(
				helloWorldFactory, 
				CLSCTX.LOCAL_SERVER);

			//Register more ClassFactories here as needed

			Console.WriteLine("All COM classes are registered.");
		}

		private static void RunMessageLoop()
		{
			Trace.TraceInformation("Starting message loop");

			var _nMainThreadID = NativeMethod.GetCurrentThreadId();
			ComServer.Instance.AllObjectsUnlocked += (sender, e) =>
			{
				// Post the WM_QUIT message to the main thread
				NativeMethod.PostThreadMessage(_nMainThreadID, NativeMethod.WM_QUIT, UIntPtr.Zero, IntPtr.Zero);
			};

			/// <summary>
			/// RunMessageLoop runs the standard message loop. The message loop 
			/// quits when it receives the WM_QUIT message.
			/// </summary>
			MSG msg;
			while (NativeMethod.GetMessage(out msg, IntPtr.Zero, 0, 0))
			{
				NativeMethod.TranslateMessage(ref msg);
				NativeMethod.DispatchMessage(ref msg);
			}

			Trace.TraceInformation("Message loop ended");
		}
	}
}
