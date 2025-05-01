using System.Globalization;
using System.Windows;

namespace WpfClient.Idioms
{
    //It should be on the services layer
    public static class Language
    {   

        public static string GetLocalizedString(TextKeys key)
        {
            return Application.Current.Resources[key.ToString()] as string ?? $"[MISSING:{key}]";
        }

        public static void ChangeLanguage(string langCode)
        {
            const string RESOURCE_FOLDER = "Idioms";
            const string BASE_NAME = "strings";
            const string EXTENSION_FILE = "xaml";
            //string resourcePath = $"{RESOURCE_FOLDER}/{BASE_NAME}.{langCode}.{EXTENSION_FILE}";
            string resourcePath = $"Idioms/strings.{langCode}.xaml";

            var newDict = new ResourceDictionary
            {
                Source = new Uri(resourcePath, UriKind.Relative)
            };

            var existingDict = Application.Current.Resources.MergedDictionaries
                                 .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains($"{RESOURCE_FOLDER}."));

            if (existingDict != null)
            {
                int index = Application.Current.Resources.MergedDictionaries.IndexOf(existingDict);
                Application.Current.Resources.MergedDictionaries[index] = newDict;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newDict);
            }
            CultureInfo culture = new CultureInfo(langCode);
            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
        }


    }
}
