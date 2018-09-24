/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          SRCDS.cs                                       */
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
using System.Windows.Forms;

namespace Kxnrl.CSI
{
    class SRCDS
    {
        public class Initialization
        {
            public class Server
            {
                public static void ServerCfg(string srcds)
                {
                    if (!File.Exists(srcds + "\\csgo\\cfg\\server.cfg"))
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(srcds + "\\csgo\\cfg\\server.cfg", true))
                            {
                                // rcon_password
                                sw.WriteLine("");
                            }

                            MessageBox.Show("服务端已经安装并设置完成!" + Environment.NewLine + "请更改您的服务器名字!", "CSGO Server Installer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Process.Start(Global.AppPath + "\\Notepad\\Notepad++.exe", " \"" + srcds + "\\csgo\\addons\\sourcemod\\configs\\hostname.cfg" + "\" ");
                        }
                        catch (Exception e)
                        {
                            Global.Print("初始化服务器名字失败 ...");
                            Global.Print("错误: " + e.Message);
                        }
                    }
                }

                public static void MotdTxt(string srcds)
                {
                    // 删除旧文件
                    Util.SafeDeleteFile(srcds + "\\csgo\\motd.txt");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(srcds + "\\csgo\\motd.txt", true))
                        {
                            // rcon_password
                            sw.WriteLine("https://github.com/kxnrl/CSGO-Server-Installer");
                        }
                    }
                    catch (Exception e)
                    {
                        Global.Print("初始化服务器MOTD失败 ...");
                        Global.Print("错误: " + e.Message);
                    }
                }
            }

            public class SourceMod
            {
                public static void HostnameCfg(string srcds)
                {
                    if (!File.Exists(srcds + "\\csgo\\addons\\sourcemod\\configs\\hostname.cfg"))
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(srcds + "\\csgo\\addons\\sourcemod\\configs\\hostname.cfg", true))
                            {
                                sw.WriteLine("[CSI] 您还没有设置服务器名字!");
                            }

                            MessageBox.Show("服务端已经安装并设置完成!" + Environment.NewLine + "请更改您的服务器名字!", "CSGO Server Installer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Process.Start(Global.AppPath + "\\Notepad\\Notepad++.exe", " \"" + srcds + "\\csgo\\addons\\sourcemod\\configs\\hostname.cfg" + "\" ");
                        }
                        catch (Exception e)
                        {
                            Global.Print("初始化服务器名字失败 ...");
                            Global.Print("错误: " + e.Message);
                        }
                    }
                }

                public static void SimpleAdmionIni(string srcds)
                {
                    // 删除旧文件
                    Util.SafeDeleteFile(srcds + "\\csgo\\addons\\sourcemod\\configs\\admins_simple.cfg");

                    try
                    {
                        using (StreamWriter sw = new StreamWriter(srcds + "\\csgo\\addons\\sourcemod\\configs\\admins_simple.cfg", true))
                        {
                            sw.WriteLine("// 权限总共有 abcdefghijklmnopqrst 和 z                                ");
                            sw.WriteLine("// 权重范围是 1 ~ 100                                                  ");
                            sw.WriteLine("// 查看对应的权限 https://wiki.alliedmods.net/Adding_Admins_(SourceMod)");
                            sw.WriteLine("// 一行对应一个管理员                                                  ");
                            sw.WriteLine("// 你说对了, //就是注释的意思, //之后的内容无效                        ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("// SteamID 可以访问 https://steamrepcn.com 查                          ");
                            sw.WriteLine("// CSGO只认STEAM_1开头的STEAMID, 如果你查到的是0, 请改为1              ");
                            sw.WriteLine("// 例如STEAM_0:1:44083262则改为STEAM_1:1:44083262                      ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("// 给你做一个示范                                                      ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("//   STEAMID           需要给什么权限和权重 (中间分号隔开)             ");
                            sw.WriteLine("//                                                                     ");
                            sw.WriteLine("\"STEAM_1:1:44083262\"       \"abcdefghijklmnopqrstz:100\"             ");

                            MessageBox.Show("管理员权限初始化完成!" + Environment.NewLine + "请按照说明设置您自己为管理员!", "CSGO Server Installer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Process.Start(Global.AppPath + "\\Notepad\\Notepad++.exe", " \"" + srcds + "\\csgo\\addons\\sourcemod\\configs\\admins_simple.cfg" + "\" ");
                        }
                    }
                    catch (Exception e)
                    {
                        Global.Print("初始化服务器管理员列表 ...");
                        Global.Print("错误: " + e.Message);
                    }
                }

                public static void CoreCfg(string srcds)
                {
                    // 删除旧文件
                    Util.SafeDeleteFile(srcds + "\\csgo\\addons\\sourcemod\\configs\\core.cfg");

                    // 下载改好的版本
                    Util.DownloadFile("https://static.kxnrl.com/CSI/configs/sourcemod/core.cfg", srcds + "\\csgo\\addons\\sourcemod\\configs\\core.cfg");
                }

                public static void SourcemodCfg(string srcds)
                {
                    // 删除旧文件
                    Util.SafeDeleteFile(srcds + "\\csgo\\cfg\\sourcemod\\sourcemod.cfg");

                    // 下载改好的版本
                    Util.DownloadFile("https://static.kxnrl.com/CSI/configs/sourcemod/sourcemod.cfg", srcds + "\\csgo\\cfg\\sourcemod\\sourcemod.cfg");
                }
            }
        }
    }
}
