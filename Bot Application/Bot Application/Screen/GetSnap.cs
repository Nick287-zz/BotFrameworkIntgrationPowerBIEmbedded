using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Diagnostics;

namespace Bot_Application.Screen
{

    /// <summary>
    /// GetSnap 的摘要说明
    /// </summary>

    public class GetSnap
    {
        private string MyURL;

        public string WebSite
        {
            get { return MyURL; }
            set { MyURL = value; }
        }

        public GetSnap(string WebSite/*, int ScreenWidth, int ScreenHeight, int ImageWidth, int ImageHeight*/)
        {
            this.WebSite = WebSite;
        }

        public Bitmap GetBitmap()
        {
            WebPageBitmap Shot = new WebPageBitmap(this.WebSite/*, this.ScreenWidth, this.ScreenHeight*/);
            Shot.GetIt();
            //Bitmap Pic = Shot.DrawBitmap(this.ImageHeight, this.ImageWidth);
            Bitmap Pic = Shot.DrawBitmap();
            return Pic;
        }
    }

    class WebPageBitmap
    {
        WebBrowser MyBrowser;
        string URL;
        int Height;
        int Width;

        public WebPageBitmap(string url/*, int width, int height*/)
        {
            this.URL = url;
            MyBrowser = new WebBrowser();
            MyBrowser.ScrollBarsEnabled = false;
        }

        public void GetIt()
        {
            MyBrowser.Navigate(this.URL);
            while (MyBrowser.ReadyState != WebBrowserReadyState.Complete)
            {
                Application.DoEvents();
            }

            this.Height = int.Parse(MyBrowser.Document.Body.GetAttribute("scrollHeight"));
            this.Width = int.Parse(MyBrowser.Document.Body.GetAttribute("scrollwidth"));
            MyBrowser.Size = new Size(this.Width, this.Height);
        }

        public Bitmap DrawBitmap(/*int theight, int twidth*/)
        {
            int theight = this.Height;
            int twidth = this.Width;
            Bitmap myBitmap = new Bitmap(Width, Height);
            Rectangle DrawRect = new Rectangle(0, 0, Width, Height);
            MyBrowser.DrawToBitmap(myBitmap, DrawRect);
            System.Drawing.Image imgOutput = myBitmap;
            System.Drawing.Image oThumbNail = new Bitmap(twidth, theight, imgOutput.PixelFormat);
            Graphics g = Graphics.FromImage(oThumbNail);
            g.CompositingQuality = CompositingQuality.HighSpeed;
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;
            Rectangle oRectangle = new Rectangle(0, 0, twidth, theight);
            g.DrawImage(imgOutput, oRectangle);
            try
            {
                return (Bitmap)oThumbNail;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                imgOutput.Dispose();
                imgOutput = null;
                MyBrowser.Dispose();
                MyBrowser = null;
            }
        }
    }
}