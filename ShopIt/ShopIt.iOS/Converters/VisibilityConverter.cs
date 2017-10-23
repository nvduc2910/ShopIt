﻿using System;
using MvvmCross.Platform.Converters;

namespace ShopIt.iOS.Converters
{
	public class VisibilityConverter : MvxValueConverter<bool, bool>
	{
		protected override bool Convert(bool value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return !value;
		}
	}
}
