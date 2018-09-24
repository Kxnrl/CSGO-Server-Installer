/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Addons.cs                                      */
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
    class Addons
    {
        public static void MetaMod(string path)
        {
            // csgo/addons
            Util.CheckDirorCreate(path + "\\csgo\\addons");

            // 尚未安装MetaMod
            if (!Directory.Exists(path + "\\csgo\\addons\\metamod"))
            {
                try
                {
                    Util.DownloadFile("https://www.sourcemm.net/latest.php?version=1.11&os=windows", path + "\\csgo\\addons\\metamod.zip", "metamod.zip");
                    Util.ExtractFile(path + "\\csgo\\addons\\metamod.zip", path + "\\csgo");
                    Global.Print("'MetaMod' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'metamod.zip' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\metamod.zip");
                }
            }

            Thread.Sleep(3000);
        }

        public static void SourceMod(string path)
        {
            // 尚未安装SourceMod
            if (!Directory.Exists(path + "\\csgo\\addons\\sourcemod"))
            {
                try
                {
                    Util.DownloadFile("https://www.sourcemod.net/latest.php?version=1.9&os=windows", path + "\\csgo\\addons\\sourcemod.zip", "sourcemod.zip");
                    Util.ExtractFile(path + "\\csgo\\addons\\sourcemod.zip", path + "\\csgo");
                    Global.Print("'SourceMod' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'sourcemod.zip' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod.zip");
                }
            }

            Thread.Sleep(3000);
        }
    }
}
