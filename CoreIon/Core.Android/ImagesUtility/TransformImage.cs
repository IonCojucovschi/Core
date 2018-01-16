using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Core.Android.ImagesUtility
{
   public class TransformImage
    {
        byte[] byteIMage;
        Bitmap ImageBitmap;



        public TransformImage(Bitmap imgbtm)
        {
            ImageBitmap = imgbtm;
        }

        public byte[] BitmapToByte(Bitmap btmp)
        {
            using (var bite = new System.IO.MemoryStream())
            {
                btmp.Compress(Bitmap.CompressFormat.Jpeg, 0, bite);
                byteIMage = bite.ToArray();
            }
            return byteIMage;
        }


    }
}