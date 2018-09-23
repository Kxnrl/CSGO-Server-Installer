/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Program.cs                                     */
/*  Description:   Make csgo server install easier.               */
/*                                                                */
/*                                                                */
/*  Copyright (C) 2018  Kyle                                      */
/*  2018/09/23 10:54:56                                           */
/*                                                                */
/*  This program is licensed under the MIT License.               */
/*                                                                */
/******************************************************************/

//
// This util for chinese only.
//

using System;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Diagnostics;

namespace Kxnrl.CSI
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // 只能运行一个
            Mutex self = new Mutex(true, Application.StartupPath.GetHashCode().ToString(), out bool allow);
            if (!allow)
            {
                MessageBox.Show("已有一个CSI在运行中了 ...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

            // 放在LocalAppData运行
            if (!Application.StartupPath.Equals(Global.AppPath))
            {
                try
                {
                    // 建立目标文件夹
                    if (!Directory.Exists(Global.AppPath))
                    {
                        Directory.CreateDirectory(Global.AppPath);
                        Console.WriteLine("创建 '" + Global.AppPath + "' 成功.");
                    }

                    // 复制文件
                    File.Copy(Application.StartupPath + "\\CSGO-Server-Installer.exe", Global.AppPath + "\\CSGO-Server-Installer.exe", true);
                    Console.WriteLine("安装 '" + Global.AppPath + "\\CSGO-Server-Installer.exe' 成功.");
                }
                catch (Exception e)
                {
                    // 挂起这个错误, 确认后直接退出
                    Console.WriteLine("安装 'CSGO-Server-Installer.exe' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                    Console.ReadLine();
                    Environment.Exit(-1);
                }

                // 创建桌面快捷方式以及自删脚本
                Win32Api.Helper.CreateShortcut();
                Win32Api.Helper.CreateBatFile();

                // 运行自删脚本
                Process.Start(Global.AppPath + "\\start.bat");

                // 退出本实例
                Environment.Exit(0);
            }

            // 删除自删脚本
            if (File.Exists(Global.AppPath + "\\start.bat"))
            {
                try
                {
                    File.Delete(Global.AppPath + "\\start.bat");
                }
                catch (Exception e)
                {
                    Console.WriteLine("删除 'start.bat' 失败.");
                    Console.WriteLine("错误: " + e.Message);
                }
            }

            // 检查是否安装SteamCmd
            Util.CheckSteam();

            // 检查是否有7zip CLI
            Util.Check7zip();

            // 刷新控制台
            welcome:
            Util.ConsoleRefresh();

            // 欢迎消息
            Util.WelcomeMessage();

            // 继续刷刷刷
            Util.ConsoleRefresh();

            // 询问是否要建立服务器, 使用死循环方法. 只有关掉或特殊按键才能终止
            do
            {
                Console.WriteLine("你要安装新的CSGO服务端吗? [Y/N] (按下ESC可以退出程序)");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.N     : goto welcome;
                    case ConsoleKey.Y     : goto selectFolder;
                    case ConsoleKey.Escape: Environment.Exit(0); break;
                    default: continue;
                }
            }
            while (true);

            // 选择要安装的文件夹
            selectFolder:
            string targetFolder;
            do
            {
                using (var browser = new FolderBrowserDialog())
                {
                    browser.Description = "选择你想要安装的路径" + Environment.NewLine + "如果没有就创建一个新的吧";
                    browser.ShowNewFolderButton = true;
                    if (browser.ShowDialog() == DialogResult.OK)
                    {
                        if (string.IsNullOrEmpty(browser.SelectedPath))
                        {
                            MessageBox.Show("非法路径!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue;
                        }
                        targetFolder = browser.SelectedPath;
                        goto startSteam;
                    }
                }
            }
            while (true);

            // Start steam to download
            startSteam:
            using (var process = new Process())
            {
                process.EnableRaisingEvents = true;
                process.StartInfo.FileName = Global.AppPath + "\\Steam\\steamcmd.exe";
                process.StartInfo.Arguments = "+login anonymous +force_install_dir \"" + targetFolder + "\" " + "+app_update 740 +exit";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();

                Util.ConsoleRefresh();
                Console.WriteLine("正在安装 CS:GO Dedicated Server 到 '" + targetFolder + "' ...");

                process.OutputDataReceived += (sender, e) => { Console.WriteLine(e.Data); };
                process.ErrorDataReceived += (sender, e) => { Console.WriteLine(e.Data); };

                process.WaitForExit();
            }

            // clr
            Util.ConsoleRefresh();

            // MetaMod & SourcMmod
            Console.WriteLine("安装 CS:GO Dedicated Server 成功 ...");
            Console.WriteLine("安装目录 '" + targetFolder + "' .");
            Console.WriteLine("按下任意键继续 -> 下载服务器附加组件 MetaMod & SourcMmod ...");
            Console.ReadKey();

            // SourceMod Extensions
            for(int i = 0; i < 3; ++i) Util.DownloadAddons(targetFolder);
            Console.WriteLine("安装 MetaMod & SourceMod 成功 ...");
            Console.WriteLine("按下任意键继续 -> 下载服务器附加组件 SourceMod Extensions ...");
            Console.ReadKey();

            // SourceMod Base Plugins
            Util.DownloadExtensions(targetFolder);
            Console.WriteLine("安装 SourceMod Extensions 成功 ...");
            Console.WriteLine("按下任意键继续 -> 下载服务器附加组件 SourceMod Base Plugins ...");
            Console.ReadKey();

            // SourceMod Game Mod Plugins
            Util.DownloadBasePlugin(targetFolder);
            Console.WriteLine("安装 SourceMod Base Plugins 成功 ...");
            Console.WriteLine("按下任意键继续 -> 下载服务器附加组件 SourceMod Game Mod Plugins ...");
            Console.ReadKey();

            // 询问游戏模式, 使用死循环方法. 只有关掉或特殊按键才能终止
            do
            {
                Util.ConsoleRefresh();
                Console.WriteLine("请选择您想要运行的游戏模式: (我们会为您安装好对于模式相应的组件)");
                Console.WriteLine("\t1. TT 叛徒模式/匪镇谍影");
                Console.WriteLine("\t2. ZE 僵尸逃跑");
                Console.WriteLine("\t3. MG 娱乐休闲/娱乐对抗/娱乐世界");
                Console.WriteLine("\t4. KZ 极限跳跃/KreedZ");
                Console.WriteLine("\t5. SR 滑翔闯关/Surf");
                Console.WriteLine("\t6. BH 极速连跳/BHop");
                Console.WriteLine("\t7. PG 满十竞赛/PUG/十人约战");
                Console.WriteLine("\t8. DM 死斗竞技/Death Match");
                Console.WriteLine("\t9. JB 越狱搞基/越狱暴动/监狱模式");
                Console.WriteLine("\t0. PB 混战竞技/休闲模式");
                Console.WriteLine("\tY. 跳过该步骤");
                Console.WriteLine("\tN. 返回主目录");
                Console.WriteLine("\tE. 退出程序");

                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.N     : goto welcome;
                    case ConsoleKey.Y     : goto doneGamemode;
                    case ConsoleKey.Escape: Environment.Exit(0);           break;
                    case ConsoleKey.E     : Environment.Exit(0);           break;
                    case ConsoleKey.D1    : Util.Install_TT(targetFolder); goto doneGamemode;
                    case ConsoleKey.D2    : Util.Install_ZE(targetFolder); goto doneGamemode;
                    case ConsoleKey.D3    : Util.Install_MG(targetFolder); goto doneGamemode;
                    case ConsoleKey.D4    : Util.Install_KZ(targetFolder); goto doneGamemode;
                    case ConsoleKey.D5    : Util.Install_SR(targetFolder); goto doneGamemode;
                    case ConsoleKey.D6    : Util.Install_BH(targetFolder); goto doneGamemode;
                    case ConsoleKey.D7    : Util.Install_PG(targetFolder); goto doneGamemode;
                    case ConsoleKey.D8    : Util.Install_DM(targetFolder); goto doneGamemode;
                    case ConsoleKey.D9    : Util.Install_JB(targetFolder); goto doneGamemode;
                    case ConsoleKey.D0    : Util.Install_PB(targetFolder); goto doneGamemode;
                    default: continue;
                }
            }
            while (true);

            // OK
            doneGamemode:
            Util.ConsoleRefresh();

            // 是否安装CSM?
            do
            {
                Console.WriteLine("你要安装CSGO服务器管理器吗? [Y/N] (按下ESC可以退出程序)");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.N     : goto doneCSM;
                    case ConsoleKey.Y     : Util.Install_App_CSM(targetFolder); goto doneCSM;
                    case ConsoleKey.Escape: Environment.Exit(0); break;
                    default: continue;
                }
            }
            while (true);

            // OK
            doneCSM:
            Util.ConsoleRefresh();

            // Well done
            Process.Start("explorer.exe \"" + targetFolder + "\"");

            Console.WriteLine("安装好了 ...");
            Console.WriteLine("按下任意键将会退出程序...");

            Console.ReadKey();
        }
    }
}
