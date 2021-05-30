namespace YTManager
{
	partial class Form1
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.groupCapture = new System.Windows.Forms.GroupBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.checkK = new System.Windows.Forms.CheckBox();
			this.checkM = new System.Windows.Forms.CheckBox();
			this.buttonBrowse = new System.Windows.Forms.Button();
			this.textDirectory = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.buttonAbout = new System.Windows.Forms.Button();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonQuit = new System.Windows.Forms.Button();
			this.buttonExit = new System.Windows.Forms.Button();
			this.groupCapture.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupCapture
			// 
			this.groupCapture.Controls.Add(this.buttonStart);
			this.groupCapture.Controls.Add(this.checkK);
			this.groupCapture.Controls.Add(this.checkM);
			this.groupCapture.Controls.Add(this.buttonBrowse);
			this.groupCapture.Controls.Add(this.textDirectory);
			this.groupCapture.Controls.Add(this.label1);
			this.groupCapture.Location = new System.Drawing.Point(12, 12);
			this.groupCapture.Name = "groupCapture";
			this.groupCapture.Size = new System.Drawing.Size(250, 131);
			this.groupCapture.TabIndex = 0;
			this.groupCapture.TabStop = false;
			this.groupCapture.Text = "捕获";
			// 
			// buttonStart
			// 
			this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonStart.Location = new System.Drawing.Point(196, 100);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(48, 25);
			this.buttonStart.TabIndex = 5;
			this.buttonStart.Text = "开始";
			this.buttonStart.UseVisualStyleBackColor = true;
			// 
			// checkK
			// 
			this.checkK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkK.Location = new System.Drawing.Point(9, 80);
			this.checkK.Name = "checkK";
			this.checkK.Size = new System.Drawing.Size(187, 22);
			this.checkK.TabIndex = 4;
			this.checkK.Text = "捕获键盘";
			this.checkK.UseVisualStyleBackColor = true;
			// 
			// checkM
			// 
			this.checkM.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.checkM.Location = new System.Drawing.Point(9, 53);
			this.checkM.Name = "checkM";
			this.checkM.Size = new System.Drawing.Size(187, 22);
			this.checkM.TabIndex = 3;
			this.checkM.Text = "捕获鼠标";
			this.checkM.UseVisualStyleBackColor = true;
			// 
			// buttonBrowse
			// 
			this.buttonBrowse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonBrowse.Location = new System.Drawing.Point(196, 23);
			this.buttonBrowse.Name = "buttonBrowse";
			this.buttonBrowse.Size = new System.Drawing.Size(48, 25);
			this.buttonBrowse.TabIndex = 2;
			this.buttonBrowse.Text = "浏览";
			this.buttonBrowse.UseVisualStyleBackColor = true;
			// 
			// textDirectory
			// 
			this.textDirectory.BackColor = System.Drawing.SystemColors.Window;
			this.textDirectory.Location = new System.Drawing.Point(68, 24);
			this.textDirectory.Name = "textDirectory";
			this.textDirectory.ReadOnly = true;
			this.textDirectory.Size = new System.Drawing.Size(128, 23);
			this.textDirectory.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "工作路径";
			// 
			// buttonAbout
			// 
			this.buttonAbout.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonAbout.Location = new System.Drawing.Point(268, 117);
			this.buttonAbout.Name = "buttonAbout";
			this.buttonAbout.Size = new System.Drawing.Size(75, 25);
			this.buttonAbout.TabIndex = 9;
			this.buttonAbout.Text = "About";
			this.buttonAbout.UseVisualStyleBackColor = true;
			// 
			// buttonClose
			// 
			this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonClose.Location = new System.Drawing.Point(268, 12);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.Size = new System.Drawing.Size(75, 25);
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "关闭";
			this.buttonClose.UseVisualStyleBackColor = true;
			// 
			// buttonQuit
			// 
			this.buttonQuit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonQuit.Location = new System.Drawing.Point(268, 47);
			this.buttonQuit.Name = "buttonQuit";
			this.buttonQuit.Size = new System.Drawing.Size(75, 25);
			this.buttonQuit.TabIndex = 7;
			this.buttonQuit.Text = "退出";
			this.buttonQuit.UseVisualStyleBackColor = true;
			// 
			// buttonExit
			// 
			this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.buttonExit.Location = new System.Drawing.Point(268, 82);
			this.buttonExit.Name = "buttonExit";
			this.buttonExit.Size = new System.Drawing.Size(75, 25);
			this.buttonExit.TabIndex = 8;
			this.buttonExit.Text = "强制退出";
			this.buttonExit.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(350, 155);
			this.Controls.Add(this.buttonExit);
			this.Controls.Add(this.buttonQuit);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.buttonAbout);
			this.Controls.Add(this.groupCapture);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "Form1";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "YTManager";
			this.groupCapture.ResumeLayout(false);
			this.groupCapture.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupCapture;
		public System.Windows.Forms.Label label1;
		public System.Windows.Forms.TextBox textDirectory;
		public System.Windows.Forms.Button buttonBrowse;
		public System.Windows.Forms.Button buttonStart;
		public System.Windows.Forms.CheckBox checkK;
		public System.Windows.Forms.CheckBox checkM;
		public System.Windows.Forms.Button buttonAbout;
		public System.Windows.Forms.Button buttonClose;
		public System.Windows.Forms.Button buttonQuit;
		public System.Windows.Forms.Button buttonExit;
	}
}

