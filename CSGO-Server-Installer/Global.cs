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
/*  2018/09/23 10:54:56                                           */
/*                                                                */
/*  This program is licensed under the MIT License.               */
/*                                                                */
/******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kxnrl.CSI
{
    class Global
    {
        public static string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Kxnrl\\CSI";
    }
}
