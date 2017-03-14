using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;

namespace DtCms.Common
{
    public class LoadImage
    {
        /// <summary>
        ///  生成缩略图 静态方法   
        /// </summary>
        /// <param name="pathImageFrom"> 源图的路径(含文件名及扩展名) </param>
        /// <param name="width"> 欲生成的缩略图 "画布" 的宽度(像素值) </param>
        /// <param name="height"> 欲生成的缩略图 "画布" 的高度(像素值) </param>
        public static void GenThumbnail(string pathImageFrom, int width, int height)
        {
            Image imageFrom = null;

            //检查图片路径是否正确
            if (File.Exists(HttpContext.Current.Server.MapPath(pathImageFrom)))
            {
                imageFrom = Image.FromFile(HttpContext.Current.Server.MapPath(pathImageFrom));
            }

            if (imageFrom == null)
            {
                return;
            }

            // 源图宽度及高度
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;

            // 生成的缩略图实际宽度及高度
            int bitmapWidth = width;
            int bitmapHeight = height;

            // 生成的缩略图在上述"画布"上的位置
            int X = 0;
            int Y = 0;

            // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置
            if (bitmapHeight * imageFromWidth > bitmapWidth * imageFromHeight)
            {
                bitmapHeight = imageFromHeight * width / imageFromWidth;
                Y = (height - bitmapHeight) / 2;
            }
            else
            {
                bitmapWidth = imageFromWidth * height / imageFromHeight;
                X = (width - bitmapWidth) / 2;
            }

            //  创建画布
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);

            // 清除整个绘图面并以透明背景色填充
            g.Clear(Color.White);   //Transparent
            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // 指定高质量、低速度呈现。
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。
            g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight), new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);

            try
            {
                //经测试 .jpg 格式缩略图大小与质量等最优
                //bmp.Save(pathImageTo, ImageFormat.Jpeg);
                bmp.Save(HttpContext.Current.Response.OutputStream, ImageFormat.Jpeg);
            }
            catch
            {
            }
            finally
            {
                //显式释放资源
                imageFrom.Dispose();
                bmp.Dispose();
                g.Dispose();
            }
        }
    }
}
