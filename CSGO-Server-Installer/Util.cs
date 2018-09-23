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
/*  2018/09/23 10:54:56                                           */
/*                                                                */
/*  This program is licensed under the MIT License.               */
/*                                                                */
/******************************************************************/

using System;
using System.IO;
using System.Net;
using System.Threading;

namespace Kxnrl.CSI
{
    class Util
    {
        public static void ConsoleRefresh()
        {
            Console.Clear();

            Console.WriteLine(@" _______  _______  _______  _______   _________ _        _______ _________ _______  _        _        _______  _______ ");
            Console.WriteLine(@"(  ____ \(  ____ \(  ____ \(  ___  )  \__   __/( (    /|(  ____ \\__   __/(  ___  )( \      ( \      (  ____ \(  ____ )");
            Console.WriteLine(@"| (    \/| (    \/| (    \/| (   ) |     ) (   |  \  ( || (    \/   ) (   | (   ) || (      | (      | (    \/| (    )|");
            Console.WriteLine(@"| |      | (_____ | |      | |   | |     | |   |   \ | || (_____    | |   | (___) || |      | |      | (__    | (____)|");
            Console.WriteLine(@"| |      (_____  )| | ____ | |   | |     | |   | (\ \) |(_____  )   | |   |  ___  || |      | |      |  __)   |     __)");
            Console.WriteLine(@"| |            ) || | \_  )| |   | |     | |   | | \   |      ) |   | |   | (   ) || |      | |      | (      | (\ (   ");
            Console.WriteLine(@"| (____/\/\____) || (___) || (___) |  ___) (___| )  \  |/\____) |   | |   | )   ( || (____/\| (____/\| (____/\| ) \ \__");
            Console.WriteLine(@"(_______/\_______)(_______)(_______)  \_______/|/    )_)\_______)   )_(   |/     \|(_______/(_______/(_______/|/   \__/");
            Console.WriteLine(@"                                                                                                                       ");

            Console.WriteLine(Environment.NewLine);
        }

        public static void WelcomeMessage()
        {
            Console.WriteLine("CSGO Server Installer 初始化成功.");
            Console.WriteLine("按下任意键继续 ...");
            Console.ReadKey();
        }

