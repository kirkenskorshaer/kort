package kirkenskorshr.medlemskortandroid.Connection;

import android.os.AsyncTask;
import android.util.Log;
import java.io.IOException;
import java.net.Socket;

public class Disconnect extends AsyncTask<Socket, Void, Boolean>
{
		protected Boolean doInBackground(Socket... sockets)
		{
			try
			{
				sockets[0].close();

				return true;
			}
			catch (IOException exception)
			{
				Log.e("error", exception.getMessage());
				return false;
			}
		}
}
