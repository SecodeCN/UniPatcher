using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace UniPatcher
{
	public class NLogger
	{
		private class Message
		{
			public enum State
			{
				Debug,
				Information,
				Exclamation,
				Error
			}

			public readonly string message;

			public readonly NLogger.Message.State state;

			public int Level
			{
				get
				{
					return (int)this.state;
				}
			}

			public Message(string ms, short st = 0)
			{
				this.message = ms;
				this.state = (NLogger.Message.State)st;
			}
		}

		private static List<NLogger.Message> _msgs;

		private static short _errorState;

		public static short MaxMessages;

		public static void Clear()
		{
			NLogger._msgs.Clear();
			NLogger._errorState = 0;
		}

		static NLogger()
		{
			NLogger._msgs = new List<NLogger.Message>();
			NLogger.MaxMessages = 8;
			NLogger._errorState = 0;
		}

		public static void LastXMessages()
		{
			if (NLogger._msgs.Count < 1)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			int count = NLogger._msgs.Count;
			if (count > (int)NLogger.MaxMessages)
			{
				for (int i = count - (int)NLogger.MaxMessages; i < count; i++)
				{
					stringBuilder.Append(NLogger._msgs[i].message).AppendLine();
				}
			}
			else
			{
				foreach (NLogger.Message current in NLogger._msgs)
				{
					stringBuilder.Append(current.message).AppendLine();
				}
			}
			NLogger.Publish(new NLogger.Message(stringBuilder.ToString(), NLogger._errorState));
		}

		public static void LastMessage()
		{
			if (NLogger._msgs.Count < 1)
			{
				return;
			}
			NLogger.Publish(NLogger._msgs[NLogger._msgs.Count - 1]);
		}

		public static void WriteLogToFile(string filename)
		{
			if (NLogger._msgs.Count < 1)
			{
				return;
			}
			try
			{
				File.WriteAllText(filename, NLogger.GetFullLog(true));
			}
			catch (Exception ex)
			{
				MessageBox.Show("IOException: Can't write log to file: " + ex.Message, "Patcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
			}
		}

		public static string GetFullLog(bool header = false)
		{
			if (NLogger._msgs.Count < 1)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (header)
			{
				stringBuilder.Append("Patcher log file.").AppendLine();
				stringBuilder.Append("File format: {ErrorLevel} {Message},").AppendLine();
				stringBuilder.Append("0 = Ok,").AppendLine();
				stringBuilder.Append("1 = Information,").AppendLine();
				stringBuilder.Append("2 = Exclamation,").AppendLine();
				stringBuilder.Append("3 = Error").AppendLine();
			}
			foreach (NLogger.Message current in NLogger._msgs)
			{
				stringBuilder.Append(string.Format("{{0}} {{1}},", current.Level, current.message));
			}
			return stringBuilder.ToString();
		}

		private static void Publish(NLogger.Message md)
		{
			if (md == null)
			{
				return;
			}
			switch (md.state)
			{
			case NLogger.Message.State.Debug:
				MessageBox.Show(md.message, "Patcher");
				return;
			case NLogger.Message.State.Information:
				MessageBox.Show(md.message, "Patcher", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
				return;
			case NLogger.Message.State.Exclamation:
				MessageBox.Show(md.message, "Patcher", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				return;
			case NLogger.Message.State.Error:
				MessageBox.Show(md.message, "Patcher", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
				return;
			default:
				return;
			}
		}

		internal static void Debug(string ms)
		{
			NLogger._msgs.Add(new NLogger.Message(ms, 0));
		}

		internal static void Inf(string ms)
		{
			NLogger._msgs.Add(new NLogger.Message(ms, 1));
			if (NLogger._errorState < 1)
			{
				NLogger._errorState = 1;
			}
		}

		internal static void Warn(string ms)
		{
			NLogger._msgs.Add(new NLogger.Message(ms, 2));
			if (NLogger._errorState < 2)
			{
				NLogger._errorState = 2;
			}
		}

		internal static void Error(string ms)
		{
			NLogger._msgs.Add(new NLogger.Message(ms, 3));
			if (NLogger._errorState < 3)
			{
				NLogger._errorState = 3;
			}
		}
	}
}