        public static void CheckSteam()
        {
            // Steam Folder
            if (!Directory.Exists(Global.AppPath + "\\Steam"))
            {
                try
                {
                    // create folder
                    Directory.CreateDirectory(Global.AppPath + "\\Steam");
                }
                catch (Exception e)
                {
                    Console.WriteLine("创建 '" + Global.AppPath + "\\Steam" + "' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            if (!File.Exists(Global.AppPath + "\\Steam\\steamcmd.exe"))
            {
                // donload steamcmd
                try
                {
                    DownloadFile("https://static.kxnrl.com/steamcmd/steamcmd.exe", Global.AppPath + "\\Steam\\steamcmd.exe", "steamcmd.exe");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'steamcmd.exe' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }

                // download completed
                Console.WriteLine("按下任意键继续 ...");
                Console.ReadKey();
            }
        }

        public static void Check7zip()
        {
            // 7zip Folder
            if (!Directory.Exists(Global.AppPath + "\\7zip"))
            {
                try
                {
                    // create folder
                    Directory.CreateDirectory(Global.AppPath + "\\7zip");
                }
                catch (Exception e)
                {
                    Console.WriteLine("创建 '" + Global.AppPath + "\\7zip" + "' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            if (!File.Exists(Global.AppPath + "\\7zip\\7za.exe"))
            {
                // donload steamcmd
                try
                {
                    DownloadFile("https://static.kxnrl.com/7zip/7za.exe", Global.AppPath + "\\7zip\\7za.exe", "7za.exe");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 '7za.exe' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }

                // download completed
                Console.WriteLine("按下任意键继续 ...");
                Console.ReadKey();
            }
        }

        private static void DownloadFile(string url, string path, string name)
        {
            int times = 0;
            retry:
            try
            {
                times++;
                ConsoleRefresh();
                Console.WriteLine("正在下载 {0} ...", url);

                var web = (HttpWebRequest)WebRequest.Create(url);
                var response = (HttpWebResponse)web.GetResponse();
                long totalBytes = response.ContentLength;
                if (totalBytes < 0)
                {
                    throw new Exception("totalBytes is " + totalBytes.ToString());
                }

                using (var stream = response.GetResponseStream())
                {
                    using (var fs = new FileStream(path, FileMode.Create))
                    {
                        long totalDownloadedByte = 0;
                        byte[] bytes = new byte[1024];
                        int osize = stream.Read(bytes, 0, (int)bytes.Length);
                        while (osize > 0)
                        {
                            totalDownloadedByte = osize + totalDownloadedByte;
                            fs.Write(bytes, 0, osize);
                            osize = stream.Read(bytes, 0, (int)bytes.Length);

                            if (totalDownloadedByte < totalBytes)
                            {
                                Console.WriteLine("[{0}%]  downloading '{1}' ({2}/{3})...", totalDownloadedByte * 100 / totalBytes, name, totalDownloadedByte / 1024, totalBytes / 1024);
                            }
                        }

                        ConsoleRefresh();
                        Console.WriteLine("下载 '{0}' 完成   (大小: {1} 字节) ...", name, totalDownloadedByte);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("下载 '" + name + "' 失败.");
                Console.WriteLine("错误: " + e.Message);
                if (times < 3) goto retry;
            }
        }

        private static void ExtractFile(string source, string target)
        {
            try
            {
                Console.WriteLine("正在解压 '" + source + "' -> '" + target + "'.");

                using (System.Diagnostics.Process proc = new System.Diagnostics.Process())
                {
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.UseShellExecute = false;
                    proc.StartInfo.RedirectStandardInput = true;
                    proc.StartInfo.RedirectStandardOutput = true;
                    proc.StartInfo.RedirectStandardError = true;
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();

                    proc.StandardInput.WriteLine(Global.AppPath + "\\7zip\\7za.exe x \"" + source + "\" -o\"" + target + "\" -y &exit");
                    proc.StandardInput.AutoFlush = true;

                    string output = proc.StandardOutput.ReadToEnd();
                    proc.WaitForExit();
                    Console.WriteLine(Environment.NewLine + output + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("解压 '" + source + "' -> '" + target + "' 失败.");
                Console.WriteLine("错误: " + e.Message);
            }
        }

        public static void DownloadAddons(string target)
        {
            if (!Directory.Exists(target + "\\csgo\\addons"))
            {
                try
                {
                    Directory.CreateDirectory(target + "\\csgo\\addons");
                }
                catch (Exception e)
                {
                    Console.WriteLine("创建 '" + target + "\\csgo\\addons" + "' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            if(!Directory.Exists(target + "\\csgo\\addons\\metamod"))
            {
                try
                {
                    DownloadFile("https://www.sourcemm.net/latest.php?version=1.11&os=windows", target + "\\csgo\\addons\\metamod.zip", "metamod.zip");
                    ExtractFile(target + "\\csgo\\addons\\metamod.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\metamod.zip");
                    Console.WriteLine("'MetaMod' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'metamod.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!Directory.Exists(target + "\\csgo\\addons\\sourcemod"))
            {
                try
                {
                    DownloadFile("https://www.sourcemod.net/latest.php?version=1.9&os=windows", target + "\\csgo\\addons\\sourcemod.zip", "sourcemod.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\sourcemod.zip");
                    Console.WriteLine("'SourceMod' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'sourcemod.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);
        }

        public static void DownloadExtensions(string target)
        {
            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\system2.ext.dll"))
            {
                // donload system2
                try
                {
                    DownloadFile("https://build.kxnrl.com/_Raw/System2-Extension/system2.ext.dll", target + "\\csgo\\addons\\sourcemod\\extensions\\system2.ext.dll", "system2.ext.dll");
                    Console.WriteLine("'system2.ext.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'system2.ext.dll' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\soundlib.ext.2.csgo.dll"))
            {
                // donload system2
                try
                {
                    DownloadFile("https://build.kxnrl.com/_Raw/SoundLib-Extension/soundlib.ext.2.csgo.dll", target + "\\csgo\\addons\\sourcemod\\extensions\\soundlib.ext.2.csgo.dll", "soundlib.ext.2.csgo.dll");
                    Console.WriteLine("'soundlib.ext.2.csgo.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'soundlib.ext.2.csgo.dll' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\PTaH.ext.2.csgo.dll"))
            {
                // donload PTaH
                try
                {
                    DownloadFile("https://ptah.zizt.ru/files/PTaH-V1.0.8-build10-windows.zip", target + "\\csgo\\addons\\sourcemod\\PTaH.zip", "PTaH.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod\\PTaH.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\sourcemod\\PTaH.zip");
                    Console.WriteLine("'PTaH.ext.2.csgo.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'PTaH.ext.2.csgo.dll' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\accelerator.ext.dll"))
            {
                // donload accelerator
                try
                {
                    DownloadFile("https://builds.limetech.io/files/accelerator-2.3.3-git92-e01565f-windows.zip", target + "\\csgo\\addons\\sourcemod\\accelerator.zip", "accelerator.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod\\accelerator.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\sourcemod\\accelerator.zip");
                    Console.WriteLine("'accelerator.ext.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'accelerator.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\SteamWorks.ext.dll"))
            {
                // donload SteamWorks
                try
                {
                    DownloadFile("https://users.alliedmods.net/~kyles/builds/SteamWorks/SteamWorks-git130-windows.zip", target + "\\csgo\\addons\\sourcemod\\SteamWorks.zip", "SteamWorks.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod\\SteamWorks.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\sourcemod\\SteamWorks.zip");
                    Console.WriteLine("'SteamWorks.ext.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'SteamWorks.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\extensions\\dhooks.ext.dll"))
            {
                // donload dhooks
                try
                {
                    DownloadFile("https://users.alliedmods.net/~drifter/builds/dhooks/2.2/dhooks-2.2.0-hg126-windows.zip", target + "\\csgo\\addons\\sourcemod\\dhooks.zip", "dhooks.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod\\dhooks.zip", target + "\\csgo");
                    File.Delete(target + "\\csgo\\addons\\sourcemod\\dhooks.zip");
                    Console.WriteLine("'dhooks.ext.dll' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'dhooks.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);
        }

        public static void DownloadBasePlugin(string target)
        {
            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\plugins\\CSI-hostname.smx"))
            {
                // donload hostname
                try
                {
                    DownloadFile("https://static.kxnrl.com/CSI/plugins/base/CSI-hostname.smx", target + "\\csgo\\addons\\sourcemod\\plugins\\CSI-hostname.smx", "CSI-hostname.smx");
                    Console.WriteLine("'CSI-hostname.smx' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'CSI-hostname.smx' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);

            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\plugins\\CSI-analytics.smx"))
            {
                // donload analytics
                try
                {
                    DownloadFile("https://static.kxnrl.com/CSI/plugins/base/CSI-analytics.smx", target + "\\csgo\\addons\\sourcemod\\plugins\\CSI-analytics.smx", "CSI-analytics.smx");
                    Console.WriteLine("'CSI-analytics.smx' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'CSI-analytics.smx' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);
        }

        public static void Install_App_CSM(string target)
        {
            if (!File.Exists(target + "\\CSGO-Server-Manager.exe"))
            {
                // donload CSGO-Server-Manager
                try
                {
                    DownloadFile("https://build.kxnrl.com/_Raw/CSGO-Server-Manager/CSGO-Server-Manager.exe", target + "\\CSGO-Server-Manager.exe", "CSGO-Server-Manager.exe");
                    Console.WriteLine("'CSGO-Server-Manager.exe' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'CSGO-Server-Manager.exe' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);
        }

        public static void Install_Plugin_MCR(string target)
        {
            if (!File.Exists(target + "\\csgo\\addons\\sourcemod\\translations\\com.kxnrl.mcr.translations.txt"))
            {
                // donload MapChooser-Redux
                try
                {
                    DownloadFile("https://build.kxnrl.com/MapChooser-Redux/1.9/MapChooser-Redux-git129-3ca013c.zip", target + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip", "MapChooser-Redux.zip");
                    ExtractFile(target + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip", target + "\\csgo\\addons\\sourcemod");
                    File.Delete(target + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip");
                    Console.WriteLine("'MapChooser-Redux' 安装成功.");
                }
                catch (Exception e)
                {
                    Console.WriteLine("下载 'MapChooser-Redux.zip' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            Thread.Sleep(3000);
        }

        public static void Install_TT(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_ZE(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_MG(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_KZ(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_SR(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_BH(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_PG(string target)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_DM(string target)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_JB(string target)
        {
            Install_Plugin_MCR(target);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_PB(string target)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }
    }
}
