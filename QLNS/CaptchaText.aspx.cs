using System;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Text;

namespace QLNS
{
    public partial class CaptchaText : System.Web.UI.Page
    {
        Random rand = new Random();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CreateImage();
            }
        }

        public void CreateImage()
        {

            string code = GetRandomText();

            Bitmap bitmap = new Bitmap(120, 50, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Graphics g = Graphics.FromImage(bitmap);

            Pen pen = new Pen(Color.Black);

            Rectangle rect = new Rectangle(0, 0, 120, 50);

            SolidBrush b = new SolidBrush(Color.Black);

            SolidBrush blue = new SolidBrush(Color.FromArgb(118, 183, 0));

            int counter = 0;

            g.DrawRectangle(pen, rect);

            g.FillRectangle(b, rect);

            for (int i = 0; i < code.Length; i++)
            {

                g.DrawString(code[i].ToString(),

                new Font("Verdena", 10 + rand.Next(14, 18)),

                blue, new PointF(10 + counter, 10));

                counter += 20;

            }

            DrawRandomLines(g);

            Response.ContentType = "image/gif";

            bitmap.Save(Response.OutputStream, ImageFormat.Gif);

            g.Dispose();

            bitmap.Dispose();

        }

        private string GetRandomText()
        {

            StringBuilder randomText = new StringBuilder();
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz123456";

            Random r = new Random();

            for (int j = 0; j <= 3; j++)
            {

                randomText.Append(alphabets[r.Next(alphabets.Length)]);

            }

            Session["Code"] = randomText.ToString();

            return Session["Code"] as String;

        }

        private void DrawRandomLines(Graphics g)
        {

            SolidBrush green = new SolidBrush(Color.FromArgb(220, 220, 220));

            for (int i = 0; i < 12; i++)
            {

                g.DrawLines(new Pen(green, rand.Next(1, 2)), GetRandomPoints());

            }

        }

        private Point[] GetRandomPoints()
        {

            Point[] points = { 
                                new Point(rand.Next(rand.Next(5, 10), 120), rand.Next(rand.Next(5, 10), 120)),
                                new Point(rand.Next(rand.Next(5, 10), 100), rand.Next(rand.Next(5, 10), 100)) 
                             };

            return points;

        }
    }
}
