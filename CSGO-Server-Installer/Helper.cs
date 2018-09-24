/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Helper.cs                                      */
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
using System.IO;

namespace Kxnrl.CSI
{
    class Helper
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
            Console.Title = "CSGO Server Installer v1.0  by Kyle \"Kxnrl\" Frankiss";

            Console.WriteLine("CSGO Server Installer 初始化成功.");
            Console.WriteLine("                                ");
            Console.WriteLine("CSGO相关困难问题都可以联系我:");
            Console.WriteLine("  QQ      : 673321480");
            Console.WriteLine("  Email   : kyle[AT]kxnrl.com");
            Console.WriteLine("  Steam   : https://steamcommunity.com/profiles/76561198048432253");
            Console.WriteLine("  Telegram: https://t.me/Kxnrl");
            Console.WriteLine("                 ");
            Console.WriteLine("按下任意键继续 ...");

            Console.ReadKey();
        }

        public static void CheckSteam()
        {
            // Steam Folder
            Util.CheckDirorCreate(Global.AppPath + "\\Steam");

            if (!File.Exists(Global.AppPath + "\\Steam\\steamcmd.exe"))
            {
                // donload steamcmd
                Installtion.Applications.SteamCmd();

                // download completed
                Console.WriteLine("按下任意键继续 ...");
                Console.ReadKey();
            }
        }

        public static void Check7zip()
        {
            // 7zip Folder
            Util.CheckDirorCreate(Global.AppPath + "\\7zip");

            if (!File.Exists(Global.AppPath + "\\7zip\\7za.exe"))
            {
                // donload 7zip cli
                Installtion.Applications.p7zipCLI();

                // download completed
                Console.WriteLine("按下任意键继续 ...");
                Console.ReadKey();
            }
        }

        public static void CheckNotepad()
        {
            // 7zip Folder
            Util.CheckDirorCreate(Global.AppPath + "\\Notepad");

            if (!File.Exists(Global.AppPath + "\\Notepad\\Notepad++.exe"))
            {
                // donload Notepad++
                Installtion.Applications.NotepadPlusPlus();

                // download completed
                Console.WriteLine("按下任意键继续 ...");
                Console.ReadKey();
            }
        }

        public static void Install_TT(string path)
        {
            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_ZE(string path)
        {
            // Stripper
            Installtion.Addons.Stripper(path);

            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            // ArmsFix
            Installtion.Plugins.Kxnrl.ArmsFix(path);

            // entWatch
            Installtion.Plugins.Kxnrl.entWatch(path);

            // MapMusic
            Installtion.Plugins.Kxnrl.MapMusic(path);

            // Alliedmodders组件
            Installtion.Plugins.Alliedmodders.csgo_movement_unlocker(path);
            Installtion.Plugins.Alliedmodders.napalmlagfix(path);
            Installtion.Plugins.Alliedmodders.ruleshax(path);
            Installtion.Plugins.Alliedmodders.voiceannounce_ex(path);

            // 僵尸逃跑组件
            Installtion.Plugins.Kxnrl.ZombiEscape(path);

            // 地图包?
            Installtion.Maps.ZombiEscape.Download(path);
        }

        public static void Install_MG(string path)
        {
            // Stripper
            //Installtion.Addons.Stripper(path);

            // 换图插件
            //Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            // MapMusic
            //Installtion.Plugins.Kxnrl.MapMusic(path);

            // Alliedmodders组件
            //Installtion.Plugins.Alliedmodders.csgo_movement_unlocker(path);

            // MiniGames组件
            //Installtion.Plugins.Kxnrl.MiniGames(path);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_KZ(string path)
        {
            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_SR(string path)
        {
            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_BH(string path)
        {
            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_PG(string path)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_DM(string path)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }

        public static void Install_JB(string path)
        {
            // 换图插件
            Installtion.Plugins.Kxnrl.MapChooserRedux(path);

            // SMHosties
            Installtion.Plugins.Alliedmodders.SMHosties(path);

            // MyJailbreak
            Installtion.Plugins.Shanapu.MyJailbreak(path);
        }

        public static void Install_PB(string path)
        {
            Console.WriteLine("该模式即将上线. 敬请期待 ...");
        }
    }
}
