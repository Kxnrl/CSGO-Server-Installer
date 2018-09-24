using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace Kxnrl.CSI.Installtion
{
    class Maps
    {
        public class ZombiEscape
        {
            public static void Download(string srcds)
            {
                if (MessageBox.Show("你要下载僵尸逃跑地图包到服务器吗?" + Environment.NewLine + "这可能需要耗费很长时间!", "CSGO Server Installer", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                {
                    // 取消下载地图
                    return;
                }

                try
                {
                    using (WebClient web = new WebClient())
                    {
                        string result = web.DownloadString("https://yukiim.kxnrl.com/csi/maps/ze.txt");

                        string[] maps = result.Split('\n');

                        foreach (string map in maps)
                        {
                            Console.WriteLine("准备下载 '" + map + "' ...");
                        }

                        Thread.Sleep(5000);

                        foreach(string map in maps)
                        {
                            if (File.Exists(srcds + "\\maps\\" + map + ".bsp"))
                            {
                                //地图已存在
                                continue;
                            }

                            try
                            {
                                web.DownloadFile("https://yukiim.kxnrl.com/csi/maps" + maps + ".7z", srcds + "\\" + map + ".7z");
                                Util.ExtractFile(srcds + "\\" + map + ".7z", srcds + "\\maps", false);
                                Global.Print("添加地图 '" + map + "' 完成.");
                            }
                            catch (Exception e)
                            {
                                Global.Print("下载 '" + map + "' 失败.");
                                Global.Print("错误: " + e.Message);
                            }
                            finally
                            {
                                // 删除地图压缩包
                                Util.SafeDeleteFile(srcds + "\\" + map + ".7z");
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Global.Print("下载 僵尸逃跑地图列表 失败.");
                    Global.Print("错误: " + e.Message);
                }
            }
        }

    }
}
