using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfClient.Utilities.Enum;

namespace WpfClient.Utilities
{
    public static class PathsIcons
    {
        private static readonly Dictionary<ConfirmationIconType, string> IconPaths = new()
        {
            { ConfirmationIconType.WarningIcon, Warning},
            { ConfirmationIconType.AlertIcon, Alert},
            { ConfirmationIconType.SirenIcon, SirenIcon },
            { ConfirmationIconType.RegisterIcon, Register },
        };

        private const string Warning = "pack://application:,,,/Resources/Images/Red_triangle_alert_icon.png";
        private const string Alert = "pack://application:,,,/Resources/Images/Red_triangle_alert_icon.png";
        private const string SirenIcon = "pack://application:,,,/Resources/Images/red_siren_icon.png";
        private const string Register = "pack://application:,,,/Resources/Images/register_icon.png";

        public static string GetIconPath(ConfirmationIconType iconType)
        {
            return IconPaths.TryGetValue(iconType, out var path) ? path : "pack://application:,,,/Resources/Images/default.png";
        }
    }
}
