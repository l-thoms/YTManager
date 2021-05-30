using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace YTManager
{
	public static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Body body = new Body(args);
			Application.Run();
		}
	}
}
