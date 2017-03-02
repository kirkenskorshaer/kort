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
	protected void onCreate(Bundle savedInstanceState)
	{
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		_random = new Random();
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

}
