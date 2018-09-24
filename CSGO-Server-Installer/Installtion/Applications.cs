/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Applications.cs                                */
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
using System.Threading;

namespace Kxnrl.CSI.Installtion
{
    class Applications
    {
        public static void SteamCmd()
        {
            try
            {
                // 阻塞线程下载
                Util.DownloadFile("https://static.kxnrl.com/steamcmd/steamcmd.exe", Global.AppPath + "\\Steam\\steamcmd.exe", "steamcmd.exe");
            }
            catch (Exception e)
            {
                Global.Print("下载 'steamcmd.exe' 失败.");
                Global.Print("错误: " + e.Message);
            }
        }

        public static void p7zipCLI()
        {
            try
            {
                // 阻塞线程下载
                Util.DownloadFile("https://static.kxnrl.com/7zip/7za.exe", Global.AppPath + "\\7zip\\7za.exe", "7za.exe");
            }
            catch (Exception e)
            {
                Global.Print("下载 '7za.exe' 失败.");
                Global.Print("错误: " + e.Message);
            }
        }

        public static void NotepadPlusPlus()
        {
            try
            {
                // 阻塞线程下载
                Util.DownloadFile("https://notepad-plus-plus.org/repository/7.x/7.5.8/npp.7.5.8.bin.x64.7z", Global.AppPath + "\\npp.7z", "Notepad++.7z");
                Util.ExtractFile(Global.AppPath + "\\npp.7z", Global.AppPath + "\\Notepad");
                Util.SafeDeleteFile(Global.AppPath + "\\npp.7z");
            }
            catch (Exception e)
            {
                Global.Print("下载 'Notepad++.7z' 失败.");
                Global.Print("错误: " + e.Message);
            }
        }

        public static void CSM(string path)
        {
            if (File.Exists(path + "\\CSGO-Server-Manager.exe"))
            {
                Global.Print("'CSGO-Server-Manager.exe' 已安装.");
                return;
            }

            // donload CSGO-Server-Manager
            try
            {
                Util.DownloadFile("https://build.kxnrl.com/_Raw/CSGO-Server-Manager/CSGO-Server-Manager.exe", path + "\\CSGO-Server-Manager.exe", "CSGO-Server-Manager.exe");
                Global.Print("'CSGO-Server-Manager.exe' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'CSGO-Server-Manager.exe' 失败.");
                Global.Print("错误: " + e.Message);
            }

            Thread.Sleep(3000);
        }
    }
}
