using Bot_Application.Screen;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Bot_Application
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            return;
            Bitmap m_Bitmap = WebSiteThumbnail.GetWebSiteThumbnail("http://pbie.chinacloudsites.cn/Dashboard/Report?reportId=0a46b754-f5a3-4c19-b032-c6599bdb2f4d", 4600, 4600, 4600, 4600);
            MemoryStream ms = new MemoryStream();

            string FileName = DateTime.Now.ToString("yyyyMMddhhmmss");

            string filePath = "SnapPic/";
            string severFilePath = Server.MapPath(filePath);
            //If directory does not exist
            if (!Directory.Exists(severFilePath))
            { // if it doesn't exist, create
                System.IO.Directory.CreateDirectory(severFilePath);
            }

            using (MemoryStream memory = new MemoryStream())
            {
                using (FileStream fs = new FileStream(Server.MapPath("SnapPic/" + FileName + ".jpg"), FileMode.Create, FileAccess.ReadWrite))
                {
                    m_Bitmap.Save(memory, ImageFormat.Jpeg);
                    byte[] bytes = memory.ToArray();
                    fs.Write(bytes, 0, bytes.Length);
                }
            }


            //m_Bitmap.Save(Server.MapPath("SnapPic/" + FileName + ".jpg"));//保存截图到SnapPic目录下

            //m_Bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);//JPG、GIF、PNG等均可

            byte[] buff = ms.ToArray();
            //Response.BinaryWrite(buff);

            //Thread NewTh = new Thread(CaptureImage);
            //NewTh.SetApartmentState(ApartmentState.STA);//必须启动单元线程
            //NewTh.Start();
        }
        public void CaptureImage()
        {
            //你的任务代码
            try
            {

                string url = "http://www.baidu.com";

                GetSnap thumb = new GetSnap(url);
                System.Drawing.Bitmap x = thumb.GetBitmap();//获取截图
                string FileName = DateTime.Now.ToString("yyyyMMddhhmmss");

                x.Save(Server.MapPath("SnapPic/" + FileName + ".jpg"));//保存截图到SnapPic目录下

            }
            catch (Exception ex)
            {
                string emg = ex.Message;
            }
        }
    }
}