using System;
using System.Runtime.InteropServices;

namespace Demo.Interfaces
{
	[ComVisible(true)]
	[Guid("3C8CCB50-20C6-4C78-9CFF-DEF8786FCB28")]
	public interface IHelloWorld
	{
		string Echo(string message);
		int Counter { get; }
	}
}
