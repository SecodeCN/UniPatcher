using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace UniPatcher
{
	public class Form2 : Form
	{
		public LicHeader.LicSettings LicSettings = new LicHeader.LicSettings();

		private IContainer components;

		private Button button1;

		private Button button2;

		private Label label1;

		private ComboBox type;

		private Label label2;

		private CheckBox Team;

		private CheckBox Wii;

		private CheckBox Xbox;

		private CheckBox PlayStation;

		private CheckBox psm;

		private CheckBox nRelease;

		private CheckBox educt;

		private ComboBox iPhone;

		private Label label3;

		private Label label4;

		private ComboBox Android;

		private Label label5;

		private ComboBox Blackberry;

		private Label label6;

		private ComboBox Flash;

		private Label label7;

		private ComboBox WinStore;

		private Label label8;

		private ComboBox Tizen;

		private Label label9;

		private ComboBox SamsungTV;

		private CheckBox Nintendo;

		public Form2()
		{
			this.InitializeComponent();
		}

		private void Form2_Load(object sender, EventArgs e)
		{
			this.LicSettings = LicHeader.PropLicSettings;
			this.type.SelectedIndex = this.LicSettings.Type;
			this.Android.SelectedIndex = this.LicSettings.Android;
			this.Blackberry.SelectedIndex = this.LicSettings.Blackberry;
			this.Flash.SelectedIndex = this.LicSettings.Flash;
			this.iPhone.SelectedIndex = this.LicSettings.IPhone;
			this.PlayStation.Checked = this.LicSettings.PlayStation;
			this.Wii.Checked = this.LicSettings.Wii;
			this.WinStore.SelectedIndex = this.LicSettings.WinStore;
			this.Xbox.Checked = this.LicSettings.Xbox;
			this.Team.Checked = this.LicSettings.Team;
			this.Tizen.SelectedIndex = this.LicSettings.Tizen;
			this.SamsungTV.SelectedIndex = this.LicSettings.SamsungTv;
			this.educt.Checked = this.LicSettings.Educt;
			this.nRelease.Checked = this.LicSettings.NRelease;
			this.Nintendo.Checked = this.LicSettings.Nin;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.LicSettings.Type = this.type.SelectedIndex;
			this.LicSettings.Android = this.Android.SelectedIndex;
			this.LicSettings.Blackberry = this.Blackberry.SelectedIndex;
			this.LicSettings.Flash = this.Flash.SelectedIndex;
			this.LicSettings.IPhone = this.iPhone.SelectedIndex;
			this.LicSettings.PlayStation = this.PlayStation.Checked;
			this.LicSettings.Wii = this.Wii.Checked;
			this.LicSettings.WinStore = this.WinStore.SelectedIndex;
			this.LicSettings.Xbox = this.Xbox.Checked;
			this.LicSettings.Team = this.Team.Checked;
			this.LicSettings.Tizen = this.Tizen.SelectedIndex;
			this.LicSettings.SamsungTv = this.SamsungTV.SelectedIndex;
			this.LicSettings.Educt = this.educt.Checked;
			this.LicSettings.NRelease = this.nRelease.Checked;
			this.LicSettings.Nin = this.Nintendo.Checked;
			LicHeader.PropLicSettings = this.LicSettings;
			base.Close();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			base.Close();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form2));
			this.button1 = new Button();
			this.button2 = new Button();
			this.label1 = new Label();
			this.type = new ComboBox();
			this.label2 = new Label();
			this.Team = new CheckBox();
			this.Wii = new CheckBox();
			this.Xbox = new CheckBox();
			this.PlayStation = new CheckBox();
			this.psm = new CheckBox();
			this.nRelease = new CheckBox();
			this.educt = new CheckBox();
			this.iPhone = new ComboBox();
			this.label3 = new Label();
			this.label4 = new Label();
			this.Android = new ComboBox();
			this.label5 = new Label();
			this.Blackberry = new ComboBox();
			this.label6 = new Label();
			this.Flash = new ComboBox();
			this.label7 = new Label();
			this.WinStore = new ComboBox();
			this.label8 = new Label();
			this.Tizen = new ComboBox();
			this.label9 = new Label();
			this.SamsungTV = new ComboBox();
			this.Nintendo = new CheckBox();
			base.SuspendLayout();
			this.button1.DialogResult = DialogResult.OK;
			this.button1.Location = new Point(379, 398);
			this.button1.Name = "button1";
			this.button1.Size = new Size(75, 26);
			this.button1.TabIndex = 0;
			this.button1.TabStop = false;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.DialogResult = DialogResult.Cancel;
			this.button2.Location = new Point(298, 398);
			this.button2.Name = "button2";
			this.button2.Size = new Size(75, 26);
			this.button2.TabIndex = 1;
			this.button2.TabStop = false;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(45, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Type:";
			this.type.FormattingEnabled = true;
			this.type.Items.AddRange(new object[]
			{
				"Unity for Embedded Systems",
				"Unity Pro",
				"Unity",
				"Unity Indie"
			});
			this.type.Location = new Point(76, 9);
			this.type.Name = "type";
			this.type.Size = new Size(378, 26);
			this.type.TabIndex = 3;
			this.type.TabStop = false;
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 47);
			this.label2.Name = "label2";
			this.label2.Size = new Size(74, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Features:";
			this.Team.AutoSize = true;
			this.Team.Location = new Point(16, 80);
			this.Team.Name = "Team";
			this.Team.Size = new Size(65, 22);
			this.Team.TabIndex = 5;
			this.Team.TabStop = false;
			this.Team.Text = "Team";
			this.Team.UseVisualStyleBackColor = true;
			this.Wii.AutoSize = true;
			this.Wii.Location = new Point(95, 80);
			this.Wii.Name = "Wii";
			this.Wii.Size = new Size(50, 22);
			this.Wii.TabIndex = 6;
			this.Wii.TabStop = false;
			this.Wii.Text = "Wii";
			this.Wii.UseVisualStyleBackColor = true;
			this.Xbox.AutoSize = true;
			this.Xbox.Location = new Point(158, 80);
			this.Xbox.Name = "Xbox";
			this.Xbox.Size = new Size(63, 22);
			this.Xbox.TabIndex = 7;
			this.Xbox.TabStop = false;
			this.Xbox.Text = "Xbox";
			this.Xbox.UseVisualStyleBackColor = true;
			this.PlayStation.AutoSize = true;
			this.PlayStation.Location = new Point(268, 80);
			this.PlayStation.Name = "PlayStation";
			this.PlayStation.Size = new Size(106, 22);
			this.PlayStation.TabIndex = 8;
			this.PlayStation.TabStop = false;
			this.PlayStation.Text = "PlayStation";
			this.PlayStation.UseVisualStyleBackColor = true;
			this.psm.AutoSize = true;
			this.psm.Location = new Point(268, 136);
			this.psm.Name = "psm";
			this.psm.Size = new Size(81, 22);
			this.psm.TabIndex = 9;
			this.psm.TabStop = false;
			this.psm.Text = "PS Vita";
			this.psm.UseVisualStyleBackColor = true;
			this.psm.Visible = false;
			this.nRelease.AutoSize = true;
			this.nRelease.Location = new Point(16, 108);
			this.nRelease.Name = "nRelease";
			this.nRelease.Size = new Size(129, 22);
			this.nRelease.TabIndex = 10;
			this.nRelease.TabStop = false;
			this.nRelease.Text = "Not for release";
			this.nRelease.UseVisualStyleBackColor = true;
			this.educt.AutoSize = true;
			this.educt.Location = new Point(16, 136);
			this.educt.Name = "educt";
			this.educt.Size = new Size(195, 22);
			this.educt.TabIndex = 11;
			this.educt.TabStop = false;
			this.educt.Text = "For educational use only";
			this.educt.UseVisualStyleBackColor = true;
			this.iPhone.FormattingEnabled = true;
			this.iPhone.Items.AddRange(new object[]
			{
				"iPhone Pro",
				"iPhone",
				"None"
			});
			this.iPhone.Location = new Point(116, 169);
			this.iPhone.Name = "iPhone";
			this.iPhone.Size = new Size(338, 26);
			this.iPhone.TabIndex = 12;
			this.iPhone.TabStop = false;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(13, 172);
			this.label3.Name = "label3";
			this.label3.Size = new Size(61, 18);
			this.label3.TabIndex = 13;
			this.label3.Text = "iPhone:";
			this.label4.AutoSize = true;
			this.label4.Location = new Point(13, 204);
			this.label4.Name = "label4";
			this.label4.Size = new Size(67, 18);
			this.label4.TabIndex = 15;
			this.label4.Text = "Android:";
			this.Android.FormattingEnabled = true;
			this.Android.Items.AddRange(new object[]
			{
				"Android Pro",
				"Android",
				"None"
			});
			this.Android.Location = new Point(116, 201);
			this.Android.Name = "Android";
			this.Android.Size = new Size(338, 26);
			this.Android.TabIndex = 14;
			this.Android.TabStop = false;
			this.label5.AutoSize = true;
			this.label5.Location = new Point(13, 236);
			this.label5.Name = "label5";
			this.label5.Size = new Size(86, 18);
			this.label5.TabIndex = 17;
			this.label5.Text = "Blackberry:";
			this.Blackberry.FormattingEnabled = true;
			this.Blackberry.Items.AddRange(new object[]
			{
				"Blackberry Pro",
				"Blackberry",
				"None"
			});
			this.Blackberry.Location = new Point(116, 233);
			this.Blackberry.Name = "Blackberry";
			this.Blackberry.Size = new Size(338, 26);
			this.Blackberry.TabIndex = 16;
			this.Blackberry.TabStop = false;
			this.label6.AutoSize = true;
			this.label6.Location = new Point(13, 268);
			this.label6.Name = "label6";
			this.label6.Size = new Size(50, 18);
			this.label6.TabIndex = 19;
			this.label6.Text = "Flash:";
			this.Flash.FormattingEnabled = true;
			this.Flash.Items.AddRange(new object[]
			{
				"Flash Pro",
				"Flash",
				"None"
			});
			this.Flash.Location = new Point(116, 265);
			this.Flash.Name = "Flash";
			this.Flash.Size = new Size(338, 26);
			this.Flash.TabIndex = 18;
			this.Flash.TabStop = false;
			this.label7.AutoSize = true;
			this.label7.Location = new Point(13, 300);
			this.label7.Name = "label7";
			this.label7.Size = new Size(77, 18);
			this.label7.TabIndex = 21;
			this.label7.Text = "WinStore:";
			this.WinStore.FormattingEnabled = true;
			this.WinStore.Items.AddRange(new object[]
			{
				"WinStore Pro",
				"WinStore",
				"None"
			});
			this.WinStore.Location = new Point(116, 297);
			this.WinStore.Name = "WinStore";
			this.WinStore.Size = new Size(338, 26);
			this.WinStore.TabIndex = 20;
			this.WinStore.TabStop = false;
			this.label8.AutoSize = true;
			this.label8.Location = new Point(13, 332);
			this.label8.Name = "label8";
			this.label8.Size = new Size(48, 18);
			this.label8.TabIndex = 23;
			this.label8.Text = "Tizen:";
			this.Tizen.FormattingEnabled = true;
			this.Tizen.Items.AddRange(new object[]
			{
				"Tizen Pro",
				"Tizen",
				"None"
			});
			this.Tizen.Location = new Point(116, 329);
			this.Tizen.Name = "Tizen";
			this.Tizen.Size = new Size(338, 26);
			this.Tizen.TabIndex = 22;
			this.Tizen.TabStop = false;
			this.label9.AutoSize = true;
			this.label9.Location = new Point(13, 364);
			this.label9.Name = "label9";
			this.label9.Size = new Size(97, 18);
			this.label9.TabIndex = 25;
			this.label9.Text = "SamsungTV:";
			this.SamsungTV.FormattingEnabled = true;
			this.SamsungTV.Items.AddRange(new object[]
			{
				"SamsungTV Pro",
				"SamsungTV",
				"None"
			});
			this.SamsungTV.Location = new Point(116, 361);
			this.SamsungTV.Name = "SamsungTV";
			this.SamsungTV.Size = new Size(338, 26);
			this.SamsungTV.TabIndex = 24;
			this.SamsungTV.TabStop = false;
			this.Nintendo.AutoSize = true;
			this.Nintendo.Location = new Point(268, 108);
			this.Nintendo.Name = "Nintendo";
			this.Nintendo.Size = new Size(125, 22);
			this.Nintendo.TabIndex = 26;
			this.Nintendo.TabStop = false;
			this.Nintendo.Text = "Nintendo 3DS";
			this.Nintendo.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.InactiveCaption;
			base.ClientSize = new Size(466, 436);
			base.Controls.Add(this.Nintendo);
			base.Controls.Add(this.label9);
			base.Controls.Add(this.SamsungTV);
			base.Controls.Add(this.label8);
			base.Controls.Add(this.Tizen);
			base.Controls.Add(this.label7);
			base.Controls.Add(this.WinStore);
			base.Controls.Add(this.label6);
			base.Controls.Add(this.Flash);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.Blackberry);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.Android);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.iPhone);
			base.Controls.Add(this.educt);
			base.Controls.Add(this.nRelease);
			base.Controls.Add(this.psm);
			base.Controls.Add(this.PlayStation);
			base.Controls.Add(this.Xbox);
			base.Controls.Add(this.Wii);
			base.Controls.Add(this.Team);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.type);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			//base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form2";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterParent;
			this.Text = "License Options";
			base.TopMost = true;
			base.Load += new EventHandler(this.Form2_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
