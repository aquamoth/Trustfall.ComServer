using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Trustfall.ComServer
{
	public class ClassFactory<T, I> : IClassFactory
		where T : new()
	{
		public int CreateInstance(IntPtr pUnkOuter, ref Guid riid, out IntPtr ppvObject)
		{
			Trace.TraceInformation("ClassFactory asked to create instance of {0:B}", riid);
			ppvObject = IntPtr.Zero;

			if (pUnkOuter != IntPtr.Zero)
			{
				// The pUnkOuter parameter was non-NULL and the object does 
				// not support aggregation.
				Marshal.ThrowExceptionForHR(COMNative.CLASS_E_NOAGGREGATION);
			}

			var clsGuid = Marshal.GenerateGuidForType(typeof(I));
			if (riid == clsGuid ||
				riid == new Guid(COMNative.IID_IDispatch) ||
				riid == new Guid(COMNative.IID_IUnknown))
			{
				// Create the instance of the .NET object
				ppvObject = Marshal.GetComInterfaceForObject(new T(), typeof(I));
			}
			else
			{
				var searchedGuid = riid;
				var type = typeof(T).GetInterfaces().FirstOrDefault(i => Marshal.GenerateGuidForType(i) == searchedGuid);
				if (type != null)
				{
					// Create the instance of the .NET object
					ppvObject = Marshal.GetComInterfaceForObject(new T(), type);
				}
				else
				{
					// The object that ppvObject points to does not support the 
					// interface identified by riid.
					Marshal.ThrowExceptionForHR(COMNative.E_NOINTERFACE);
				}
			}

			return 0;   // S_OK
		}

		public int LockServer(bool fLock)
		{
			Trace.TraceInformation("ClassFactory asked to {0}", fLock ? "LOCK" : "UNLOCK");
			return 0;   // S_OK
		}
	}
}
