using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace prjAdmin.Models
{
    public class CCaptcha
    {
        // 產生驗證碼 設定CodeLength
        public static string CreateRandomCode(int CodeLength)
        {
            int rand;
            char code;
            string randomCode = "";

            for (int i = 0; i < CodeLength; i++)
            {
                byte[] buffer = Guid.NewGuid().ToByteArray();
                int seed = BitConverter.ToInt32(buffer, 0);
                Random random = new Random(seed);
                rand = random.Next();

                if (rand % 3 == 1)
                {
                    code = (char)('A' + (char)(rand % 26));
                }
                else if (rand % 3 == 2)
                {
                    code = (char)('a' + (char)(rand % 26));
                }
                else
                {
                    code = (char)('0' + (char)(rand % 10));
                }

                randomCode += code.ToString();
            }

            return randomCode;
        }

        public static byte[] CreatImage(string strValidCode)
        {            
            int RandomAngle = 30;
            int MapWidth = (int)(strValidCode.Length * 25);
            MemoryStream ms = new MemoryStream();
            using (Bitmap map = new Bitmap(MapWidth, 40))
            {
                // 背景
                using (Graphics graphics = Graphics.FromImage(map))
                {
                    graphics.Clear(Color.Honeydew);
                    graphics.DrawRectangle(new Pen(Color.Black, 0), map.Width - 1, map.Height - 1, 1, 1);
                    graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    Random rand = new Random();

                    //線條
                    Pen greenPen = new Pen(Color.MediumAquamarine, 0.1f);
                    for (int i = 0; i < 2; i++)
                    {
                        int x1 = rand.Next(MapWidth);
                        int y1 = rand.Next(28);
                        int x2 = rand.Next(MapWidth);
                        int y2 = rand.Next(28);
                        graphics.DrawLine(greenPen, x1, y1, x2, y2);
                    }

                    //斑點
                    Pen grayPen = new Pen(Color.LightGray, 0);
                    for (int i = 0; i < 50; i++)
                    {
                        int x = rand.Next(0, MapWidth);
                        int y = rand.Next(0, map.Height);
                        graphics.DrawRectangle(grayPen, x, y, 1, 1);
                    }
                    //===============================================================================

                    //旋轉
                    char[] chars = strValidCode.ToCharArray();

                    //code置中
                    StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;

                    //code顏色
                    Color[] color = { Color.Turquoise, Color.LightSeaGreen, Color.MediumTurquoise, Color.PaleTurquoise, Color.Teal, Color.DarkCyan, Color.Aquamarine };

                    //字型
                    string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "Consolas", "Consolas" };
                    for (int i = 0; i < chars.Length; i++)
                    {
                        int cindex = rand.Next(7);
                        int findex = rand.Next(6);
                        Font f = new Font(font[findex], 24, FontStyle.Bold);
                        Brush brush = new SolidBrush(color[cindex]);
                        Point point = new Point(10, 8);

                        float angle = rand.Next(-RandomAngle, RandomAngle);
                        graphics.TranslateTransform(point.X, point.Y);
                        graphics.RotateTransform(angle);
                        graphics.DrawString(chars[i].ToString(), f, brush, point, format);
                        graphics.RotateTransform(-angle);
                        graphics.TranslateTransform(13, -point.Y);
                    }

                    map.Save(ms, ImageFormat.Jpeg);
                }

                return ms.GetBuffer();                
            }
        }
    }
}
