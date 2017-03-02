using System;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace Scanner
{
	public class ScannerConnectionFinder
	{
		public event EventHandler ScannerConnected;

		private int _port = 997;
		private IPAddress _ipaddress = IPAddress.Any;

		private BackgroundWorker _androidConnectionListener = new BackgroundWorker();

		public ScannerConnectionFinder()
		{
			_androidConnectionListener.DoWork += ListenForAndroidConnection;
			_androidConnectionListener.RunWorkerCompleted += ReturnConnection;
			_androidConnectionListener.RunWorkerAsync();
		}

		private void ReturnConnection(object sender, RunWorkerCompletedEventArgs e)
		{
			OnScannerConnected((AbstractScannerConnection)e.Result);
			_androidConnectionListener.RunWorkerAsync();
		}

		private void ListenForAndroidConnection(object sender, DoWorkEventArgs e)
		{
			TcpListener tcpListener = new TcpListener(_ipaddress, _port);
			tcpListener.Start();

			Socket clientSocket = tcpListener.AcceptSocket();

			tcpListener.Stop();

			e.Result = new AndroidConnection(clientSocket);
		}

		private void OnScannerConnected(AbstractScannerConnection scannerConnection)
		{
			EventHandler handler = ScannerConnected;
			if (handler != null)
			{
				handler(this, scannerConnection);
			}
		}
	}
}
