using Demo.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoClient
{
	public partial class Form1 : Form
	{
		IHelloWorld helloWorld;

		public Form1()
		{
			InitializeComponent();
			helloWorld = (IHelloWorld)Activator.CreateInstance(Type.GetTypeFromProgID("Demo.Server.HelloWorld"));
		}

		private void buttonSend_Click(object sender, EventArgs e)
		{
			textBoxResponse.Text = helloWorld.Echo(textBoxMessage.Text);
			labelCounter.Text = string.Format("(Counter = {0})", helloWorld.Counter);
		}
	}
}
