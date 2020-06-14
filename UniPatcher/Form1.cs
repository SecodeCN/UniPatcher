using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

namespace UniPatcher
{
    public class Form1 : Form
    {
        private string appPath = "C:";

        private int patchAs = 2019;

        #region �滻ֵ
        private static readonly byte[] Fin1 = new byte[]
        {
            103,
            101,
            116,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            73,
            110,
            102,
            111,
            40,
            99,
            97,
            108,
            108,
            98,
            97,
            99,
            107,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            47,
            47,
            32,
            108,
            111,
            97,
            100,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            13,
            10,
            32,
            32,
            32,
            32,
            47,
            47,
            32,
            103,
            101,
            116,
            32,
            108,
            97,
            116,
            101,
            115,
            116,
            32,
            100,
            97,
            116,
            97,
            32,
            102,
            114,
            111,
            109,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            67,
            111,
            114,
            101,
            13,
            10,
            32,
            32,
            32,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            73,
            110,
            102,
            111,
            46,
            97,
            99,
            116,
            105,
            118,
            97,
            116,
            101,
            100,
            32,
            61,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            67
        };

        private static readonly byte[] Rep1 = new byte[]
        {
            103,
            101,
            116,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            73,
            110,
            102,
            111,
            40,
            99,
            97,
            108,
            108,
            98,
            97,
            99,
            107,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            47,
            47,
            32,
            108,
            111,
            97,
            100,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            13,
            10,
            32,
            32,
            32,
            32,
            47,
            47,
            32,
            103,
            101,
            116,
            32,
            108,
            97,
            116,
            101,
            115,
            116,
            32,
            100,
            97,
            116,
            97,
            32,
            102,
            114,
            111,
            109,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            67,
            111,
            114,
            101,
            13,
            10,
            32,
            32,
            32,
            32,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            73,
            110,
            102,
            111,
            46,
            97,
            99,
            116,
            105,
            118,
            97,
            116,
            101,
            100,
            32,
            61,
            32,
            32,
            116,
            114,
            117,
            101,
            59,
            47,
            47
        };

        private static readonly byte[] Fin2 = new byte[]
        {
            118,
            101,
            114,
            105,
            102,
            121,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            68,
            97,
            116,
            97,
            40,
            120,
            109,
            108,
            44,
            32,
            110,
            101,
            119,
            102,
            105,
            108,
            101,
            32,
            61,
            32,
            102,
            97,
            108,
            115,
            101,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            114,
            101,
            116,
            117,
            114,
            110,
            32,
            110,
            101,
            119,
            32,
            80,
            114,
            111,
            109,
            105,
            115,
            101,
            40,
            40,
            114,
            101,
            115,
            111,
            108,
            118,
            101,
            44,
            32,
            114,
            101,
            106,
            101,
            99,
            116,
            41,
            32,
            61,
            62,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            105,
            102,
            32,
            40,
            120,
            109,
            108,
            32,
            61,
            61,
            61,
            32,
            39,
            39,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            116,
            104,
            105,
            115,
            46,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            83,
            116,
            97,
            116,
            117,
            115,
            32,
            61,
            32,
            76,
            73,
            67,
            69,
            78,
            83,
            69,
            95,
            83,
            84,
            65,
            84,
            85,
            83,
            46,
            107,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            69,
            114,
            114,
            111,
            114,
            70,
            108,
            97,
            103,
            95,
            78,
            111,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            114,
            101,
            106,
            101,
            99,
            116,
            40,
            41,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            114,
            101,
            116,
            117,
            114,
            110,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            125
        };

