using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace UniPatcher
{
    public class Patcher : IDisposable
    {
        public class PatcherState
        {
            public readonly uint Fail;

            public readonly bool FileHasChanged;

            public readonly uint Good;

            public readonly string StringState;

            public PatcherState(bool fileHasChanged, uint good, uint fail, string stringState = "")
            {
                this.FileHasChanged = fileHasChanged;
                this.Good = good;
                this.Fail = fail;
                this.StringState = stringState;
            }
        }

        public struct MainPattern
        {
            public readonly Patcher.Pattern Ptrn;

            public readonly long[] StreamPosition;

            public int SuccessfullyFound
            {
                get
                {
                    if (this.StreamPosition == null)
                    {
                        return 0;
                    }
                    return this.StreamPosition.Length;
                }
            }

            public bool Success
            {
                get
                {
                    return this.StreamPosition != null && ((this.Ptrn.BrakAfter < 1u && this.StreamPosition.Length != 0) || (ulong)(this.Ptrn.BrakAfter - this.Ptrn.ReplaceAfter) == (ulong)((long)this.StreamPosition.Length));
                }
            }

            public MainPattern(Patcher.Pattern ptrn, long[] streamPosition)
            {
                if (ptrn == null)
                {
                    throw new ArgumentNullException("Pattern.");
                }
                this.Ptrn = ptrn;
                this.StreamPosition = streamPosition;
            }
        }

        public class Pattern
        {
            public struct UniversalByte
            {
                public enum Action
                {
                    Skip,
                    Normal
                }

                public Patcher.Pattern.UniversalByte.Action Act;

                public byte B;
            }

            public readonly Patcher.Pattern.UniversalByte[] ReplaceBytes;

            public readonly Patcher.Pattern.UniversalByte[] SearchBytes;

            private readonly bool _valid;

            public uint BrakAfter;

            public uint ReplaceAfter;

            public bool ValidPattern
            {
                get
                {
                    return (this.BrakAfter <= 0u || this.BrakAfter - this.ReplaceAfter >= 1u) && this._valid;
                }
            }

            private bool replaceImmediately;

            public bool ReplaceImmediately
            {
                [CompilerGenerated]
                get
                {
                    return this.replaceImmediately;
                }
            }

            public Pattern(string se, string rep, uint brakAfter = 1u, uint replaceAfter = 0u)
            {
                if (string.IsNullOrEmpty(se))
                {
                    throw new ArgumentException("Input string null or empty.");
                }
                this.BrakAfter = brakAfter;
                this.ReplaceAfter = replaceAfter;
                if (rep == null)
                {
                    this.SearchBytes = this.TryParse(se);
                    this.replaceImmediately = false;
                    this._valid = true;
                    return;
                }
                this.SearchBytes = this.TryParse(se);
                this.ReplaceBytes = this.TryParse(rep);
                this.replaceImmediately = true;
                this._valid = true;
            }

            private Patcher.Pattern.UniversalByte[] TryParse(string st)
            {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < st.Length; i++)
                {
                    char c = st[i];
                    if (c > ' ' && c < '\u007f')
                    {
                        stringBuilder.Append(c);
                    }
                }
                string text = stringBuilder.ToString().ToUpper();
                if (text.Length % 2 != 0)
                {
                    throw new Exception("String of byte must be power of two.");
                }
                bool flag = false;
                Patcher.Pattern.UniversalByte[] array = new Patcher.Pattern.UniversalByte[text.Length / 2];
                int num = 0;
                for (int j = 0; j < text.Length; j += 2)
                {
                    string text2 = text.Substring(j, 2);
                    if (text2 == "??")
                    {
                        array[num].Act = Patcher.Pattern.UniversalByte.Action.Skip;
                    }
                    else
                    {
                        array[num].Act = Patcher.Pattern.UniversalByte.Action.Normal;
                        array[num].B = byte.Parse(text2, NumberStyles.HexNumber);
                        flag = true;
                    }
                    num++;
                }
                if (!flag)
                {
                    throw new ArgumentNullException("Can't add the string of byte (Does not make sense).");
                }
                return array;
            }
        }

        [CompilerGenerated]
        [Serializable]
        private sealed class PatcherData
        {
            public static readonly Patcher.PatcherData Instance = new Patcher.PatcherData();

            public static Predicate<Patcher.Pattern> PatcherDataPredicate;

            internal bool Patch(Patcher.Pattern item)
            {
                return item == null;
            }
        }

        private int _bufferSize = 2048;

        public string FileName;

        private FileStream _fs;

        private bool _isDisposed;

        private string[] _succesMessages = new string[]
        {
            "Pattern not found!!",
            "Patched!!",
            "Patched but result is other!!",
            "Found!!",
            "Found but result is other!!"
        };

        public Patcher.PatcherState CurrentState
        {
            get;
            private set;
        }

        public string[] SuccesMessages
        {
            get
            {
                return this._succesMessages;
            }
            set
            {
                if (value != null && value.Length == 5)
                {
                    this._succesMessages = value;
                }
            }
        }

        public int BufferSize
        {
            get
            {
                return this._bufferSize;
            }
            set
            {
                this._bufferSize = ((value < 16) ? 16 : value);
            }
        }

        private List<Patcher.Pattern> patterns;

        public List<Patcher.Pattern> Patterns
        {
            [CompilerGenerated]
            get
            {
                return this.patterns;
            }
        }

        public Patcher()
        {
            this.patterns = new List<Patcher.Pattern>();
        }

        public Patcher(string filename)
        {
            this.patterns = new List<Patcher.Pattern>();
            this.FileName = filename;
        }

        public Patcher(string filename, List<Patcher.Pattern> patterns)
        {
            if (this.Patterns == null)
            {
                throw new ArgumentNullException("patterns.");
            }
            if (this.Patterns.Count == 0)
            {
                throw new ArgumentOutOfRangeException("Collection is empty: patterns.");
            }
            this.patterns = patterns;
            this.FileName = filename;
        }

        public bool AddString(string se, string rep, uint brakAfter = 1u, uint replaceAfter = 0u)
        {
            try
            {
                this.Patterns.Add(new Patcher.Pattern(se, rep, brakAfter, replaceAfter));
            }
            catch (Exception ex)
            {
                NLogger.Warn(string.Format("AddString: {0}: {1}", ex.GetType(), ex.Message));
                return false;
            }
            return true;
        }

        public bool Patch()
        {
            if (this.Patterns == null)
            {
                NLogger.Error("ArgumentNullException: patterns.");
                return false;
            }
            List<Patcher.Pattern> arg_39_0 = this.Patterns;
            Predicate<Patcher.Pattern> arg_39_1;
            arg_39_1 = Patcher.PatcherData.PatcherDataPredicate;
            if (arg_39_1 == null)
            {
                Patcher.PatcherData.PatcherDataPredicate = new Predicate<Patcher.Pattern>(Patcher.PatcherData.Instance.Patch);
                arg_39_1 = Patcher.PatcherData.PatcherDataPredicate;
            }
            arg_39_0.RemoveAll(arg_39_1);
            if (this.Patterns.Count == 0)
            {
                NLogger.Error("Collection is empty: patterns.");
                return false;
            }
            uint num = 0u;
            uint num2 = 0u;
            bool flag = false;
            try
            {
                this._fs = this.CreateStream(this.FileName);
                foreach (Patcher.Pattern current in this.Patterns)
                {
                    Patcher.MainPattern pt = this.FindAllPatterns(current);
                    if (pt.SuccessfullyFound > 0)
                    {
                        if (this.ReplaceAllPatterns(pt))
                        {
                            flag = true;
                        }
                        if (pt.Success)
                        {
                            num2 += 1u;
                        }
                        else
                        {
                            num2 += 1u;
                            num += 1u;
                        }
                    }
                    else
                    {
                        num += 1u;
                    }
                }
            }
            catch (Exception ex)
            {
                if (this._fs != null)
                {
                    this._fs.Dispose();
                    this._fs = null;
                }
                NLogger.Error(string.Format("{0}: {1}", ex.GetType(), ex.Message));
                return false;
            }
            if (this._fs != null)
            {
                this._fs.Close();
                this._fs = null;
            }
            if (flag)
            {
                if (num > 0u && num2 > 0u)
                {
                    NLogger.Debug(this._succesMessages[2]);
                    this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[2]);
                    return true;
                }
                if (num == 0u && num2 > 0u)
                {
                    NLogger.Debug(this._succesMessages[1]);
                    this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[1]);
                    return true;
                }
                this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[0]);
                NLogger.Debug(this._succesMessages[0]);
            }
            else
            {
                if (num > 0u && num2 > 0u)
                {
                    this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[4]);
                    NLogger.Debug(this._succesMessages[4]);
                    return true;
                }
                if (num == 0u && num2 > 0u)
                {
                    this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[3]);
                    NLogger.Debug(this._succesMessages[3]);
                    return true;
                }
                this.CurrentState = new Patcher.PatcherState(flag, num2, num, this._succesMessages[0]);
                NLogger.Debug(this._succesMessages[0]);
            }
            return false;
        }

        private Patcher.MainPattern FindAllPatterns(Patcher.Pattern pt)
        {
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("Object was disposed.");
            }
            if (pt == null)
            {
                throw new ArgumentNullException("Pattern");
            }
            if (!pt.ValidPattern)
            {
                return new Patcher.MainPattern(pt, null);
            }
            long num = 0L;
            int num2 = 0;
            int num3 = 0;
            uint num4 = 0u;
            uint num5 = 0u;
            byte[] array = new byte[this._bufferSize];
            int i = this._bufferSize;
            Patcher.Pattern.UniversalByte[] searchBytes = pt.SearchBytes;
            List<long> list = new List<long>();
            if (this._isDisposed)
            {
                throw new ObjectDisposedException("Object was disposed.");
            }
            this._fs.Position = 0L;
            while (i > 0)
            {
                if (this._isDisposed)
                {
                    throw new ObjectDisposedException("Object was disposed.");
                }
                long position = this._fs.Position;
                i = this._fs.Read(array, 0, array.Length);
                for (int j = 0; j < i; j++)
                {
                    if (searchBytes[num3].Act == Patcher.Pattern.UniversalByte.Action.Skip || array[j] == searchBytes[num3].B)
                    {
                        if (num3 == 0)
                        {
                            num = position;
                            num2 = j;
                        }
                        if (searchBytes.Length - 1 == num3)
                        {
                            num3 = 0;
                            if (num4 == pt.ReplaceAfter)
                            {
                                num5 += 1u;
                                list.Add(num + (long)num2);
                                if (pt.BrakAfter > 0u && num5 == pt.BrakAfter)
                                {
                                    return new Patcher.MainPattern(pt, list.ToArray());
                                }
                            }
                            else
                            {
                                num4 += 1u;
                            }
                        }
                        else
                        {
                            num3++;
                        }
                    }
                    else if (num3 > 0)
                    {
                        num3 = 0;
                        j = num2;
                        if (num != position)
                        {
                            this._fs.Position = num;
                            i = this._fs.Read(array, 0, array.Length);
                        }
                    }
                }
            }
            if (num5 <= 0u)
            {
                return new Patcher.MainPattern(pt, null);
            }
            return new Patcher.MainPattern(pt, list.ToArray());
        }

        private bool ReplaceAllPatterns(Patcher.MainPattern pt)
        {
            if (!pt.Ptrn.ReplaceImmediately || this._isDisposed)
            {
                return false;
            }
            int num = 0;
            long[] arg_3E_0 = pt.StreamPosition;
            Patcher.Pattern.UniversalByte[] replaceBytes = pt.Ptrn.ReplaceBytes;
            byte[] array = new byte[pt.Ptrn.SearchBytes.Length];
            long[] array2 = arg_3E_0;
            for (int i = 0; i < array2.Length; i++)
            {
                long position = array2[i];
                if (this._isDisposed)
                {
                    return false;
                }
                bool flag = false;
                this._fs.Position = position;
                this._fs.Read(array, 0, array.Length);
                for (int j = 0; j < array.Length; j++)
                {
                    if (replaceBytes[j].Act > Patcher.Pattern.UniversalByte.Action.Skip && array[j] != replaceBytes[j].B)
                    {
                        array[j] = replaceBytes[j].B;
                        flag = true;
                    }
                }
                if (flag)
                {
                    this._fs.Position = position;
                    this._fs.Write(array, 0, array.Length);
                    num++;
                }
            }
            return true;
        }

        private FileStream CreateStream(string fileName)
        {
            FileAttributes fileAttributes = File.GetAttributes(fileName);
            fileAttributes = FileAttributes.Normal;
            File.SetAttributes(fileName, fileAttributes);
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite);
            if (fileStream.Length < 16L)
            {
                fileStream.Close();
                throw new IOException("File is too short! (posible damaged).");
            }
            return fileStream;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._isDisposed)
            {
                return;
            }
            if (disposing && this._fs != null)
            {
                this._fs.Dispose();
            }
            this._fs = null;
            this._isDisposed = true;
        }
    }
}
