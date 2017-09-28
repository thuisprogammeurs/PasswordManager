using System;
using System.Globalization;
using MvvmCross.Platform.Converters;
using Zzz.Core.Utility;

namespace Zzz.Droid.Converters
{
    public class MenuOptionToIconAssetNameConverter : MvxValueConverter<MenuOption, int>
    {
        protected override int Convert(MenuOption value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case MenuOption.GroupOverview:
                    return Resource.Drawable.search_icon;
                case MenuOption.PasswordGenerator:
                    return Resource.Drawable.password_generator_icon;
                case MenuOption.PasswordOverview:
                    return Resource.Drawable.save_icon;
                case MenuOption.Settings:
                    return Resource.Drawable.settings_icon;
                default:
                    return Resource.Drawable.refresh_icon;
            }
        }
    }
}