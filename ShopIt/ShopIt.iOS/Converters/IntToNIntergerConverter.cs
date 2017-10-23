using System;
using MvvmCross.Platform.Converters;

namespace ShopIt.iOS.Converters
{
	public class IntToNIntergerConverter : MvxValueConverter<int, nint>
	{
		protected override nint Convert(int value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return (nint)value;
		}
	}
}
