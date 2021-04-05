using System;
using System.IO;
using System.Net;

namespace LuckDrawLauncher
{
    class Program
    {
        static void Main()
        {
            string current = Directory.GetCurrentDirectory();
            DownloadSource();
            Console.WriteLine("下载完毕！");

            try
            {
                Directory.Delete(current + @"\LuckDraw-master", true);
                Directory.Delete(current + @"\LuckDraw", true);
                Directory.Delete(current + @"\Shared", true);
            }
            catch { }
            System.IO.Compression.ZipFile.ExtractToDirectory(current + @"\LuckDraw.zip", current + @"");
            Directory.CreateDirectory(current + @"\LuckDraw");
            Directory.Move(current + @"\LuckDraw-master\Desktop\LuckDrawWPF", current + @"\LuckDraw\LuckDraw");
            Directory.Move(current + @"\LuckDraw-master\Shared", current + @"\Shared");
            Directory.Delete(current + @"\LuckDraw-master", true);
            File.Delete(current + @"\LuckDraw.zip");
            Console.WriteLine("解压完毕！");

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("cd LuckDraw");
            p.StandardInput.WriteLine("cd LuckDraw");
            p.StandardInput.WriteLine("start run.cmd");

            Console.ReadKey();
        }
        public static void DownloadSource()
        {
            FileStream FStream;
            FStream = new FileStream(Directory.GetCurrentDirectory() + "\\LuckDraw.zip", FileMode.Create);
            try
            {
                //打开网络连接
                HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://archive.fastgit.org/laipuran/LuckDraw/archive/refs/heads/master.zip");
                //向服务器请求,获得服务器的回应数据流
                Stream myStream = myRequest.GetResponse().GetResponseStream();
                //定义一个字节数据
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
                Console.WriteLine(Ex.Message);
            }
        }
    }
}
