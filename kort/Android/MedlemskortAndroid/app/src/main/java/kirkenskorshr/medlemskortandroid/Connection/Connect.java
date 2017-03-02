package kirkenskorshr.medlemskortandroid.Connection;

import android.os.AsyncTask;
import android.util.Log;

import java.net.Socket;

public class Connect extends AsyncTask<Connection, Void, Socket>
{
	protected Socket doInBackground(Connection... connections)
	{
		Socket socket = null;
		try
		{
			try
			{
				socket = new Socket(connections[0].Ip, connections[0].Port);

				return socket;
			}
			catch (Exception e)
			{
				Log.e("ClientActivity", "S: Error", e);
			}
		}
		catch (Exception e)
		{
			Log.e("ClientActivity", "C: Error", e);
		}

		return socket;
	}
}
