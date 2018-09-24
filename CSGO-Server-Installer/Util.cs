/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Util.cs                                        */
/*  Description:   Make csgo server install easier.               */
/*                                                                */
/*                                                                */
/*  Copyright (C) 2018  Kyle                                      */
/*  2018/09/24 04:43:15                                           */
/*                                                                */
/*  This program is licensed under the MIT License.               */
/*                                                                */
/******************************************************************/

using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;

namespace Kxnrl.CSI
{
    class Util
    {
        public static void SafeDeleteFile(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    // Delete
                    File.Delete(path);
                }
                catch (Exception e)
                {
                    Global.Print("清理 '" + path + "' 失败.");
                    Global.Print("错误: " + e.Message);
                }
            }
        }

        public static void SafeDeleteDir(string path)
        {
            if (Directory.Exists(path))
            {
                try
                {
                    using (Process process = new Process())
                    {
                        process.EnableRaisingEvents = true;
                        process.StartInfo.FileName = "cmd.exe";
                        process.StartInfo.Arguments = "rmdir \"" + path + "\" /S /Q ";
                        process.StartInfo.UseShellExecute = false;
                        process.StartInfo.RedirectStandardError = true;
                        process.StartInfo.RedirectStandardInput = true;
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();
                        process.WaitForExit();
                    }
                }
                catch (Exception e)
                {
                    Global.Print("删除 '" + path + "' 失败.");
                    Global.Print("错误: " + e.Message);
                }
            }
        }

        public static void DeleteScript()
        {
            if (File.Exists(Global.AppPath + "\\start.bat"))
            {
                try
                {
                    File.Delete(Global.AppPath + "\\start.bat");
                }
                catch (Exception e)
                {
                    Global.Print("删除 'start.bat' 失败.");
                    Global.Print("错误: " + e.Message);
                }
            }
        }

        public static void CheckDirorCreate(string path)
        {
            if (!Directory.Exists(path))
            {
                try
                {
                    // 创建
                    Directory.CreateDirectory(path);
                }
                catch (Exception e)
                {
                    Global.Print("创建 '" + path + "' 失败.");
                    Global.Print("错误: " + e.Message);
                }
            }
        }

        public static void DownloadFile(string url, string path)
        {
            int times = 0;

            // 失败重试
            retry1:

            try
            {
                // 失败重试
                times++;

                using (WebClient web = new WebClient())
                {
                    Global.Print("正在下载 " + url + " ...");
                    web.DownloadFile(url, path);
                    Global.Print("下载 '" + Path.GetFileName(path) + "' 完成 ...");
                }
            }
            catch (Exception e)
            {
                Global.Print("下载 '" + Path.GetFileName(path) + "' 失败.");
                Global.Print("错误: " + e.Message);
                if (times < 3) goto retry1;
            }
        }

        public static void DownloadFile(string url, string path, string name)
        {
            int times = 0;

            // 失败重试
            retry2:

            try
            {
                // 计数
                times++;

                Helper.ConsoleRefresh();
                Global.Print("正在下载 " + url + " ...");

                HttpWebRequest web = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)web.GetResponse();

                using (Stream stream = response.GetResponseStream())
                {
                    using (FileStream fs = new FileStream(path, FileMode.Create))
                    {
                        long totalDownloadedByte = 0;
                        byte[] bytes = new byte[2048];
                        int osize = stream.Read(bytes, 0, bytes.Length);

                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            fs.Write(bytes, 0, osize);
                            osize = stream.Read(bytes, 0, bytes.Length);

                            if (totalDownloadedByte < response.ContentLength)
                            {
                                // Tracking...
                                Console.WriteLine("[{0}%]  downloading '{1}' ({2}/{3})...", totalDownloadedByte * 100 / response.ContentLength, name, totalDownloadedByte, response.ContentLength);
                            }
                        }

                        Helper.ConsoleRefresh();
                        Global.Print("下载 '" + name + "' 完成   (大小: " + totalDownloadedByte.ToString() + " 字节) ...");
                    }
                }
            }
            catch (Exception e)
            {
                Global.Print("下载 '" + name + "' 失败.");
                Global.Print("错误: " + e.Message);
                if (times < 3) goto retry2;
            }
        }

        public static void ExtractFile(string source, string target, bool print = true)
        {
            try
            {
                Global.Print("正在解压: '" + source + "' -> '" + target + "'.");

                using (Process proc = new Process())
                {
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.EnableRaisingEvents = true;

                    proc.Start();

                    proc.BeginErrorReadLine();
                    proc.BeginOutputReadLine();

                    proc.StandardInput.WriteLine(Global.AppPath + "\\7zip\\7za.exe x \"" + source + "\" -o\"" + target + "\" -y &exit");
                    proc.StandardInput.AutoFlush = true;

                    if (print)
                    {
                        proc.OutputDataReceived += (sender, e) => { Global.Print(e.Data); };
                        proc.ErrorDataReceived += (sender, e) => { Global.Print(e.Data); };
                    }

                    proc.WaitForExit();
                }

                Global.Print("解完完成: '" + source + "' -> '" + target + "'.");
            }
            catch (Exception e)
            {
                Global.Print("解压 '" + source + "' -> '" + target + "' 失败.");
                Global.Print("错误: " + e.Message);
            }
        }
    }
}
