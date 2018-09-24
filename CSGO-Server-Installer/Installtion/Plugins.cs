/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Plugins.cs                                     */
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
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Kxnrl.CSI.Installtion
{
    class Plugins
    {
        public class CSI
        {
            public static void Hostname(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\CSI-hostname.smx"))
                {
                    Global.Print("'CSI-hostname.smx' 已安装.");
                    return;
                }

                // donload hostname
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/base/CSI-hostname.smx", path + "\\csgo\\addons\\sourcemod\\plugins\\CSI-hostname.smx");
                    Global.Print("'CSI-hostname.smx' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'CSI-hostname.smx' 失败.");
                    Global.Print("错误: " + e.Message);
                }

                Thread.Sleep(3000);
            }

            public static void Analytics(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\CSI-analytics.smx"))
                {
                    Global.Print("'CSI-analytics.smx' 已安装.");
                    return;
                }

                // donload analytics
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/base/CSI-analytics.smx", path + "\\csgo\\addons\\sourcemod\\plugins\\CSI-analytics.smx");
                    Global.Print("'CSI-analytics.smx' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'CSI-analytics.smx' 失败.");
                    Global.Print("错误: " + e.Message);
                }

                Thread.Sleep(3000);
            }
        }

        public class Kxnrl
        {
            public static void MapChooserRedux(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\translations\\com.kxnrl.mcr.translations.txt"))
                {
                    Global.Print("'MapChooser-Redux' 已安装.");
                    return;
                }

                // donload MapChooser-Redux
                try
                {
                    Util.DownloadFile("https://build.kxnrl.com/MapChooser-Redux/1.9/MapChooser-Redux-git129-3ca013c.zip", path + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip", "MapChooser-Redux.zip");
                    Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip", path + "\\csgo\\addons\\sourcemod");
                    Global.Print("'MapChooser-Redux' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'MapChooser-Redux.zip' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除所有多余文件.
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\MapChooser-Redux.zip");
                }

                Thread.Sleep(3000);
            }

            public static void ArmsFix(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\ArmsFix.smx"))
                {
                    Global.Print("'ArmsFix.smx' 已安装.");
                    return;
                }

                // donload ArmsFix
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/ArmsFix/ArmsFix.smx", path + "\\csgo\\addons\\sourcemod\\plugins\\ArmsFix.smx");
                    Global.Print("'ArmsFix.smx' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'ArmsFix.smx' 失败.");
                    Global.Print("错误: " + e.Message);
                }

                Thread.Sleep(3000);
            }

            public static void entWatch(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\entWatch.smx"))
                {
                    Global.Print("'entWatch.smx' 已安装.");
                    return;
                }

                // donload entWatch
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/entWatch/entWatch.7z", path + "\\csgo\\addons\\entWatch.7z", "entWatch.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\entWatch.7z", path + "\\csgo");
                    Global.Print("'entWatch' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'entWatch.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\entWatch.7z");
                }

                Thread.Sleep(3000);
            }

            public static void MapMusic(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\MapMusic.smx"))
                {
                    Global.Print("'MapMusic' 已安装.");
                    return;
                }

                // donload entWatch
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/MapMusic/MapMusic.7z", path + "\\csgo\\addons\\MapMusic.7z", "MapMusic.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\MapMusic.7z", path + "\\csgo");
                    Global.Print("'MapMusic' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'MapMusic.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\MapMusic.7z");
                }

                Thread.Sleep(3000);
            }

            public static void ZombiEscape(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\zombiereloaded.smx"))
                {
                    Global.Print("'ZombiEscape' 已安装.");
                    return;
                }

                // donload ZombiEscape
                try
                {
                    Util.DownloadFile("https://yukiim.kxnrl.com/csi/packages/ZombiEscape.7z", path + "\\csgo\\addons\\ZombiEscape.zip", "ZombiEscape.zip");
                    Util.ExtractFile(path + "\\csgo\\addons\\ZombiEscape.zip", path + "\\csgo");
                    Global.Print("'ZombiEscape' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'ZombiEscape.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\ZombiEscape.7z");
                }

                Thread.Sleep(3000);
            }
        }

        public class Shanapu
        {
            public static void MyJailbreak(string target)
            {
                if (File.Exists(target + "\\csgo\\addons\\sourcemod\\plugins\\MyJailbreak\\myjailbreak.smx"))
                {
                    Global.Print("'MyJailbreak' 已安装.");
                    return;
                }

                // download MyJB
                try
                {
                    Util.DownloadFile("https://shanapu.de/MyJailbreak/downloads/SM1.9/MyJB-master-latest.zip", target + "\\csgo\\addons\\sourcemod\\MyJailbreak.zip", "MyJailbreak.zip");
                    Util.ExtractFile(target + "\\csgo\\addons\\sourcemod\\MyJailbreak.zip", target + "\\csgo\\addons\\sourcemod\\temp");

                    if (Directory.Exists(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver"))
                    {
                        try
                        {
                            Directory.Move(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver\\addons", target + "\\csgo");
                            Directory.Move(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver\\cfg", target + "\\csgo");
                            Directory.Move(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver\\materials", target + "\\csgo");
                            Directory.Move(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver\\models", target + "\\csgo");
                            Directory.Move(target + "\\csgo\\addons\\sourcemod\\temp\\gameserver\\sound", target + "\\csgo");
                        }
                        catch (Exception e)
                        {
                            Global.Print("'MyJailbreak' 安装失败.");
                            Global.Print("错误: " + e.Message);
                        }
                    }
                    else
                    {
                        throw new Exception("文件夹不存在");
                    }

                    Global.Print("'MyJailbreak' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("'MyJailbreak' 安装失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除所有多余文件.
                    Util.SafeDeleteDir(target + "\\csgo\\addons\\sourcemod\\temp");
                    Util.SafeDeleteFile(target + "\\csgo\\addons\\sourcemod\\MyJailbreak.zip");
                }
            }
        }

        public class Alliedmodders
        {
            public static void SMHosties(string path)
            {
                if (!File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\sm_hosties.smx"))
                {
                    Global.Print("'SMHosties' 已安装.");
                    return;
                }

                // download SMHosties
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/jailbreak/sm-hosties-2.2.0.zip", path + "\\csgo\\addons\\sourcemod\\sm_hosties.zip", "sm_hosties.zip");
                    Util.ExtractFile(path + "\\csgo\\addons\\sourcemod\\sm_hosties.zip", path + "\\csgo");
                    Global.Print("'SMHosties' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("'SMHosties' 安装失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除所有多余文件.
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\sourcemod\\sm_hosties.zip");
                }
            }

            public static void csgo_movement_unlocker(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\csgo_movement_unlocker.smx"))
                {
                    Global.Print("'csgo_movement_unlocker' 已安装.");
                    return;
                }

                // donload csgo_movement_unlocker
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/Alliedmodders/csgo_movement_unlocker.7z", path + "\\csgo\\addons\\csgo_movement_unlocker.7z", "csgo_movement_unlocker.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\csgo_movement_unlocker.7z", path + "\\csgo");
                    Global.Print("'csgo_movement_unlocker' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'csgo_movement_unlocker.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\csgo_movement_unlocker.7z");
                }

                Thread.Sleep(3000);
            }

            public static void napalmlagfix(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\napalmlagfix.smx"))
                {
                    Global.Print("'napalmlagfix' 已安装.");
                    return;
                }

                // donload napalmlagfix
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/Alliedmodders/napalmlagfix.7z", path + "\\csgo\\addons\\napalmlagfix.7z", "napalmlagfix.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\napalmlagfix.7z", path + "\\csgo");
                    Global.Print("'napalmlagfix' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'napalmlagfix.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\napalmlagfix.7z");
                }

                Thread.Sleep(3000);
            }

            public static void ruleshax(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\ruleshax.smx"))
                {
                    Global.Print("'ruleshax' 已安装.");
                    return;
                }

                // donload ruleshax
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/Alliedmodders/ruleshax.7z", path + "\\csgo\\addons\\ruleshax.7z", "ruleshax.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\ruleshax.7z", path + "\\csgo");
                    Global.Print("'ruleshax' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'ruleshax.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\ruleshax.7z");
                }

                Thread.Sleep(3000);
            }

            public static void voiceannounce_ex(string path)
            {
                if (File.Exists(path + "\\csgo\\addons\\sourcemod\\plugins\\voiceannounce_ex.smx"))
                {
                    Global.Print("'voiceannounce_ex' 已安装.");
                    return;
                }

                // donload voiceannounce_ex
                try
                {
                    Util.DownloadFile("https://static.kxnrl.com/CSI/plugins/Alliedmodders/voiceannounce_ex.7z", path + "\\csgo\\addons\\voiceannounce_ex.7z", "voiceannounce_ex.7z");
                    Util.ExtractFile(path + "\\csgo\\addons\\voiceannounce_ex.7z", path + "\\csgo");
                    Global.Print("'voiceannounce_ex' 安装成功.");
                }
                catch (Exception e)
                {
                    Global.Print("下载 'voiceannounce_ex.7z' 失败.");
                    Global.Print("错误: " + e.Message);
                }
                finally
                {
                    // 删除安装包
                    Util.SafeDeleteFile(path + "\\csgo\\addons\\voiceannounce_ex.7z");
                }

                Thread.Sleep(3000);
            }
        }
    }
}
