using System;

namespace Scanner
{
	public abstract class AbstractScannerConnection : EventArgs
	{
		public virtual string Name { get; protected set; }

		public event EventHandler CodeScanned;
		public event EventHandler NameChanged;
		public event EventHandler Disconnect;

		public override string ToString()
		{
			return $"{Name}";
		}

		protected void OnStringEvent(StringEventArgs stringEventArgs, EventHandler handler)
		{
			if (handler != null)
			{
				handler(this, stringEventArgs);
			}
		}

		protected void OnDisconnect(EventArgs eventArgs)
		{
			EventHandler handler = Disconnect;
			if (handler != null)
			{
				handler(this, eventArgs);
			}
		}

		protected void RetreiveDataFromScanner(string key, string value)
		{
			StringEventArgs args = new StringEventArgs()
			{
				Data = value,
			};

			switch (key)
			{
				case "Code":
					OnStringEvent(args, CodeScanned);
					break;
				case "Name":
					Name = value;
					OnStringEvent(args, NameChanged);
					break;
				default:
					break;
			}
		}
	}
}
