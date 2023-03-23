using System;
using System.Windows;

namespace LuckDraw
{
    internal class Language
    {
        public enum Languages
        {
            Chinese,
            English
        }

        public static void SetChinese()
        {
            App.settings.lang = Languages.Chinese;
            Application.Current.Resources.MergedDictionaries.Add(GetDictionary(Languages.Chinese));
            Application.Current.Resources.MergedDictionaries.Remove(GetDictionary(Languages.English));
            App.ContentFrame.Refresh();
        }

        public static void SetEnglish()
        {
            App.settings.lang = Languages.English;
            Application.Current.Resources.MergedDictionaries.Add(GetDictionary(Languages.English));
            Application.Current.Resources.MergedDictionaries.Remove(GetDictionary(Languages.Chinese));
            App.ContentFrame.Refresh();
        }

        private static ResourceDictionary GetDictionary(Languages languages)
        {
            ResourceDictionary dictionary = new()
            {
                Source = new(MainWindow.GetString(languages.ToString()), UriKind.Relative)
            };
            return dictionary;
        }
    }
}
