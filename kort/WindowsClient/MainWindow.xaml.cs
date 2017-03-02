using System;
using System.Collections.ObjectModel;
using System.Windows;
using Scanner;

namespace WindowsClient
{
	public partial class MainWindow : Window
	{
		private ScannerConnectionFinder _scannerConnectionFinder;

		private ObservableCollection<AbstractScannerConnection> _scannerConnections = new ObservableCollection<AbstractScannerConnection>();

		public MainWindow()
		{
			InitializeComponent();

			scannerListView.ItemsSource = _scannerConnections;

			_scannerConnectionFinder = new ScannerConnectionFinder();
			_scannerConnectionFinder.ScannerConnected += ScannerConnected;
		}

		private void ScannerConnected(object sender, EventArgs scannerEventArgs)
		{
			AbstractScannerConnection scannerConnection = (AbstractScannerConnection)scannerEventArgs;

			scannerConnection.CodeScanned += CodeScanned;
			scannerConnection.Disconnect += Disconnect;

			_scannerConnections.Add(scannerConnection);
		}

		private void Disconnect(object sender, EventArgs e)
		{
			Dispatcher.Invoke(() => _scannerConnections.Remove((AbstractScannerConnection)sender));
		}

		private void CodeScanned(object sender, EventArgs e)
		{
			string code = ((StringEventArgs)e).Data;

			Dispatcher.Invoke(() => idLabel.Content = code);
		}
	}
}
