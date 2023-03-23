using Newtonsoft.Json;
using System;
using System.IO;
using static LuckDraw.Language;

namespace LuckDraw
{
    internal class Settings
    {
        public int number;
        public Languages lang;

        public Settings(int num)
        {
            number = num;
            lang = Languages.Chinese;
        }

        public static void Save(Settings settings)
        {
            string json = JsonConvert.SerializeObject(settings);
            if (!Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LuckDraw"))
            {
                Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LuckDraw");
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\LuckDraw/Settings.json", json);
        }

        public static void Load()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"/LuckDraw/Settings.json";
            if (!File.Exists(path))
                return;
            string json = File.ReadAllText(path);
#pragma warning disable CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
#pragma warning restore CS8600 // 将 null 字面量或可能为 null 的值转换为非 null 类型。
            if (settings is null)
            {
                App.settings = new(55);
                return;
            }
            App.settings = settings;
        }
    }
}
