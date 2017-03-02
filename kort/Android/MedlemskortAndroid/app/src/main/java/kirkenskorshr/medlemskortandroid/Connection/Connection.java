package kirkenskorshr.medlemskortandroid.Connection;

import android.os.AsyncTask;
import android.util.Log;
import java.net.Socket;

public class Connection
{
	public String Ip;
	public int Port;

	private AsyncTask<Connection, Void, Socket> _connectTask;

	private Socket _socket;

	public Connection(String ip, int port)
	{
		Ip = ip;
		Port = port;
	}

	public void connect()
	{
		Connect connect = new Connect();
		_connectTask = connect.execute(this);
	}

	public void disconnect()
	{
		try
		{
			Disconnect disconnect = new Disconnect();
			disconnect.execute(_socket);
		}
		catch (Exception exception)
		{
			Log.e("error", exception.getMessage());
		}
	}

	public Boolean send(String message)
	{
		if (readySocket() == false)
		{
			return false;
		}

		Send send = new Send(_socket);
		send.execute(message);

		return true;
	}

	private Boolean readySocket()
	{
		try
		{
			if (_socket != null)
			{
				if (_socket.isConnected())
				{
					return true;
				}

				_socket.close();
				_socket = null;
			}

			if (_connectTask != null)
			{
				_socket = _connectTask.get();
				_connectTask = null;
				return true;
			}

			return false;
		}
		catch (Exception exception)
		{
			Log.e("error", exception.getMessage());
			return false;
		}
	}
}