        private static readonly byte[] Rep2 = new byte[]
        {
            118,
            101,
            114,
            105,
            102,
            121,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            68,
            97,
            116,
            97,
            40,
            120,
            109,
            108,
            44,
            32,
            110,
            101,
            119,
            102,
            105,
            108,
            101,
            32,
            61,
            32,
            102,
            97,
            108,
            115,
            101,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            114,
            101,
            116,
            117,
            114,
            110,
            32,
            110,
            101,
            119,
            32,
            80,
            114,
            111,
            109,
            105,
            115,
            101,
            40,
            40,
            114,
            101,
            115,
            111,
            108,
            118,
            101,
            44,
            32,
            114,
            101,
            106,
            101,
            99,
            116,
            41,
            32,
            61,
            62,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            114,
            101,
            115,
            111,
            108,
            118,
            101,
            40,
            116,
            114,
            117,
            101,
            41,
            59,
            47,
            42,
            39,
            41,
            32,
            123,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            116,
            104,
            105,
            115,
            46,
            108,
            105,
            99,
            101,
            110,
            115,
            101,
            83,
            116,
            97,
            116,
            117,
            115,
            32,
            61,
            32,
            76,
            73,
            67,
            69,
            78,
            83,
            69,
            95,
            83,
            84,
            65,
            84,
            85,
            83,
            46,
            107,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            69,
            114,
            114,
            111,
            114,
            70,
            108,
            97,
            103,
            95,
            78,
            111,
            76,
            105,
            99,
            101,
            110,
            115,
            101,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            114,
            101,
            106,
            101,
            99,
            116,
            40,
            41,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            32,
            114,
            101,
            116,
            117,
            114,
            110,
            59,
            13,
            10,
            32,
            32,
            32,
            32,
            32,
            42,
            47
        };

        private static readonly byte[] Fin2019 = new byte[]
        {
            76,
            141,
            68,
            36,
            32,
            72,
            141,
            85,
            224,
            72,
            139,
            206,
            232,
            195,
            46,
            0,
            0,
            132,
            192,
            65
        };

        private static readonly byte[] Rep2019 = new byte[]
        {
            76,
            141,
            68,
            36,
            32,
            72,
            141,
            85,
            224,
            72,
            139,
            206,
            232,
            195,
            46,
            0,
            0,
            176,
            1,
            65
        };
        #endregion

        private IContainer components;

        private Label label1;

        private TextBox textBox1;

        private TextBox textBox4;

        private TextBox textBox5;

        private TextBox textBox6;

        private TextBox textBox7;

        private TextBox textBox8;

        private Button button1;

        private Button button2;

        private TextBox textBox9;

        private Button button6;

