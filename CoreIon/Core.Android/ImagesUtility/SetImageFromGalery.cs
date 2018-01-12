using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Core.Android.ImagesUtility
{
    public class SetImageFromGalery:Activity
    {
        //private Uri fileNamePath;
        private const int PICK_IMAGE_REQUEST = 71;

        private void ChoseImage()
        {
            Intent intent = new Intent();
            intent.SetType("image/*");
            intent.SetAction(Intent.ActionGetContent);
            StartActivityForResult(Intent.CreateChooser(intent, "Select Picture"), PICK_IMAGE_REQUEST);

        }


        //protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    if (requestCode == PICK_IMAGE_REQUEST && resultCode == Result.Ok && data != null && data.Data != null)
        //    {
        //        fileNamePath = data.Data;
        //        try
        //        {
        //            Bitmap bitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, fileNamePath);
        //            imageView.SetImageBitmap(bitmap);

        //        }
        //        catch (IOException ex)
        //        {
        //            ex.PrintStackTrace();
        //        }

        //    }

        //}

    }
}