using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace Converters
{
	public class TrueFalseConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, CultureInfo culture)
		{
			return !value;
		}
	}
}
