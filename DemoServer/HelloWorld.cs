using Demo.Interfaces;
using System;
using System.Runtime.InteropServices;

namespace Demo.Server
{
	[ComVisible(true)]
	[Guid("0A6B3861-F892-440C-9692-9F8734A49739")]
	[ComSourceInterfaces(typeof(IHelloWorld))]
	[ClassInterface(ClassInterfaceType.None)]
	public class HelloWorld : Trustfall.ComServer.SelfRegisteringObject, IHelloWorld
	{
		public string Echo(string message)
		{
			var assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			Singleton.Default.Counter++;
			return string.Format("Hello World {0}: {1}", assemblyVersion, (message ?? "(no message)"));
		}

		public int Counter { get { return Singleton.Default.Counter; } }
	}
}
