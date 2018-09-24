/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Extensions.cs                                  */
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
    class Extensions
    {
        public static void System2(string path)
        {
            if (!File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\system2.ext.dll"))
            {
                Global.Print("'system2.ext.dll' 已安装.");
                return;
            }

            // donload system2
            try
            {
                Util.DownloadFile("https://build.kxnrl.com/_Raw/System2-Extension/system2.ext.dll", path + "\\csgo\\addons\\sourcemod\\extensions\\system2.ext.dll", "system2.ext.dll");
                Global.Print("'system2.ext.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'system2.ext.dll' 失败.");
                Global.Print("错误: " + e.Message);
            }

            Thread.Sleep(3000);
        }

        public static void SoundLib(string path)
        {
            if (File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\soundlib.ext.2.csgo.dll"))
            {
                Global.Print("'soundlib.ext.2.csgo.dll' 已安装.");
                return;
            }

            // donload soundlib
            try
            {
                Util.DownloadFile("https://build.kxnrl.com/_Raw/SoundLib-Extension/soundlib.ext.2.csgo.dll", path + "\\csgo\\addons\\sourcemod\\extensions\\soundlib.ext.2.csgo.dll", "soundlib.ext.2.csgo.dll");
                Global.Print("'soundlib.ext.2.csgo.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'soundlib.ext.2.csgo.dll' 失败.");
                Global.Print("错误: " + e.Message);
            }

            Thread.Sleep(3000);
        }

        public static void PTaH(string path)
        {
            if (File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\PTaH.ext.2.csgo.dll"))
            {
                Global.Print("'PTaH.ext.2.csgo.dll' 已安装.");
                return;
            }

            // donload PTaH
            try
            {
                Util.DownloadFile("https://ptah.zizt.ru/files/PTaH-V1.0.8-build10-windows.zip", path + "\\csgo\\addons\\sourcemod\\PTaH.zip", "PTaH.zip");
                Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\PTaH.zip", path + "\\csgo");
                Global.Print("'PTaH.ext.2.csgo.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'PTaH.ext.2.csgo.dll' 失败.");
                Global.Print("错误: " + e.Message);
            }
            finally
            {
                // 删除安装包
                Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\PTaH.zip");
            }

            Thread.Sleep(3000);
        }

        public static void Accelerator(string path)
        {
            if (!File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\accelerator.ext.dll"))
            {
                Global.Print("'accelerator.ext.dll' 已安装.");
                return;
            }

            // donload accelerator
            try
            {
                Util.DownloadFile("https://builds.limetech.io/files/accelerator-2.3.3-git92-e01565f-windows.zip", path + "\\csgo\\addons\\sourcemod\\accelerator.zip", "accelerator.zip");
                Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\accelerator.zip", path + "\\csgo");
                Global.Print("'accelerator.ext.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'accelerator.zip' 失败.");
                Global.Print("错误: " + e.Message);
            }
            finally
            {
                // 删除安装包
                Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\accelerator.zip");
            }

            Thread.Sleep(3000);
        }

        public static void SteamWorks(string path)
        {
            if (!File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\SteamWorks.ext.dll"))
            {
                Global.Print("'SteamWorks.ext.dll' 已安装.");
                return;
            }

            // donload SteamWorks
            try
            {
                Util.DownloadFile("https://users.alliedmods.net/~kyles/builds/SteamWorks/SteamWorks-git130-windows.zip", path + "\\csgo\\addons\\sourcemod\\SteamWorks.zip", "SteamWorks.zip");
                Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\SteamWorks.zip", path + "\\csgo");
                Global.Print("'SteamWorks.ext.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'SteamWorks.zip' 失败.");
                Global.Print("错误: " + e.Message);
            }
            finally
            {
                // 删除安装包
                Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\SteamWorks.zip");
            }

            Thread.Sleep(3000);
        }

        public static void DHooks(string path)
        {
            if (!File.Exists(path + "\\csgo\\addons\\sourcemod\\extensions\\dhooks.ext.dll"))
            {
                Global.Print("'dhooks.ext.dll' 已安装.");
                return;
            }

            // donload dhooks
            try
            {
                Util.DownloadFile("https://users.alliedmods.net/~drifter/builds/dhooks/2.2/dhooks-2.2.0-hg126-windows.zip", path + "\\csgo\\addons\\sourcemod\\dhooks.zip", "dhooks.zip");
                Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\dhooks.zip", path + "\\csgo");
                Global.Print("'dhooks.ext.dll' 安装成功.");
            }
            catch (Exception e)
            {
                Global.Print("下载 'dhooks.zip' 失败.");
                Global.Print("错误: " + e.Message);
            }
            finally
            {
                // 删除安装包
                Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\dhooks.zip");
            }

            Thread.Sleep(3000);
        }
    }
}
