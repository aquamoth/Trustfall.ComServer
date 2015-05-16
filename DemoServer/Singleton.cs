using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Server
{
	internal class Singleton
	{
		int _counter = 0;
		public int Counter 
		{ 
			get
			{
				return _counter;
			}
			set
			{
				lock (_lockObject)
					_counter = value;
			}
		}


		private Singleton() { }
		
		static object _lockObject = new object();
		
		static Singleton _default;
		public static Singleton Default
		{
			get
			{
				if (_default == null)
				lock (_lockObject)
				if (_default == null)
					_default = new Singleton();
				return _default;
			}
		}
	}
}