        public Form1()
        {
            this.InitializeComponent();
            this.button2.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
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
                    if (FileVersionInfo.GetVersionInfo(this.appPath).FileVersion.Substring(0, 4) != "2019")
                    {
                        this.button2.Enabled = false;
                        throw new IOException("Not v2019!");
                    }
                    TextBox expr_E2 = this.textBox9;
                    expr_E2.Text = expr_E2.Text + "Unity v2019 found. :)" + Environment.NewLine;
                    if (!File.Exists("C:\\Program Files\\Unity Hub\\Unity Hub.exe"))
                    {
                        TextBox expr_147 = this.textBox9;
                        expr_147.Text = expr_147.Text + "Unity Hub not found. :(" + Environment.NewLine;
                        TextBox expr_167 = this.textBox9;
                        expr_167.Text = expr_167.Text + "Please install Unity Hub and start again!!!" + Environment.NewLine;
                        throw new IOException("Unity Hub not installed!");
                    }
                    TextBox expr_10E = this.textBox9;
                    expr_10E.Text = expr_10E.Text + "Unity Hub found. :)" + Environment.NewLine;
                    this.button6.Enabled = true;
                    this.patchAs = 2019;
                }
                catch (Exception arg_18E_0)
                {
                    MessageBox.Show(arg_18E_0.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }

        private bool WriteLicenseToFile(string appDir, bool spfold, int version)
        {
            string text = string.Format("{0}-{1}-{2}-{3}-{4}", new object[]
            {
                "U3",
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
            List<byte> list = new List<byte>();
            List<byte> arg_8B_0 = list;
            byte[] expr_87 = new byte[4];
            expr_87[0] = 1;
            arg_8B_0.AddRange(expr_87);
            list.AddRange(Encoding.ASCII.GetBytes(string.Format("{0}-{1}", text, this.textBox8.Text)));
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
            list2.Add("    <LicenseVersion Value=\"6.x\"/>");
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
            string text2 = string.Empty;
            if (spfold)
            {
                text2 = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Unity";
                if (!Directory.Exists(text2))
                {
                    try
                    {
                        Directory.CreateDirectory(text2);
                        goto IL_345;
                    }
                    catch (Exception arg_32F_0)
                    {
                        spfold = false;
                        MessageBox.Show(arg_32F_0.Message, string.Empty, MessageBoxButtons.OK);
                        goto IL_345;
                    }
                }
                spfold = true;
            }
            IL_345:
            string text3 = text2 + "\\Unity_lic.ulf";
            if (spfold)
            {
                if (File.Exists(text3))
                {
                    File.Delete(text3);
                }
                if (spfold)
                {
                    try
                    {
                        if (text3 == text2 + "\\Unity_lic.ulf")
                        {
                            using (FileStream fileStream = new FileStream(text3, FileMode.Append))
                            {
                                foreach (string current in list2)
                                {
                                    byte[] bytes = Encoding.ASCII.GetBytes(current + "\r");
                                    fileStream.Write(bytes, 0, bytes.Length);
                                }
                                fileStream.Flush();
                                fileStream.Close();
                                goto IL_404;
                            }
                        }
                        File.WriteAllLines(text3, list2);
                        IL_404:;
                    }
                    catch (Exception arg_409_0)
                    {
                        spfold = false;
                        MessageBox.Show(arg_409_0.Message, string.Empty, MessageBoxButtons.OK);
                    }
                }
            }
            if (!spfold)
            {
                if (File.Exists(text3))
                {
                    File.Delete(text3);
                }
                try
                {
                    if (text3 == text2 + "\\Unity_lic.ulf")
                    {
                        using (FileStream fileStream2 = new FileStream(text3, FileMode.Append))
                        {
                            foreach (string current2 in list2)
                            {
                                byte[] bytes2 = Encoding.ASCII.GetBytes(current2 + "\r");
                                fileStream2.Write(bytes2, 0, bytes2.Length);
                            }
                            fileStream2.Flush();
                            fileStream2.Close();
                            goto IL_4C8;
                        }
                    }
                    File.WriteAllLines(text3, list2);
                    IL_4C8:;
                }
                catch (Exception arg_4D0_0)
                {
                    list2.Clear();
                    MessageBox.Show(arg_4D0_0.Message, string.Empty, MessageBoxButtons.OK);
                    return false;
                }
            }
            list2.Clear();
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

        private void Button3_Click(object sender, EventArgs e)
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
            this.WriteLicenseToFile(text, true, 2019);
        }

        private void Button2_Click(object sender, EventArgs e)
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
            if (!File.Exists(text + "/Unity.exe.bak"))
            {
                File.Copy(text + "/Unity.exe", text + "/Unity.exe.bak", true);
            }
            NLogger.Clear();
            string se = "4C 8D 44 24 20 48 8D 55 E0 48 8B CE E8 ?? ?? 00 00 84 C0 41";
            string rep = "4C 8D 44 24 20 48 8D 55 E0 48 8B CE E8 ?? ?? 00 00 B0 01 41";
            Patcher patcher = new Patcher(text + "/Unity.exe");
            patcher.Patterns.Clear();
            if (patcher.AddString(se, rep, 1u, 0u))
            {
                if (patcher.Patch())
                {
                    TextBox expr_D6 = this.textBox9;
                    expr_D6.Text = expr_D6.Text + "Unity patched successfully. :)   License being re-written." + Environment.NewLine;
                    this.WriteLicenseToFile(text, true, this.patchAs);
                    TextBox expr_105 = this.textBox9;
                    expr_105.Text = expr_105.Text + "License written. Enjoy :)" + Environment.NewLine;
                    return;
                }
                TextBox expr_126 = this.textBox9;
                expr_126.Text = expr_126.Text + "Unity patching failed. Already patched or unpatchable!!!" + Environment.NewLine;
            }
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

        private void Button6_Click(object sender, EventArgs e)
        {
            if (Form1.FindBytes("C:\\Program Files\\Unity Hub\\resources\\app.asar", Form1.Rep1) != -1)
            {
                TextBox expr_18 = this.textBox9;
                expr_18.Text = expr_18.Text + "Unity Hub already patched. :)   Now fix Unity." + Environment.NewLine;
                this.button6.Enabled = false;
                this.button2.Enabled = true;
                return;
            }
            Form1.PatchASAR1("C:\\Program Files\\Unity Hub\\resources\\app.asar");
            Form1.PatchASAR2("C:\\Program Files\\Unity Hub\\resources\\app.asar");
            TextBox expr_65 = this.textBox9;
            expr_65.Text = expr_65.Text + "Unity Hub patched successfully. :)   Now fix Unity." + Environment.NewLine;
            this.button6.Enabled = false;
            this.button2.Enabled = true;
        }

        private void NewPatching()
        {
            string text = this.textBox1.Text + "\\Unity.exe";
            if (Form1.FindBytes(text, Form1.Rep2019) != -1)
            {
                TextBox expr_2A = this.textBox9;
                expr_2A.Text = expr_2A.Text + "Unity already patched. :)   License being re-written." + Environment.NewLine;
                this.WriteLicenseToFile(text, true, 2019);
                TextBox expr_58 = this.textBox9;
                expr_58.Text = expr_58.Text + "License re-written. Enjoy :)" + Environment.NewLine;
                return;
            }
            if (Form1.FindBytes(text, Form1.Fin2019) != -1)
            {
                Form1.Patch2019(text);
                TextBox expr_8D = this.textBox9;
                expr_8D.Text = expr_8D.Text + "Unity patched successfully. :)   License being re-written." + Environment.NewLine;
                this.WriteLicenseToFile(text, true, 2019);
                TextBox expr_BB = this.textBox9;
                expr_BB.Text = expr_BB.Text + "License written. Enjoy :)" + Environment.NewLine;
                return;
            }
            TextBox expr_DC = this.textBox9;
            expr_DC.Text = expr_DC.Text + "Pattern not found!" + Environment.NewLine;
        }

        private static int FindBytes(string fileTarget, byte[] sequence)
        {
            byte[] array = File.ReadAllBytes(fileTarget);
            int i = 0;
            int num = array.Length - sequence.Length;
            byte b = sequence[0];
            while (i < num)
            {
                if (array[i] == b)
                {
                    int num2 = 1;
                    while (num2 < sequence.Length && array[i + num2] == sequence[num2])
                    {
                        if (num2 == sequence.Length - 1)
                        {
                            return i;
                        }
                        num2++;
                    }
                }
                i++;
            }
            return -1;
        }

        private static bool DetectPatch1(byte[] sequence, int position)
        {
            if (position + Form1.Fin1.Length > sequence.Length)
            {
                return false;
            }
            for (int i = 0; i < Form1.Fin1.Length; i++)
            {
                if (Form1.Fin1[i] != sequence[position + i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void PatchASAR1(string originalFile)
        {
            byte[] array = File.ReadAllBytes(originalFile);
            for (int i = 0; i < array.Length; i++)
            {
                if (Form1.DetectPatch1(array, i))
                {
                    for (int j = 0; j < Form1.Fin1.Length; j++)
                    {
                        array[i + j] = Form1.Rep1[j];
                    }
                }
            }
            File.WriteAllBytes(originalFile, array);
        }

        private static bool DetectPatch2(byte[] sequence, int position)
        {
            if (position + Form1.Fin2.Length > sequence.Length)
            {
                return false;
            }
            for (int i = 0; i < Form1.Fin2.Length; i++)
            {
                if (Form1.Fin2[i] != sequence[position + i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void PatchASAR2(string originalFile)
        {
            byte[] array = File.ReadAllBytes(originalFile);
            for (int i = 0; i < array.Length; i++)
            {
                if (Form1.DetectPatch2(array, i))
                {
                    for (int j = 0; j < Form1.Fin2.Length; j++)
                    {
                        array[i + j] = Form1.Rep2[j];
                    }
                }
            }
            File.WriteAllBytes(originalFile, array);
        }

        private static bool DetectPatch2019(byte[] sequence, int position)
        {
            if (position + Form1.Fin2019.Length > sequence.Length)
            {
                return false;
            }
            for (int i = 0; i < Form1.Fin2019.Length; i++)
            {
                if (Form1.Fin2019[i] != sequence[position + i])
                {
                    return false;
                }
            }
            return true;
        }

        private static void Patch2019(string originalFile)
        {
            byte[] array = File.ReadAllBytes(originalFile);
            for (int i = 0; i < array.Length; i++)
            {
                if (Form1.DetectPatch2019(array, i))
                {
                    for (int j = 0; j < Form1.Fin2019.Length; j++)
                    {
                        array[i + j] = Form1.Rep2019[j];
                    }
                }
            }
            File.WriteAllBytes(originalFile, array);
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
            this.textBox4 = new TextBox();
            this.textBox5 = new TextBox();
            this.textBox6 = new TextBox();
            this.textBox7 = new TextBox();
            this.textBox8 = new TextBox();
            this.button1 = new Button();
            this.button2 = new Button();
            this.textBox9 = new TextBox();
            this.button6 = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(251, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Folder containing Unity.exe (v2019)";
            this.textBox1.Enabled = false;
            this.textBox1.Location = new Point(16, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(383, 26);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.Text = "Unity folder not selected yet !";
            this.textBox4.Enabled = false;
            this.textBox4.Location = new Point(647, 36);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new Size(65, 26);
            this.textBox4.TabIndex = 6;
            this.textBox4.TabStop = false;
            this.textBox4.Text = "AAAA";
            this.textBox5.Enabled = false;
            this.textBox5.Location = new Point(647, 66);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new Size(65, 26);
            this.textBox5.TabIndex = 7;
            this.textBox5.TabStop = false;
            this.textBox5.Text = "AAAA";
            this.textBox6.Enabled = false;
            this.textBox6.Location = new Point(647, 98);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new Size(65, 26);
            this.textBox6.TabIndex = 8;
            this.textBox6.TabStop = false;
            this.textBox6.Text = "AAAA";
            this.textBox7.Enabled = false;
            this.textBox7.Location = new Point(647, 130);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new Size(65, 26);
            this.textBox7.TabIndex = 9;
            this.textBox7.TabStop = false;
            this.textBox7.Text = "AAAA";
            this.textBox8.Enabled = false;
            this.textBox8.Location = new Point(647, 162);
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
            this.button1.Click += new EventHandler(this.Button1_Click);
            this.button2.Location = new Point(406, 143);
            this.button2.Name = "button2";
            this.button2.Size = new Size(152, 80);
            this.button2.TabIndex = 12;
            this.button2.TabStop = false;
            this.button2.Text = "2nd... Fix Unity";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new EventHandler(this.Button2_Click);
            this.textBox9.Font = new Font("Arial", 10f, FontStyle.Regular, GraphicsUnit.Point, 0);
            this.textBox9.Location = new Point(17, 68);
            this.textBox9.Multiline = true;
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.ScrollBars = ScrollBars.Vertical;
            this.textBox9.Size = new Size(383, 155);
            this.textBox9.TabIndex = 18;
            this.textBox9.TabStop = false;
            this.textBox9.Text = "This release of UniPatcher is for Unity v2019.x.x ONLY.\r\n\r\n";
            this.button6.Enabled = false;
            this.button6.Location = new Point(406, 68);
            this.button6.Name = "button6";
            this.button6.Size = new Size(151, 69);
            this.button6.TabIndex = 19;
            this.button6.Text = "1st... Fix Hub";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new EventHandler(this.Button6_Click);
            base.AutoScaleDimensions = new SizeF(9f, 18f);
            base.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = SystemColors.InactiveCaption;
            base.ClientSize = new Size(567, 231);
            base.Controls.Add(this.button6);
            base.Controls.Add(this.textBox9);
            base.Controls.Add(this.button2);
            base.Controls.Add(this.button1);
            base.Controls.Add(this.textBox8);
            base.Controls.Add(this.textBox7);
            base.Controls.Add(this.textBox6);
            base.Controls.Add(this.textBox5);
            base.Controls.Add(this.textBox4);
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
            this.Text = "UniPatcher v3 (for v2019 x64)";
            base.Load += new EventHandler(this.Form1_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }
    }
}
