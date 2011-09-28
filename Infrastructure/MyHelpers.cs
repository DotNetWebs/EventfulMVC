using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace EventfulMVC.Infrastructure
{
    public static class MyHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper helper, string src, string altText, string link)
        {
            int count = src.LastIndexOf("/");
            string src1 = src.Substring(count);
            string src2 = src.Substring(0, count);
            string src3 = "Content/support/images/thumbs/100" + src1;

            var aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", CreateUri(link));
            aTag.MergeAttribute("title", link);
            var imgTag = new TagBuilder("img");
            imgTag.MergeAttribute("src", src3);
            imgTag.MergeAttribute("alt", altText);
            aTag.InnerHtml = (MvcHtmlString.Create(imgTag.ToString(TagRenderMode.SelfClosing)).ToString());

            //var a2Tag = new TagBuilder("a");
            //a2Tag.MergeAttribute("href", CreateUri(link));
            //a2Tag.InnerHtml = link;
            //MvcHtmlString.Create(a2Tag.ToString(TagRenderMode.Normal));

            return MvcHtmlString.Create(aTag.ToString(TagRenderMode.Normal));
        }

        //TODO: detect for updated version of source images
        public static string GetThumb(string imageSource)
        {
            int count = imageSource.LastIndexOf("/");
            string imageThumbDirectory = HostingEnvironment.MapPath("~/Content/support/images/thumbs");
            string image100ThumbDirectory = HostingEnvironment.MapPath("~/Content/support/images/thumbs/100");
            string imageFile = imageSource.Substring(count);
            string thumb = imageThumbDirectory + imageFile;
            string thumb100 = image100ThumbDirectory + imageFile;

            var wc = new WebClient();
            Stream stream = wc.OpenRead(imageSource);

            if (stream != null)
            {
                var bmp = new Bitmap(stream);

                using (var m = new MemoryStream())
                {
                    bmp.Save(m, ImageFormat.Jpeg);
                    var img = System.Drawing.Image.FromStream(m);

                    if (!File.Exists(thumb))
                    {
                        img.Save(thumb);
                    }

                }
            }

            if (!File.Exists(thumb100))
            {
                var encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                Image imgPhotoVert = System.Drawing.Image.FromFile(thumb);
                Image imgPhoto = ScaleByPercent(imgPhotoVert, 50);
                imgPhoto.Save(thumb100, GetImageCodeInfo("image/jpeg"), encoderParameters);
                imgPhoto.Dispose();
            }

            return thumb;
        }

        private static Image ScaleByPercent(Image imgPhoto, int percent)
        {
            float nPercent = ((float)percent / 100);
            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            const int sourceX = 0;
            const int sourceY = 0;
            const int destX = 0;
            const int destY = 0;
            var destWidth = (int)(sourceWidth * nPercent);
            var destHeight = (int)(sourceHeight * nPercent);
            var bmPhoto = new Bitmap(destWidth, destHeight, PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution);
            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.SmoothingMode = SmoothingMode.HighQuality;
            grPhoto.CompositingQuality = CompositingQuality.HighQuality;
            grPhoto.PixelOffsetMode = PixelOffsetMode.HighQuality;
            grPhoto.DrawImage(imgPhoto, new Rectangle(destX, destY, destWidth, destHeight), new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight), GraphicsUnit.Pixel);
            grPhoto.Dispose();
            return bmPhoto;
        }

        private static ImageCodecInfo GetImageCodeInfo(string mimeType)
        {

            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            return info.FirstOrDefault(ici => ici.MimeType.Equals(mimeType, StringComparison.OrdinalIgnoreCase));
        }

        private static string CreateUri(string business)
        {
            return "http://www.visithorsham.co.uk/Business/" + Regex.Replace(business, " ", "_") + ".aspx";
        }
    }
}