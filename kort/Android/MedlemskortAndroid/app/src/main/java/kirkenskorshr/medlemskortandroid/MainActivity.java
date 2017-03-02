package kirkenskorshr.medlemskortandroid;

import android.net.Uri;
import android.os.Parcelable;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;

import java.util.Random;

import kirkenskorshr.medlemskortandroid.Connection.Connection;

public class MainActivity extends AppCompatActivity
{
	private Random _random;
	private Connection _connection;

	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);

		EditText ipText = (EditText) findViewById(R.id.edit_ip);
		EditText portText = (EditText) findViewById(R.id.edit_port);

		//ipText.setText("10.10.11.126");
		ipText.setText("192.168.1.82");
		portText.setText("997");

		_random = new Random();
	}

	public void connect(View view)
	{
		EditText ipText = (EditText) findViewById(R.id.edit_ip);
		EditText portText = (EditText) findViewById(R.id.edit_port);

		String ip = ipText.getText().toString();
		int port = Integer.parseInt(portText.getText().toString());

		_connection = new Connection(ip, port);
		_connection.connect();

		send("Name:" + android.os.Build.MODEL);
	}

	public void send(View view)
	{
		String randomString = GetRandomString();
		send("Code:" + randomString);
	}

	private Boolean send(String message)
	{
		return _connection.send(message);
	}

	private String GetRandomString()
	{
		int length = (Math.abs(_random.nextInt()) % 5) + 2;

		String randomString = "";
		for (int index = 0; index < length; index++)
		{
			int randomInt = (Math.abs(_random.nextInt()) % 57) + 65;
			char randomChar = (char) randomInt;

			randomString += randomChar;
		}

		return randomString;
	}

	public void disconnect(View view)
	{
		_connection.disconnect();
	}
}
