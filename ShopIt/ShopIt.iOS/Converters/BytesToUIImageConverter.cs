using System;
using MvvmCross.Platform.Converters;
using ShopIt.iOS.Extensions;
using UIKit;

namespace Converters
{
	public class BytesToUIImageConverter : MvxValueConverter<byte[], UIImage>
	{
		protected override UIImage Convert(byte[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			if (value != null)
				return value.ToImage();
			else {
				return null;
			}

		}
	}
}
