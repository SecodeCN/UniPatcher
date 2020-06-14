using System;
using System.Collections.Generic;
using System.Linq;

namespace UniPatcher
{
    public class LicHeader
    {
        public class LicSettings
        {
            public int Android;

            public int Blackberry;

            public bool Educt;

            public int Flash;

            public int IPhone;

            public bool Nin = true;

            public bool NRelease;

            public bool PlayStation = true;

            public int SamsungTv;

            public bool Team = true;

            public int Tizen;

            public int Type = 1;

            public bool Wii = true;

            public int WinStore;

            public bool Xbox = true;
        }

        public static LicHeader.LicSettings PropLicSettings
        {
            get;
            set;
        }

        public static int[] ReadAll()
        {
            List<int> list = new List<int>();
            switch (LicHeader.PropLicSettings.Type)
            {
                case 0:
                    list.Add(0);
                    list.Add(1);
                    list.Add(16);
                    break;
                case 1:
                    list.Add(0);
                    list.Add(1);
                    break;
                case 2:
                    list.Add(62);
                    break;
            }
            if (LicHeader.PropLicSettings.Team)
            {
                list.Add(2);
            }
            int num = LicHeader.PropLicSettings.IPhone;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(3);
                    list.Add(9);
                }
            }
            else
            {
                list.Add(3);
                list.Add(4);
                list.Add(9);
            }
            if (LicHeader.PropLicSettings.Xbox)
            {
                list.Add(5);
                list.Add(33);
                list.Add(11);
            }
            if (LicHeader.PropLicSettings.PlayStation)
            {
                list.Add(6);
                list.Add(10);
                list.Add(30);
                list.Add(31);
                list.Add(32);
            }
            if (LicHeader.PropLicSettings.Wii)
            {
                list.Add(23);
                list.Add(36);
            }
            if (LicHeader.PropLicSettings.Nin)
            {
                list.Add(39);
                list.Add(35);
            }
            if (LicHeader.PropLicSettings.NRelease)
            {
                list.Add(61);
            }
            if (LicHeader.PropLicSettings.Educt)
            {
                list.Add(63);
            }
            num = LicHeader.PropLicSettings.Android;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(12);
                }
            }
            else
            {
                list.Add(12);
                list.Add(13);
            }
            num = LicHeader.PropLicSettings.Flash;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(14);
                }
            }
            else
            {
                list.Add(14);
                list.Add(15);
            }
            num = LicHeader.PropLicSettings.WinStore;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(19);
                }
            }
            else
            {
                list.Add(19);
                list.Add(20);
                list.Add(21);
                list.Add(26);
            }
            num = LicHeader.PropLicSettings.SamsungTv;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(24);
                    list.Add(34);
                }
            }
            else
            {
                list.Add(24);
                list.Add(25);
                list.Add(34);
            }
            num = LicHeader.PropLicSettings.Blackberry;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(17);
                    list.Add(28);
                }
            }
            else
            {
                list.Add(17);
                list.Add(18);
                list.Add(28);
            }
            num = LicHeader.PropLicSettings.Tizen;
            if (num != 0)
            {
                if (num == 1)
                {
                    list.Add(33);
                    list.Add(29);
                }
            }
            else
            {
                list.Add(33);
                list.Add(34);
                list.Add(29);
            }
            list.Sort();
            return list.Distinct<int>().ToArray<int>();
        }

        static LicHeader()
        {
            // Note: this type is marked as 'beforefieldinit'.
            LicHeader.PropLicSettings = new LicHeader.LicSettings();
        }
    }
}