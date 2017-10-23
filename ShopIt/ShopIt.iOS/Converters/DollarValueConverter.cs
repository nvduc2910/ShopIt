using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace ShopIt.iOS.Converters
{
	public class DollarValueConverter : MvxValueConverter<decimal?, string>
	{
		protected override string Convert(decimal? value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value.HasValue)
			{
				int count = 0;
				var valueStr = value.ToString().Replace(",", string.Empty);
				string temp = valueStr;
				for (int i = valueStr.Length; i > 0; i--)
				{
					count++;
					if (count == 3 && i != 1)
					{
						temp = temp.Insert(i - 1, ",");
						count = 0;
					}
				}
				return temp;
			}
			return "0";
		}
	}
}
