using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Trustfall.ComServer
{
	/// <summary>
	/// Reference counted object base.
	/// </summary>
	[ComVisible(false)]
	public class ReferenceCountedObject
	{
		public ReferenceCountedObject()
		{
			// Increment the lock count of objects in the COM server.
			ComServer.Instance.Lock();
		}

		~ReferenceCountedObject()
		{
			// Decrement the lock count of objects in the COM server.
			ComServer.Instance.Unlock();
		}
	}
}
