/******************************************************************/
/*                                                                */
/*                     CSGO Server Installer                      */
/*                                                                */
/*                                                                */
/*  File:          Global.cs                                      */
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
    class Global
    {
        public static string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Kxnrl\\CSI";

        private static StreamWriter sw;

        public static void Init()
        {
            // 删除旧日志
            Util.SafeDeleteFile(AppPath + "\\console.log");

            // 初始写入流
            sw = new StreamWriter(AppPath + "\\console.log", true);
        }

        public static void Print(string text)
        {
            // 写入日志
            sw.WriteLine(text);
            
            // 输出
            Console.WriteLine(text);
        }
    }
}
