using MessagingToolkit.Barcode;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WindowsClient.UI
{
	public partial class CardForPrint : Window
	{
		private const long _minBarcodeValue = 0;
		private const long _maxBarcodeValue = 9999999999999;
		private const int _barcodeLength = 13;
		private const string _backgroundLocation = "UI\\KirkensKorshaer.png";
		private const BarcodeFormat _barcodeFormat = BarcodeFormat.EAN13;

		private static PointF _barcodeLocation = new PointF(100, 100);
		private static PointF _barcodeTextLocation = new PointF(160, 203);
		private static Font _font = new Font("Verdana", 12);

		public CardForPrint(long contentLong)
		{
			InitializeComponent();

			try
			{
				string barcodeContent = LongToBarcodeContent(contentLong);
				DrawBarCode(barcodeContent);
			}
			catch (Exception)
			{

			}
		}

		private string LongToBarcodeContent(long contentLong)
		{
			if (contentLong < _minBarcodeValue || contentLong > _maxBarcodeValue)
			{
				throw new ArgumentOutOfRangeException($"barcode content must be in range [{_minBarcodeValue} - {_maxBarcodeValue}]");
			}

			string convertedContent = contentLong.ToString();

			convertedContent = convertedContent.PadLeft(_barcodeLength, '0');

			return convertedContent;
		}

		private void DrawBarCode(string barcodeContent)
		{
			Bitmap backgroundBitmap = new Bitmap(_backgroundLocation);

			BarcodeEncoder barcodeEncoder = new BarcodeEncoder();
			WriteableBitmap barcodeBitmap = barcodeEncoder.Encode(_barcodeFormat, barcodeContent);

			Graphics graphics = Graphics.FromImage(backgroundBitmap);

			graphics.DrawImage(BitmapFromWriteableBitmap(barcodeBitmap), _barcodeLocation);
			graphics.DrawString(barcodeContent, _font, Brushes.Black, _barcodeTextLocation);

			graphics.Dispose();

			BitmapImage bitmapImage = BitmapImageFromBitmap(backgroundBitmap);

			cardImage.Source = bitmapImage;
		}

		private BitmapImage BitmapImageFromBitmap(Bitmap backgroundBitmap)
		{
			MemoryStream memoryStream = new MemoryStream();
			backgroundBitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);

			BitmapImage bitmapImage = new BitmapImage();
			bitmapImage.BeginInit();
			bitmapImage.StreamSource = new MemoryStream(memoryStream.ToArray());
			bitmapImage.EndInit();
			return bitmapImage;
		}

		private Bitmap BitmapFromWriteableBitmap(WriteableBitmap writeableBitmap)
		{
			Bitmap createdBitmap;
			using (MemoryStream outStream = new MemoryStream())
			{
				BitmapEncoder bitmapEncoder = new BmpBitmapEncoder();
				bitmapEncoder.Frames.Add(BitmapFrame.Create(writeableBitmap));
				bitmapEncoder.Save(outStream);
				createdBitmap = new Bitmap(outStream);
			}
			return createdBitmap;
		}
	}
}
