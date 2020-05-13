using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UniPatcher
{
	public class Form3 : Form
	{
		private IContainer components;

		private TextBox textBox1;

		private Button button1;

		public Form3()
		{
			this.InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			this.textBox1 = new TextBox();
			this.button1 = new Button();
			base.SuspendLayout();
			this.textBox1.BackColor = SystemColors.Control;
			this.textBox1.BorderStyle = BorderStyle.None;
			this.textBox1.Location = new Point(12, 13);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(281, 64);
			this.textBox1.TabIndex = 0;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "This patcher requires elevated priveleges.\r\n\r\nRetry... Run as administrator";
			this.textBox1.TextAlign = HorizontalAlignment.Center;
			this.button1.DialogResult = DialogResult.OK;
			this.button1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.button1.Location = new Point(113, 83);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.TabStop = false;
			this.button1.Text = "OKAY";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.ClientSize = new Size(305, 120);
			base.ControlBox = false;
			base.Controls.Add(this.button1);
			base.Controls.Add(this.textBox1);
			this.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.ForeColor = SystemColors.WindowText;
			base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form3";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Alert";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
