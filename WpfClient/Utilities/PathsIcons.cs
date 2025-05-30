﻿using WpfClient.Utilities.Enum;

namespace WpfClient.Utilities
{
    public static class PathsIcons
    {
        public const string DEFAULT_CAR = "pack://application:,,,/Resources/Images/default_car.jpg";
        private static readonly Dictionary<ConfirmationIconType, string> IconPaths = new()
        {
            { ConfirmationIconType.WarningIcon, Warning},
            { ConfirmationIconType.AlertIcon, Alert},
            { ConfirmationIconType.SirenIcon, SirenIcon },
            { ConfirmationIconType.RegisterIcon, Register },
        };
        private static readonly Dictionary<AlertIconType, string> AlertIconPaths = new()
        {
            { AlertIconType.WarningIcon, Warning},
            { AlertIconType.AlertIcon, Alert},
            { AlertIconType.SirenIcon, SirenIcon },
        };

        private const string Warning = "pack://application:,,,/Resources/Images/Red_triangle_alert_icon.png";
        private const string Alert = "pack://application:,,,/Resources/Images/Red_triangle_alert_icon.png";
        private const string SirenIcon = "pack://application:,,,/Resources/Images/red_siren_icon.png";
        private const string Register = "pack://application:,,,/Resources/Images/register_icon.png";

        public static string GetIconPath(ConfirmationIconType iconType)
        {
            return IconPaths.TryGetValue(iconType, out var path) ? path : "pack://application:,,,/Resources/Images/default.png";
        }
        public static string GetIconPath(AlertIconType iconType)
        {
            return AlertIconPaths.TryGetValue(iconType, out var path) ? path : "pack://application:,,,/Resources/Images/default.png";
        }
    }
}
