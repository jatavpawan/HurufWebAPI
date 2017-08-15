using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Huruf.BAL.Repository
{
    public enum FileType
    {
        UserProfile = 1, BlogImage = 2

    }
    public class ResizeImage
    {

        public string SaveImage(Stream postedFile, int maxWidth, int maxHeight, string savelocation, HttpContext context, string filename)
        {
           

            System.Drawing.Bitmap originalImage = new System.Drawing.Bitmap(postedFile);
            int newWidth = originalImage.Width;
            int newHeight = originalImage.Height;
            double aspectRatio = (double)originalImage.Width / (double)originalImage.Height;

            if (newWidth < maxWidth && newHeight < maxHeight)
            {

            }
            else if (newWidth < maxWidth && newHeight > maxHeight)
            {
                newWidth = maxWidth;
                newHeight = (int)Math.Round(newWidth / aspectRatio);
                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)Math.Round(newHeight * aspectRatio);

                }
            }
            else if (newWidth > maxWidth && newHeight < maxHeight)
            {
                newHeight = maxHeight;
                newWidth = (int)Math.Round(newHeight * aspectRatio);
                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)Math.Round(newWidth / aspectRatio);
                }
            }
            else if (newWidth > maxWidth && newHeight > maxHeight)
            {
                newWidth = maxWidth;
                newHeight = (int)Math.Round(newWidth / aspectRatio);
                if (newHeight > maxHeight)
                {
                    newHeight = maxHeight;
                    newWidth = (int)Math.Round(newHeight * aspectRatio);

                }

                newHeight = maxHeight;
                newWidth = (int)Math.Round(newHeight * aspectRatio);
                if (newWidth > maxWidth)
                {
                    newWidth = maxWidth;
                    newHeight = (int)Math.Round(newWidth / aspectRatio);
                }
            }


            var image = System.Drawing.Image.FromStream(postedFile);
            var thumbnailBitmap = new System.Drawing.Bitmap(newWidth, newHeight);
            var thumbnailGraph = System.Drawing.Graphics.FromImage(thumbnailBitmap);
            thumbnailGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            thumbnailGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            thumbnailGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

            var imageRectangle = new System.Drawing.Rectangle(0, 0, newWidth, newHeight);

            thumbnailGraph.DrawImage(image, imageRectangle);
            string base64String;
            using (MemoryStream m = new MemoryStream())
            {
                thumbnailBitmap.Save(m, image.RawFormat);
                byte[] imageBytes = m.ToArray();

                // Convert byte[] to Base64 String
                base64String = Convert.ToBase64String(imageBytes);

            }
            //   string g=thumbnailBitmap.ToString();

            // thumbnailBitmap.Save(context.Server.MapPath(savelocation + "/" + filename), image.RawFormat);

            thumbnailGraph.Dispose();
            thumbnailBitmap.Dispose();
            image.Dispose();
            return base64String;
        }

        public string ImageToBase64(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, image.RawFormat);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
    }


}

