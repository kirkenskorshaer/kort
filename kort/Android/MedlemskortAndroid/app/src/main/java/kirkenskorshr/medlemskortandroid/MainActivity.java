package kirkenskorshr.medlemskortandroid;

import android.app.PendingIntent;
import android.content.Intent;
import android.media.Ringtone;
import android.media.RingtoneManager;
import android.net.Uri;
import android.nfc.NdefMessage;
import android.nfc.NfcAdapter;
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

	private NfcAdapter mAdapter;
	private PendingIntent mPendingIntent;
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

		mAdapter = NfcAdapter.getDefaultAdapter(this);
		mPendingIntent = PendingIntent.getActivity(this, 0, new Intent(this, getClass()).addFlags(Intent.FLAG_ACTIVITY_SINGLE_TOP), 0);
	}

	@Override
	protected void onResume()
	{
		super.onResume();
		if (mAdapter != null)
		{
			mAdapter.enableForegroundDispatch(this, mPendingIntent, null, null);
		}
	}

	@Override
	protected void onPause()
	{
		super.onPause();
		if (mAdapter != null)
		{
			mAdapter.disableForegroundDispatch(this);
		}
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

	@Override
	protected void onNewIntent(Intent intent)
	{
		super.onNewIntent(intent);

		if (intent == null)
		{
			return;
		}

		String action = intent.getAction();

		if (NfcAdapter.ACTION_TAG_DISCOVERED.equals(action) || NfcAdapter.ACTION_TECH_DISCOVERED.equals(action) || NfcAdapter.ACTION_NDEF_DISCOVERED.equals(action))
		{
			Parcelable[] rawMsgs = intent.getParcelableArrayExtra(NfcAdapter.EXTRA_NDEF_MESSAGES);
			NdefMessage[] msgs;
			if (rawMsgs != null)
			{
				msgs = new NdefMessage[rawMsgs.length];
				for (int i = 0; i < rawMsgs.length; i++)
				{
					msgs[i] = (NdefMessage) rawMsgs[i];
					//msgs[i].
				}
			}
			else
			{
				// Unknown tag type
				byte[] id = intent.getByteArrayExtra(NfcAdapter.EXTRA_ID);
				send("Code:" + bytesToString(id));

				makeBeep();
			}
		}
	}

	private void makeBeep()
	{
		try
		{
			Uri notification = RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION);
			Ringtone ringtone = RingtoneManager.getRingtone(getApplicationContext(), notification);
			ringtone.play();
		}
		catch (Exception e)
		{
			e.printStackTrace();
		}
	}

	private String bytesToString(byte[] bytes)
	{
		long result = 0;
		long currentFactor = 1;
		for (byte currentByte : bytes)
		{
			char[] currentByteChars = String.format("%8s", Integer.toBinaryString(currentByte & 0xFF)).replace(' ', '0').toCharArray();

			for (int index = 0; index < 8; index++)
			{
				char bitChar = currentByteChars[index];
				int bit = Integer.parseInt(Character.toString(bitChar));

				result += (currentFactor * bit);
				currentFactor *= 2;
			}
		}

		return Long.toString(result);
	}
}
