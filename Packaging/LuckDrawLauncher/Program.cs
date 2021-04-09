using System;
using System.Net;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;

namespace LuckDrawLauncher
{

    class Program
    {
        static void Main()
        {
            string current = Directory.GetCurrentDirectory();
            Console.WriteLine("当前位置：" + current);

            string url = "https://api.github.com/repos/laipuran/LuckDraw/commits/master";
            string sha = GetCommit(url);

            FileStream stream = new FileStream(current + @"\sha", FileMode.OpenOrCreate);
            int Length = (int)stream.Length;
            byte[] Byte = new byte[Length];
            int r = stream.Read(Byte, 0, Byte.Length);
            string oldsha = System.Text.Encoding.UTF8.GetString(Byte);
            stream.Close();
            Console.WriteLine("最新版本的sha是：" + sha);

            if (oldsha != sha)
            {
                Console.WriteLine("老版本的sha为：" + oldsha);
                Console.WriteLine("正在下载新版本……");
                DownloadSource();
                Console.WriteLine("下载运行完毕！");
                try
                {
                    Directory.Delete(current + @"\LuckDraw-master", true);
                    Directory.Delete(current + @"\LuckDraw", true);
                    Directory.Delete(current + @"\Shared", true);
                    Console.WriteLine("删除旧版本完毕！");
                }
                catch (Exception Ex)
                {
#if DEBUG
                    Console.WriteLine(Ex.Message);
#endif
                }

                System.IO.Compression.ZipFile.ExtractToDirectory(current + @"\LuckDraw.zip", current + @"");
                Console.WriteLine("解压完毕！");

                Directory.CreateDirectory(current + @"\LuckDraw");
                Directory.Move(current + @"\LuckDraw-master\Desktop\LuckDrawWPF", current + @"\LuckDraw\LuckDraw");
                Directory.Move(current + @"\LuckDraw-master\Shared", current + @"\Shared");
                Directory.Delete(current + @"\LuckDraw-master", true);
                Console.WriteLine("配置源代码完毕！");

                System.IO.File.Delete(current + @"\LuckDraw.zip");
                System.IO.File.Delete(current + @"\sha");
                Console.WriteLine("删除临时数据完毕！");

                stream = new FileStream(current + @"\sha", FileMode.Create);
                Length = (int)stream.Length;
                byte[] byteArray = System.Text.Encoding.Default.GetBytes(sha);
                stream.Write(Byte, 0, Byte.Length);
                stream.Close();
                Console.WriteLine("写版本文件完毕！");
            }
            else
            {
                Console.WriteLine("您使用的是最新版本，无需更新！");
            }

            Process p = new Process();
            p.StartInfo.FileName = "powershell.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.StandardInput.AutoFlush = true;

            p.StandardInput.WriteLine("cd LuckDraw");
            p.StandardInput.WriteLine("cd LuckDraw");
            p.StandardInput.WriteLine("dotnet run");
            Console.WriteLine("运行命令执行完毕！");

            Console.ReadKey();
        }
        public static string GetCommit(string url)
        {
            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.UserAgent = "Anything";
            webRequest.ServicePoint.Expect100Continue = false;

            StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
            string reader = responseReader.ReadToEnd();
            Root root = JsonConvert.DeserializeObject<Root>(reader);
            return root.sha;
        }
        public static void DownloadSource()
        {
            FileStream FStream;
            FStream = new FileStream(Directory.GetCurrentDirectory() + "\\LuckDraw.zip", FileMode.Create);
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://archive.fastgit.org/laipuran/LuckDraw/archive/refs/heads/master.zip");
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                byte[] btContent = new byte[512];
                int intSize = 0;
                intSize = myStream.Read(btContent, 0, 512);
                while (intSize > 0)
                {
                    FStream.Write(btContent, 0, intSize);
                    intSize = myStream.Read(btContent, 0, 512);
                }
                //关闭流
                FStream.Close();
                myStream.Close();
            }
            catch (Exception Ex)
            {
                FStream.Close();
#if DEBUG
                Console.WriteLine(Ex.Message);
#endif
            }
        }
    }
}
