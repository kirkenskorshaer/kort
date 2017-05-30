using System;
using System.Collections.ObjectModel;
using System.Windows;
using Scanner;
using WindowsClient.CardServer;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Controls;
using System.ComponentModel;
using System.Windows.Data;

namespace WindowsClient
{
	public partial class MainWindow : Window
	{
		private ScannerConnectionFinder _scannerConnectionFinder;
		private CardServerClient _client;

		private ObservableCollection<AbstractScannerConnection> _scannerConnections = new ObservableCollection<AbstractScannerConnection>();
		private ObservableCollection<VisitTimeAlert> _alerts = new ObservableCollection<VisitTimeAlert>();
		private ObservableCollection<Member> _membersToAlert = new ObservableCollection<Member>();
		private ObservableCollection<Service> _services = new ObservableCollection<Service>();
		private ObservableCollection<Visit> _visits = new ObservableCollection<Visit>();

		private Member _currentMember;

		private object _codeLock = new object();
		private bool _isCodeUpdate = false;

		public MainWindow()
		{
			InitializeComponent();

			scannerListView.ItemsSource = _scannerConnections;

			alertListView.ItemsSource = _alerts;
			serviceListView.ItemsSource = _services;
			visitListView.ItemsSource = _visits;
			alertAllListView.ItemsSource = _membersToAlert;

			_scannerConnectionFinder = new ScannerConnectionFinder();
			_scannerConnectionFinder.ScannerConnected += ScannerConnected;

			_client = new CardServerClient();

			string ips = GetAllLocalIPv4(NetworkInterfaceType.Ethernet).Aggregate((allIps, currentIp) => allIps + " , " + currentIp);
			Dispatcher.Invoke(() => ipLabel.Content = ips);

			try
			{
				List<Member> membersToAlert = _client.GetMembersToAlert();
				Dispatcher.Invoke(() => membersToAlert.ForEach(member => _membersToAlert.Add(member)));
			}
			catch (Exception)
			{
			}
		}

		public static string[] GetAllLocalIPv4(NetworkInterfaceType networkInterfaceType)
		{
			List<string> ipAddrList = new List<string>();
			foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
			{
				if (item.NetworkInterfaceType == networkInterfaceType && item.OperationalStatus == OperationalStatus.Up)
				{
					foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
					{
						if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
						{
							ipAddrList.Add(ip.Address.ToString());
						}
					}
				}
			}
			return ipAddrList.ToArray();
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

			Dispatcher.Invoke(() => scannerIdLabel.Content = code);

			ClearAll();

			ReadOrCreateMember(code);

			AddVisit();
		}

		private void ClearAll()
		{
			Dispatcher.Invoke(() => _alerts.Clear());
			Dispatcher.Invoke(() => _services.Clear());
			Dispatcher.Invoke(() => _visits.Clear());

			lock (_codeLock)
			{
				_isCodeUpdate = true;
				Dispatcher.Invoke(() => nameTextBox.Text = "");
				_isCodeUpdate = false;
			}
		}

		private void AddVisit()
		{
			if (_currentMember == null)
			{
				return;
			}

			Visit visit = new Visit()
			{
				MemberId = _currentMember.Id,
				VisitTime = DateTime.Now,
			};

			_client.InsertVisit(visit);

			Dispatcher.Invoke(() => _visits.Insert(0, visit));
		}

		private void ReadOrCreateMember(string code)
		{
			List<Member> members = _client.GetMembersByCardId(code);

			_currentMember = members.SingleOrDefault();

			if (_currentMember == null)
			{
				_currentMember = new Member()
				{
					CardId = code,
				};

				_client.InsertMember(_currentMember);

				return;
			}

			ReadMemberRelatedInformationFromServer();
		}

		private void ReadMemberRelatedInformationFromServer()
		{
			if (_currentMember == null)
			{
				return;
			}

			List<VisitTimeAlert> alerts = _client.GetAlertsOnMember(_currentMember.Id);
			List<Service> services = _client.GetServicesOnMember(_currentMember.Id);
			List<Visit> visits = _client.GetVisitsOnMember(_currentMember.Id);

			Dispatcher.Invoke(() => alerts.ForEach(alert => _alerts.Add(alert)));
			Dispatcher.Invoke(() => services.ForEach(service => _services.Add(service)));
			Dispatcher.Invoke(() => visits.OrderByDescending(visit => visit.VisitTime).ToList().ForEach(visit => _visits.Add(visit)));

			lock (_codeLock)
			{
				_isCodeUpdate = true;
				Dispatcher.Invoke(() => nameTextBox.Text = _currentMember.NickName);
				_isCodeUpdate = false;
			}
		}

		private void AddService(object sender, RoutedEventArgs e)
		{
			if (_currentMember == null)
			{
				return;
			}

			Service service = new Service()
			{
				MemberId = _currentMember.Id,
				MaxServiceUses = 10,
				Name = "10 Frokost",
				ServiceUses = new List<ServiceUse>(),
			};

			_client.InsertService(service);

			Dispatcher.Invoke(() => _services.Add(service));
		}

		private void AddAlert(object sender, RoutedEventArgs e)
		{
			if (_currentMember == null)
			{
				return;
			}

			VisitTimeAlert alert = new VisitTimeAlert()
			{
				Member = _currentMember,
				TimeFromLatestVisitBeforeAlert = TimeSpan.FromDays(5),
			};

			_client.InsertAlert(alert);

			Dispatcher.Invoke(() => _alerts.Add(alert));
		}

		private void NickNameTextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
		{
			if (_currentMember == null)
			{
				return;
			}

			if (_isCodeUpdate)
			{
				return;
			}

			_currentMember.NickName = nameTextBox.Text;

			_client.UpdateMember(_currentMember);
		}

		private void UseService(object sender, MouseButtonEventArgs e)
		{
			if (_currentMember == null)
			{
				return;
			}

			Service service = ((ListViewItem)sender).Content as Service;

			if (service == null)
			{
				return;
			}

			ServiceUse serviceUse = new ServiceUse()
			{
				UsedTime = DateTime.Now,
			};

			service.ServiceUses.Add(serviceUse);

			_client.UpdateService(service);

			ICollectionView view = CollectionViewSource.GetDefaultView(_services);
			view.Refresh();
		}
	}
}
