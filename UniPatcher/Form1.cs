using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace UniPatcher
{
	public class Form1 : Form
	{
		private string appPath = "C:";

		private int patchAs;

		private IContainer components;

		private Label label1;

		private TextBox textBox1;

		private Label label2;

		private TextBox textBox2;

		private Label label3;

		private TextBox textBox3;

		private TextBox textBox4;

		private TextBox textBox5;

		private TextBox textBox6;

		private TextBox textBox7;

		private TextBox textBox8;

		private Button button1;

		private Button button2;

		private Button button3;

		private Button button4;

		private Button button5;

		private TextBox textBox9;

		public Form1()
		{
			this.InitializeComponent();
			this.button2.Enabled = false;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog
			{
				RootFolder = Environment.SpecialFolder.Desktop,
				Description = "Please select the folder where Unity.exe is...",
				ShowNewFolderButton = false
			};
			if (Directory.Exists(this.textBox1.Text))
			{
				folderBrowserDialog.SelectedPath = this.textBox1.Text;
			}
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				this.textBox1.Text = folderBrowserDialog.SelectedPath;
				try
				{
					Directory.SetCurrentDirectory(folderBrowserDialog.SelectedPath);
					if (!File.Exists(folderBrowserDialog.SelectedPath + "/Unity.exe"))
					{
						throw new IOException("Application not found!");
					}
					this.appPath = folderBrowserDialog.SelectedPath + "/Unity.exe";
					if (FileVersionInfo.GetVersionInfo(this.appPath).FileVersion.Substring(0, 1) != "2")
					{
						this.textBox2.Text = FileVersionInfo.GetVersionInfo(this.appPath).FileVersion.Substring(0, 5);
						this.button2.Enabled = true;
						FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(this.appPath);
						this.patchAs = int.Parse(versionInfo.FileVersion[0].ToString()) * 100;
						this.patchAs += int.Parse(versionInfo.FileVersion[2].ToString()) * 10;
						this.patchAs += int.Parse(versionInfo.FileVersion[4].ToString());
					}
					else
					{
						this.textBox2.Text = FileVersionInfo.GetVersionInfo(this.appPath).FileVersion.Substring(0, 8);
						this.button2.Enabled = true;
						int num = int.Parse(FileVersionInfo.GetVersionInfo(this.appPath).FileVersion.Substring(5, 1));
						if (num > 0)
						{
							if (num == 1)
							{
								this.patchAs = 20171;
							}
							else
							{
								this.patchAs = 20172;
							}
						}
						else
						{
							this.patchAs = 2017;
						}
					}
					this.PrintDebug();
				}
				catch (Exception arg_1F5_0)
				{
					MessageBox.Show(arg_1F5_0.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			string text = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			Random random = new Random();
			this.textBox4.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
			this.textBox5.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
			this.textBox6.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
			this.textBox7.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
			this.textBox8.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			new Form2().ShowDialog();
		}

		private bool WriteLicenseToFile(string appDir, bool spfold, int version)
		{
			string text = string.Format("{0}-{1}-{2}-{3}-{4}", new object[]
			{
				this.textBox3.Text,
				this.textBox4.Text,
				this.textBox5.Text,
				this.textBox6.Text,
				this.textBox7.Text
			});
			if (text.Length + this.textBox8.TextLength != 26)
			{
				MessageBox.Show("Invalid Key must be \"22\" chars.", string.Empty, MessageBoxButtons.OK);
				return false;
			}
			this.PrintDebug();
			Console.WriteLine(version);
			List<byte> list = new List<byte>();
			List<byte> arg_9D_0 = list;
			byte[] expr_99 = new byte[4];
			expr_99[0] = 1;
			arg_9D_0.AddRange(expr_99);
			list.AddRange(Encoding.ASCII.GetBytes(string.Format("{0}-{1}", text, this.textBox8.Text)));
			if (version != 20172)
			{
				List<string> list2 = new List<string>
				{
					"<root>",
					"  <TimeStamp2 Value=\"cn/lkLOZ3vFvbQ==\"/>",
					"  <TimeStamp Value=\"jWj8PXAeZMPzUw==\"/>",
					"  <License id=\"Terms\">",
					"    <ClientProvidedVersion Value=\"\"/>",
					string.Format("    <DeveloperData Value=\"{0}\"/>", Convert.ToBase64String(list.ToArray())),
					"    <Features>"
				};
				int[] array = LicHeader.ReadAll();
				for (int i = 0; i < array.Length; i++)
				{
					int num = array[i];
					list2.Add(string.Format("      <Feature Value=\"{0}\"/>", num));
				}
				list2.Add("    </Features>");
				if (version < 500)
				{
					list2.Add("    <LicenseVersion Value=\"4.x\"/>");
				}
				if (version >= 500 && version < 2017)
				{
					list2.Add("    <LicenseVersion Value=\"5.x\"/>");
				}
				if (version == 2017)
				{
					list2.Add("    <LicenseVersion Value=\"2017.x\"/>");
				}
				if (version == 20171)
				{
					list2.Add("    <LicenseVersion Value=\"6.x\"/>");
				}
				list2.Add("    <MachineBindings>");
				list2.Add("    </MachineBindings>");
				list2.Add("    <MachineID Value=\"\"/>");
				list2.Add("    <SerialHash Value=\"\"/>");
				list2.Add(string.Format("    <SerialMasked Value=\"{0}-XXXX\"/>", text));
				DateTime now = DateTime.Now;
				list2.Add(string.Format("    <StartDate Value=\"{0}T00:00:00\"/>", now.AddDays(-1.0).ToString("yyyy-MM-dd")));
				list2.Add("    <StopDate Value=\"\"/>");
				list2.Add(string.Format("    <UpdateDate Value=\"{0}T00:00:00\"/>", now.AddYears(10).ToString("yyyy-MM-dd")));
				list2.Add("  </License>");
				list2.Add("");
				list2.Add("<Signature xmlns=\"http://www.w3.org/2000/09/xmldsig#\">");
				list2.Add("<SignedInfo>");
				list2.Add("<CanonicalizationMethod Algorithm=\"http://www.w3.org/TR/2001/REC-xml-c14n-20010315#WithComments\"/>");
				list2.Add("<SignatureMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#rsa-sha1\"/>");
				list2.Add("<Reference URI=\"#Terms\">");
				list2.Add("<Transforms>");
				list2.Add("<Transform Algorithm=\"http://www.w3.org/2000/09/xmldsig#enveloped-signature\"/>");
				list2.Add("</Transforms>");
				list2.Add("<DigestMethod Algorithm=\"http://www.w3.org/2000/09/xmldsig#sha1\"/>");
				list2.Add("<DigestValue>oeMc1KScgy617DHMPTxbYhqNjIM=</DigestValue>");
				list2.Add("</Reference>");
				list2.Add("</SignedInfo>");
				list2.Add("<SignatureValue>WuzMPTi0Ko1vffk9gf9ds/iU0b0K8UHaLpi4kWgm6q1am5MPTYYnzH1InaSWuzYo");
				list2.Add("EpJThKspOZdO0JISeEolNdJVf3JpsY55OsD8UaruvhwZn4r9pLeNSC7SzQ1rvAWP");
				list2.Add("h77XaHizhVVs15w6NYevP27LTxbZaem5L8Zs+34VKXQFeG4g0dEI/Jhl70TqE0CS");
				list2.Add("YNF+D0zqEtyMNHsh0Rq/vPLSzPXUN12jfPLZ3dO9B+9/mG7Ljd6emZjjLZUVuSKQ");
				list2.Add("uKxN5jlHZsm2kRMudijICV6YOWMPT+oZePlCg+BJQg5/xcN5aYVBDZhNeuNwQL1H");
				list2.Add("MPT/GJPxVuETgd9k8c4uDg==</SignatureValue>");
				list2.Add("</Signature>");
				list2.Add("</root>");
				string text2 = "";
				if (version < 500)
				{
					text2 = "Unity_v4.x.ulf";
				}
				if (version >= 500 && version < 2017)
				{
					text2 = "Unity_v5.x.ulf";
				}
				if (version == 2017)
				{
					text2 = "Unity_v2017.x.ulf";
				}
				if (version == 20171)
				{
					text2 = "Unity_lic.ulf";
				}
				string text3 = string.Empty;
				if (spfold)
				{
					text3 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Unity";
					if (!Directory.Exists(text3))
					{
						try
						{
							Directory.CreateDirectory(text3);
						}
						catch (Exception arg_3E0_0)
						{
							spfold = false;
							MessageBox.Show(arg_3E0_0.Message, string.Empty, MessageBoxButtons.OK);
						}
					}
				}
				if (spfold)
				{
					if (File.Exists(text3 + "/" + text2))
					{
						spfold = this.TestAtr(text3 + "/" + text2);
						if (spfold && MessageBox.Show(string.Format("Replace the \"{0}\\{1}\"?", text3, text2), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.OK)
						{
							list2.Clear();
							return true;
						}
					}
					if (spfold)
					{
						try
						{
							if (version < 500)
							{
								text2 = "Unity_v4.x.ulf";
							}
							if (version >= 500 && version < 2017)
							{
								text2 = "Unity_v5.x.ulf";
							}
							if (version == 2017)
							{
								text2 = "Unity_v2017.x.ulf";
							}
							if (version == 20171)
							{
								text2 = "Unity_lic.ulf";
							}
							if (text2 == "Unity_lic.ulf")
							{
								using (FileStream fileStream = new FileStream(text3 + "/" + text2, FileMode.Append))
								{
									foreach (string current in list2)
									{
										byte[] bytes = Encoding.ASCII.GetBytes(string.Format("{0}\r", current));
										fileStream.Write(bytes, 0, bytes.Length);
									}
									fileStream.Flush();
									fileStream.Close();
									goto IL_540;
								}
							}
							File.WriteAllLines(text3 + "/" + text2, list2);
							IL_540:;
						}
						catch (Exception arg_545_0)
						{
							spfold = false;
							MessageBox.Show(arg_545_0.Message, string.Empty, MessageBoxButtons.OK);
						}
					}
				}
				if (!spfold)
				{
					if (File.Exists(appDir + "/" + text2))
					{
						if (!this.TestAtr(appDir + "/" + text2))
						{
							list2.Clear();
							return false;
						}
						if (MessageBox.Show(string.Format("Replace the \"{0}\\{1}\"?", appDir, text2), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.OK)
						{
							list2.Clear();
							return true;
						}
					}
					try
					{
						if (version < 500)
						{
							text2 = "Unity_v4.x.ulf";
						}
						if (version >= 500 && version < 2017)
						{
							text2 = "Unity_v5.x.ulf";
						}
						if (version == 2017)
						{
							text2 = "Unity_v2017.x.ulf";
						}
						if (version == 20171)
						{
							text2 = "Unity_lic.ulf";
						}
						if (text2 == "Unity_lic.ulf")
						{
							using (FileStream fileStream2 = new FileStream(text3 + "/" + text2, FileMode.Append))
							{
								foreach (string current2 in list2)
								{
									byte[] bytes2 = Encoding.ASCII.GetBytes(string.Format("{0}\r", current2));
									fileStream2.Write(bytes2, 0, bytes2.Length);
								}
								fileStream2.Flush();
								fileStream2.Close();
								goto IL_6A2;
							}
						}
						File.WriteAllLines(text3 + "/" + text2, list2);
						IL_6A2:;
					}
					catch (Exception arg_6AA_0)
					{
						list2.Clear();
						MessageBox.Show(arg_6AA_0.Message, string.Empty, MessageBoxButtons.OK);
						bool result = false;
						return result;
					}
				}
				list2.Clear();
				return true;
			}
			string text4 = string.Format("{0}-{1}-{2}-{3}-{4}-{5}", new object[]
			{
				this.textBox3.Text,
				this.textBox4.Text,
				this.textBox5.Text,
				this.textBox6.Text,
				this.textBox7.Text,
				this.textBox8.Text
			});
			int[] array2 = LicHeader.ReadAll();
			if (text4.Length != 27)
			{
				MessageBox.Show("Invalid Key must be \"27\" chars.", string.Empty, MessageBoxButtons.OK);
				return false;
			}
			string path = "Unity_lic.ulf";
			string value = text4.Remove(text4.Length - 4, 4) + "XXXX";
			byte[] expr_780 = new byte[4];
			expr_780[0] = 1;
			string value2 = Convert.ToBase64String(expr_780.Concat(Encoding.ASCII.GetBytes(text4)).ToArray<byte>().ToArray<byte>());
			string value3 = "6.x";
			string value4 = "false";
			string value5 = "";
			string value6 = DateTime.UtcNow.AddDays(-1.0).ToString("s", CultureInfo.InvariantCulture);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			string value7 = "";
			string value8 = "";
			string value9 = "";
			string value10 = "";
			string value11 = DateTime.UtcNow.AddYears(10).ToString("s", CultureInfo.InvariantCulture);
			MemoryStream memoryStream = new MemoryStream();
			XmlWriterSettings settings = new XmlWriterSettings
			{
				Indent = true,
				IndentChars = "  ",
				NewLineChars = "\n",
				OmitXmlDeclaration = true,
				Encoding = Encoding.ASCII
			};
			using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings))
			{
				xmlWriter.WriteStartElement("root");
				xmlWriter.WriteStartElement("License");
				xmlWriter.WriteAttributeString("id", "Terms");
				xmlWriter.WriteStartElement("AlwaysOnline");
				xmlWriter.WriteAttributeString("Value", value4);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("ClientProvidedVersion");
				xmlWriter.WriteAttributeString("Value", value5);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("DeveloperData");
				xmlWriter.WriteAttributeString("Value", value2);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("Features");
				int[] array = array2;
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = array[i];
					xmlWriter.WriteStartElement("Feature");
					xmlWriter.WriteAttributeString("Value", num2.ToString());
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteFullEndElement();
				xmlWriter.WriteStartElement("InitialActivationDate");
				xmlWriter.WriteAttributeString("Value", value6);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("LicenseVersion");
				xmlWriter.WriteAttributeString("Value", value3);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("MachineBindings");
				foreach (KeyValuePair<string, string> current3 in dictionary)
				{
					xmlWriter.WriteStartElement("Binding");
					xmlWriter.WriteAttributeString("Key", current3.Key);
					xmlWriter.WriteAttributeString("Value", current3.Value);
					xmlWriter.WriteEndElement();
				}
				xmlWriter.WriteFullEndElement();
				xmlWriter.WriteStartElement("MachineID");
				xmlWriter.WriteAttributeString("Value", value7);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("SerialHash");
				xmlWriter.WriteAttributeString("Value", value8);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("SerialMasked");
				xmlWriter.WriteAttributeString("Value", value);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("StartDate");
				xmlWriter.WriteAttributeString("Value", value9);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("StopDate");
				xmlWriter.WriteAttributeString("Value", value10);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteStartElement("UpdateDate");
				xmlWriter.WriteAttributeString("Value", value11);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
			}
			memoryStream.Position = 0L;
			XmlDocument xmlDocument = new XmlDocument
			{
				PreserveWhitespace = true
			};
			xmlDocument.Load(memoryStream);
			SignedXml signedXml = new SignedXml(xmlDocument)
			{
				SigningKey = new RSACryptoServiceProvider()
			};
			Reference reference = new Reference
			{
				Uri = "#Terms"
			};
			reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
			signedXml.AddReference(reference);
			signedXml.ComputeSignature();
			StringBuilder stringBuilder = new StringBuilder();
			using (XmlWriter xmlWriter2 = XmlWriter.Create(stringBuilder, settings))
			{
				XmlDocument xmlDocument2 = new XmlDocument
				{
					InnerXml = xmlDocument.InnerXml
				};
				XmlElement expr_B8D = xmlDocument2.DocumentElement;
				if (expr_B8D != null)
				{
					expr_B8D.AppendChild(xmlDocument2.ImportNode(signedXml.GetXml(), true));
				}
				xmlDocument2.Save(xmlWriter2);
				xmlWriter2.Flush();
			}
			string contents = stringBuilder.Replace(" />", "/>").ToString();
			string text5 = spfold ? Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Unity") : appDir;
			string text6 = Path.Combine(text5, path);
			try
			{
				Directory.CreateDirectory(text5);
				if (File.Exists(text6) && this.TestAtr(text6) && MessageBox.Show(string.Format("Replace the \"{0}\"?", text6), string.Empty, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				{
					bool result = true;
					return result;
				}
				File.WriteAllText(text6, contents);
			}
			catch (Exception arg_C49_0)
			{
				MessageBox.Show(arg_C49_0.Message, string.Empty, MessageBoxButtons.OK);
				bool result = false;
				return result;
			}
			return true;
		}

		private bool TestAtr(string path)
		{
			try
			{
				FileAttributes fileAttributes = File.GetAttributes(path);
				fileAttributes = FileAttributes.Normal;
				File.SetAttributes(path, fileAttributes);
			}
			catch (Exception arg_16_0)
			{
				MessageBox.Show(arg_16_0.Message, string.Empty, MessageBoxButtons.OK);
				return false;
			}
			return true;
		}

		private bool ValidateTheFile(string fileName, ref int patchAs)
		{
			try
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(fileName);
				if (versionInfo.ProductName == null || versionInfo.FileVersion == null || !versionInfo.FileVersion.Contains("."))
				{
					throw new Exception("Can't parse the file info.");
				}
				if (versionInfo.ProductName.ToLower() != "unity")
				{
					throw new Exception("This patch is created only for Unity3d :).");
				}
				int num = 0;
				int.TryParse(versionInfo.FileVersion.Split(new char[]
				{
					'.'
				})[0], out num);
				if (num == 4)
				{
					patchAs = 0;
				}
				else
				{
					if (num <= 4)
					{
						throw new Exception("This patch will not working on 2.0x - 3.0x versions.");
					}
					patchAs = 1;
				}
			}
			catch (Exception arg_97_0)
			{
				MessageBox.Show(arg_97_0.Message, string.Empty, MessageBoxButtons.OK);
				return false;
			}
			return true;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text.Length < 3)
			{
				this.textBox1.Text = Application.StartupPath;
			}
			string text = this.textBox1.Text;
			try
			{
				Directory.SetCurrentDirectory(text);
			}
			catch (Exception arg_37_0)
			{
				MessageBox.Show(arg_37_0.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			if (this.patchAs < 500)
			{
				this.WriteLicenseToFile(text, true, this.patchAs);
				return;
			}
			if (this.patchAs >= 500 && this.patchAs < 529)
			{
				this.WriteLicenseToFile(text, true, this.patchAs);
				return;
			}
			if (this.patchAs >= 530 && this.patchAs < 2017)
			{
				this.WriteLicenseToFile(text, true, this.patchAs);
				return;
			}
			if (this.patchAs == 2017)
			{
				this.WriteLicenseToFile(text, true, 2017);
				return;
			}
			if (this.patchAs == 20171)
			{
				this.WriteLicenseToFile(text, true, 20171);
				return;
			}
			this.WriteLicenseToFile(text, true, 20172);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (this.textBox1.Text.Length < 3)
			{
				this.textBox1.Text = Application.StartupPath;
			}
			string text = this.textBox1.Text;
			try
			{
				if (Process.GetProcessesByName("unity").Length != 0)
				{
					throw new Exception("Need to close application first.");
				}
			}
			catch (Exception arg_49_0)
			{
				MessageBox.Show(arg_49_0.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			File.Copy(text + "/Unity.exe", text + "/Unity.exe.bak", true);
			NLogger.Clear();
			string se = "";
			string rep = "";
			string se2 = "";
			string rep2 = "";
			if (this.patchAs < 530)
			{
				se = "CC 55 8B EC 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				rep = "CC B0 01 C3 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				se2 = "CC 40 57 48 83 EC 30 80 79 ?? ?? 48 8B F9 75 ??";
				rep2 = "CC B0 01 C3 90 90 90 80 79 ?? ?? 48 8B F9 75 ??";
			}
			if (this.patchAs >= 530 && this.patchAs < 20172)
			{
				se = "CC 55 8B EC 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				rep = "CC B0 01 C3 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				se2 = "CC 40 57 48 83 EC 30 80 79 ?? ?? 48 8B F9 75 ??";
				rep2 = "CC B0 01 C3 90 90 90 80 79 ?? ?? 48 8B F9 75 ??";
			}
			if (this.patchAs >= 20172)
			{
				se = "CC 55 8B EC 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				rep = "CC B0 01 C3 83 EC ?? 53 56 8B F1 80 7E ?? ?? 57 75 ??";
				se2 = "CC 48 89 5C 24 10 48 89 6C 24 18 56 41 54 41 55 ?? 83 EC 30 ?? 8B E9";
				rep2 = "CC B0 01 C3 90 90 48 89 6C 24 18 56 41 54 41 55 ?? 83 EC 30 ?? 8B E9";
			}
			Patcher patcher = new Patcher(text + "/Unity.exe");
			if (patcher.AddString(se, rep, 1u, 0u))
			{
				if (patcher.Patch())
				{
					NLogger.LastMessage();
					this.WriteLicenseToFile(text, true, this.patchAs);
					return;
				}
				if (this.patchAs >= 500)
				{
					patcher.Patterns.Clear();
					if (patcher.AddString(se2, rep2, 1u, 0u) && patcher.Patch())
					{
						NLogger.LastMessage();
						this.WriteLicenseToFile(text, true, this.patchAs);
						return;
					}
				}
			}
			NLogger.LastMessage();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
			{
				string text = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
				Random random = new Random();
				this.textBox4.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
				this.textBox5.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
				this.textBox6.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
				this.textBox7.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
				this.textBox8.Text = text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString() + text[random.Next(0, 36)].ToString();
				return;
			}
			new Form3().ShowDialog();
		}

		private void PrintDebug()
		{
			Console.WriteLine(this.appPath);
			Console.WriteLine(this.patchAs);
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
			this.label1 = new Label();
			this.textBox1 = new TextBox();
			this.label2 = new Label();
			this.textBox2 = new TextBox();
			this.label3 = new Label();
			this.textBox3 = new TextBox();
			this.textBox4 = new TextBox();
			this.textBox5 = new TextBox();
			this.textBox6 = new TextBox();
			this.textBox7 = new TextBox();
			this.textBox8 = new TextBox();
			this.button1 = new Button();
			this.button2 = new Button();
			this.button3 = new Button();
			this.button4 = new Button();
			this.button5 = new Button();
			this.textBox9 = new TextBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Location = new Point(13, 13);
			this.label1.Name = "label1";
			this.label1.Size = new Size(194, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Folder containing Unity.exe";
			this.textBox1.Enabled = false;
			this.textBox1.Location = new Point(16, 35);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new Size(383, 26);
			this.textBox1.TabIndex = 1;
			this.textBox1.TabStop = false;
			this.textBox1.Text = "Unity folder not selected yet !";
			this.label2.AutoSize = true;
			this.label2.Location = new Point(12, 69);
			this.label2.Name = "label2";
			this.label2.Size = new Size(131, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Version Selected:";
			this.textBox2.Enabled = false;
			this.textBox2.Location = new Point(16, 90);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new Size(170, 26);
			this.textBox2.TabIndex = 3;
			this.textBox2.TabStop = false;
			this.textBox2.Text = "?";
			this.textBox2.TextAlign = HorizontalAlignment.Center;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(12, 124);
			this.label3.Name = "label3";
			this.label3.Size = new Size(112, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Serial Number:";
			this.textBox3.Enabled = false;
			this.textBox3.Location = new Point(16, 145);
			this.textBox3.Name = "textBox3";
			this.textBox3.ReadOnly = true;
			this.textBox3.Size = new Size(28, 26);
			this.textBox3.TabIndex = 5;
			this.textBox3.TabStop = false;
			this.textBox3.Text = "U3";
			this.textBox4.Enabled = false;
			this.textBox4.Location = new Point(50, 145);
			this.textBox4.Name = "textBox4";
			this.textBox4.ReadOnly = true;
			this.textBox4.Size = new Size(65, 26);
			this.textBox4.TabIndex = 6;
			this.textBox4.TabStop = false;
			this.textBox4.Text = "AAAA";
			this.textBox5.Enabled = false;
			this.textBox5.Location = new Point(121, 145);
			this.textBox5.Name = "textBox5";
			this.textBox5.ReadOnly = true;
			this.textBox5.Size = new Size(65, 26);
			this.textBox5.TabIndex = 7;
			this.textBox5.TabStop = false;
			this.textBox5.Text = "AAAA";
			this.textBox6.Enabled = false;
			this.textBox6.Location = new Point(192, 145);
			this.textBox6.Name = "textBox6";
			this.textBox6.ReadOnly = true;
			this.textBox6.Size = new Size(65, 26);
			this.textBox6.TabIndex = 8;
			this.textBox6.TabStop = false;
			this.textBox6.Text = "AAAA";
			this.textBox7.Enabled = false;
			this.textBox7.Location = new Point(263, 145);
			this.textBox7.Name = "textBox7";
			this.textBox7.ReadOnly = true;
			this.textBox7.Size = new Size(65, 26);
			this.textBox7.TabIndex = 9;
			this.textBox7.TabStop = false;
			this.textBox7.Text = "AAAA";
			this.textBox8.Enabled = false;
			this.textBox8.Location = new Point(334, 145);
			this.textBox8.Name = "textBox8";
			this.textBox8.ReadOnly = true;
			this.textBox8.Size = new Size(65, 26);
			this.textBox8.TabIndex = 10;
			this.textBox8.TabStop = false;
			this.textBox8.Text = "NUUN";
			this.button1.Location = new Point(405, 35);
			this.button1.Name = "button1";
			this.button1.Size = new Size(152, 26);
			this.button1.TabIndex = 11;
			this.button1.TabStop = false;
			this.button1.Text = "Browse";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new EventHandler(this.button1_Click);
			this.button2.Location = new Point(405, 177);
			this.button2.Name = "button2";
			this.button2.Size = new Size(152, 155);
			this.button2.TabIndex = 12;
			this.button2.TabStop = false;
			this.button2.Text = "PATCH";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new EventHandler(this.button2_Click);
			this.button3.Location = new Point(405, 89);
			this.button3.Name = "button3";
			this.button3.Size = new Size(152, 26);
			this.button3.TabIndex = 13;
			this.button3.TabStop = false;
			this.button3.Text = "Create License";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new EventHandler(this.button3_Click);
			this.button4.Location = new Point(405, 145);
			this.button4.Name = "button4";
			this.button4.Size = new Size(152, 26);
			this.button4.TabIndex = 14;
			this.button4.TabStop = false;
			this.button4.Text = "<<< Randomize";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new EventHandler(this.button4_Click);
			this.button5.Location = new Point(247, 90);
			this.button5.Name = "button5";
			this.button5.Size = new Size(152, 26);
			this.button5.TabIndex = 15;
			this.button5.TabStop = false;
			this.button5.Text = "License Options...";
			this.button5.UseVisualStyleBackColor = true;
			this.button5.Click += new EventHandler(this.button5_Click);
			this.textBox9.Font = new Font("Arial", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.textBox9.Location = new Point(16, 177);
			this.textBox9.Multiline = true;
			this.textBox9.Name = "textBox9";
			this.textBox9.ReadOnly = true;
			this.textBox9.ScrollBars = ScrollBars.Vertical;
			this.textBox9.Size = new Size(383, 155);
			this.textBox9.TabIndex = 18;
			this.textBox9.TabStop = false;
			this.textBox9.Text = "Requirements: .NET v4.0\r\n\r\nUpdates see here :\r\nhttps://forum.cgpersia.com/f13/unity-v4-x-x-v5-x-x-v2017-x-x-updates-win-mac-medicines-updated-regularly-99173/\r\n\r\ncredit: \"for_file\"  - for helping.";
			base.AutoScaleDimensions = new SizeF(9f, 18f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.InactiveCaption;
			base.ClientSize = new Size(574, 344);
			base.Controls.Add(this.textBox9);
			base.Controls.Add(this.button5);
			base.Controls.Add(this.button4);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.textBox8);
			base.Controls.Add(this.textBox7);
			base.Controls.Add(this.textBox6);
			base.Controls.Add(this.textBox5);
			base.Controls.Add(this.textBox4);
			base.Controls.Add(this.textBox3);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.textBox2);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.textBox1);
			base.Controls.Add(this.label1);
			this.Font = new Font("Arial", 12f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedSingle;
			//base.Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
			base.Margin = new Padding(4);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "Form1";
			base.SizeGripStyle = SizeGripStyle.Hide;
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "Unity Pro Universal Patcher v2017.6 (aug2017)";
			base.Load += new EventHandler(this.Form1_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
