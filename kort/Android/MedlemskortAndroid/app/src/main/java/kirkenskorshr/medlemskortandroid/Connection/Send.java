package kirkenskorshr.medlemskortandroid.Connection;

import android.os.AsyncTask;
import android.util.Log;

import java.io.BufferedOutputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.Socket;

public class Send extends AsyncTask<String, Void, Boolean>
{
	private Socket _socket;
	private PrintWriter _printWriter;

	public Send(Socket socket)
	{
		_socket = socket;
		try
		{
			_printWriter = new PrintWriter(new BufferedOutputStream(_socket.getOutputStream()), true);
		}
		catch (IOException exception)
		{
			Log.e("error", exception.getMessage());
		}
	}

	protected Boolean doInBackground(String... messages)
	{
		try
		{
			if (_socket == null || _socket.isClosed())
			{
				return false;
			}

			for (String message : messages)
			{
				_printWriter.print(message + "§");
			}
			_printWriter.flush();

			return true;
		}
		catch (Exception exception)
		{
			Log.e("error", exception.getMessage());
			return false;
		}
	}
}
