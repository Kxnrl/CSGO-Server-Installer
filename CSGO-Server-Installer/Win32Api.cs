/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Win32Api.cs                                    */
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
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace Kxnrl.CSI.Win32Api
{
    class Helper
    {
        public static void CreateShortcut()
        {
            string lnkpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\Server Installer.lnk";

            if(System.IO.File.Exists(lnkpath))
            {
                return;
            }

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(lnkpath);

            shortcut.Description = "CSGO Server Installer";
            //shortcut.Hotkey = "Ctrl+Shift+C";
            shortcut.TargetPath = Global.AppPath + "\\CSGO-Server-Installer.exe";
            shortcut.Save();
        }

        public static void CreateBatFile()
        {
            using (StreamWriter sw = new StreamWriter(Global.AppPath + "\\start.bat", true))
            {
                sw.WriteLine("@Echo Wscript.Sleep(100) > sleep.vbs");
                sw.WriteLine("@Start /w wscript.exe sleep.vbs");
                sw.WriteLine("@Del /Q sleep.vbs");
                sw.WriteLine("@Del /Q " + Application.StartupPath + "\\CSGO-Server-Installer.exe");
                sw.WriteLine("Start /high %localappdata%/Kxnrl/CSI/CSGO-Server-Installer.exe");
            }
        }
    }
}
