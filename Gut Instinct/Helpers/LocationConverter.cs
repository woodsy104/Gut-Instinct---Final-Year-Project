﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gut_Instinct.Helpers
{
    public class LocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
            {
                return null;
            }

            if (value is not string latLong)
            {
                throw new ArgumentException("Value needs to be a string!");
            }

            return GetLocation(latLong);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public static Location GetLocation(string latLong)
        {
            var (latitude, longitude) = GetLatitudeAndLongitude(latLong);
            return new Location(latitude, longitude);
        }

        public static (double Latitude, double Longitude) GetLatitudeAndLongitude(string latLong)
        {
            if (string.IsNullOrEmpty(latLong))
            {
                throw new ArgumentNullException(nameof(latLong));
            }

            var segments = latLong.Split(',');
            var latitude = double.Parse(segments[0], CultureInfo.InvariantCulture);
            var longitude = double.Parse(segments[1].TrimStart(), CultureInfo.InvariantCulture);
            return (latitude, longitude);
        }
    }
}
