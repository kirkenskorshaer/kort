using System;
using System.ComponentModel;
using System.IO;
using System.Net.Sockets;

namespace Scanner
{
	public class AndroidConnection : AbstractScannerConnection
	{
		private Socket _socket;

		private BackgroundWorker _dataReader = new BackgroundWorker();

		public AndroidConnection(Socket socket)
		{
			_socket = socket;

			_dataReader.DoWork += Read;
			_dataReader.RunWorkerCompleted += ReadEnd;

			_dataReader.RunWorkerAsync();
		}

		private void Read(object sender, DoWorkEventArgs e)
		{
			NetworkStream networkStream = new NetworkStream(_socket);

			StreamReader reader = new StreamReader(networkStream);

			string read = "";
			int nextChar = 0;
			while (_socket.Connected && nextChar != -1)
			{
				nextChar = reader.Read();

				if (nextChar == '§')
				{
					string[] keyValue = read.Split(':');

					if (keyValue.Length == 2)
					{
						string key = keyValue[0];
						string value = keyValue[1];

						RetreiveDataFromScanner(key, value);
					}

					read = "";
				}
				else
				{
					read += (char)nextChar;
				}
			}
		}

        private void ReadEnd(object sender, RunWorkerCompletedEventArgs e)
		{
			OnDisconnect(new EventArgs());
		}
	}
}
