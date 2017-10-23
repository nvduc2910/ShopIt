using System;
using System.IO;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Widget;
using Java.Lang;
using Environment = Android.OS.Environment;
using Exception = System.Exception;
using File = Java.IO.File;
using FileNotFoundException = Java.IO.FileNotFoundException;
using IOException = Java.IO.IOException;
using Math = System.Math;
using Orientation = Android.Media.Orientation;
using String = System.String;
using Uri = Android.Net.Uri;

namespace ShopIt.Droid.Extensions
{
    public static class BitmapExtensions
    {
        public static string Tag = "BitmapExtensions";

        public static string GetImagesPath(this Context context, string imagesFolder)
        {
            if (Environment.ExternalStorageState == Environment.MediaMounted)
            {
                return Environment.ExternalStorageDirectory
                       + "/Android/data/"
                       + "com.ShopIt.droid"
                       + "/"
                       + imagesFolder;
            }
            else
            {
                return string.Empty;
            }
        }

        public static File GetImageFile(this Context context, string imagesFolder, String imageFileName)
        {
            if (Environment.ExternalStorageState == Environment.MediaMounted)
            {
                return new File(context.GetImagesPath(imagesFolder) + File.Separator + imageFileName);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create a File for saving an image or video
        /// </summary>
        /// <param name="context"></param>
        /// <param name="imagesFolder">Images folder Name</param>
        /// <param name="fileExtension">File Extension</param>
        /// <returns></returns>
        public static File GetOutputMediaFile(this Context context, string imagesFolder, string fileExtension = "png")
        {
            if (Environment.ExternalStorageState != Environment.MediaMounted)
                return null;

            var mediaStorageDir = new File(context.GetImagesPath(imagesFolder));

            if (!mediaStorageDir.Exists())
            {
                if (!mediaStorageDir.Mkdirs())
                    return null;
            }

            // Create a media file name
            var timeStamp = DateTime.UtcNow.ToString("ddMMyyyy_HHmmss");
            var mImageName = timeStamp + "." + fileExtension;
            var mediaFile = new File(mediaStorageDir.Path + File.Separator + mImageName);
            return mediaFile;
        }
        public static File GetOutputMediaFileName(this Context context, string imagesFolder,string filename)
        {
            if (Environment.ExternalStorageState != Environment.MediaMounted)
                return null;

            var mediaStorageDir = new File(context.GetImagesPath(imagesFolder));

            if (!mediaStorageDir.Exists())
            {
                if (!mediaStorageDir.Mkdirs())
                    return null;
            }

            var mediaFile = new File(mediaStorageDir.Path + File.Separator + filename);
            return mediaFile;
        }

        public static string StoreImage(this Bitmap image, Context context, string imagesFolder,string imageFileName = "")
        {
            File pictureFile = null;
            if (String.IsNullOrEmpty(imageFileName))
            {
                pictureFile = context.GetOutputMediaFile(imagesFolder);
            }
            else
            {
                pictureFile = context.GetOutputMediaFileName(imagesFolder, imageFileName);
            }

            if (pictureFile == null)
            {
                Log.Debug(Tag, "Error creating media file, check storage permissions: ");
                return "";
            }
            try
            {
                var stream = new FileStream(pictureFile.AbsolutePath, FileMode.OpenOrCreate);
                image.Compress(Bitmap.CompressFormat.Png, 90, stream);
                stream.Close();
                return pictureFile.Name;
            }
            catch (FileNotFoundException ex)
            {
                Log.Debug(Tag, "File not found: " + ex.Message);
                return "";
            }
            catch (IOException exx)
            {
                Log.Debug(Tag, "Error accessing file: " + exx.Message);
                return "";
            }
        }

        public static Bitmap LoadImage(this Context context, string imagesFolder, string imageFileName, BitmapFactory.Options options = null)
        {
            if (Environment.ExternalStorageState != Environment.MediaMounted)
                return null;
            if (options != null)
            {

                Bitmap bm = BitmapFactory.DecodeFile(GetImagesPath(context,imagesFolder) + File.Separator + imageFileName, options);
                Matrix mtx = new Matrix();
                ExifInterface exif = new ExifInterface(GetImagesPath(context, imagesFolder) + File.Separator + imageFileName);
                string orientation = exif.GetAttribute(ExifInterface.TagOrientation);

                switch (orientation)
                {
                    case "6": // portrait
                        mtx.PreRotate(90);
                        bm = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, mtx, false);
                        mtx.Dispose();
                        mtx = null;
                        break;
                    case "1": // landscape
                        break;
                    default:
                        mtx.PreRotate(90);
                        bm = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, mtx, false);
                        mtx.Dispose();
                        mtx = null;
                        break;
                }
                return bm;
            }
            return BitmapFactory.DecodeFile(context.GetImagesPath(imagesFolder) + File.Separator + imageFileName);
        }

        public static Bitmap LoadImageAndResize(this Context context, string imagesFolder, string imageFileName, int width, int height)
        {
            if (Environment.ExternalStorageState != Environment.MediaMounted)
            {
                return null;
            }
                
            
            var fileName = GetImagesPath(context, imagesFolder) + File.Separator + imageFileName;
            BitmapFactory.Options options = new BitmapFactory.Options { InJustDecodeBounds = true };
            BitmapFactory.DecodeFile(fileName, options);

            int outHeight = options.OutHeight;
            int outWidth = options.OutWidth;
            int inSampleSize = 1;

            if (outHeight > height || outWidth > width)
            {
                inSampleSize = outWidth > outHeight
                                   ? outHeight / height
                                   : outWidth / width;
            }

            options.InSampleSize = inSampleSize;
            options.InJustDecodeBounds = false;
            options.InPreferredConfig = Bitmap.Config.Rgb565;

            Bitmap bm = BitmapFactory.DecodeFile(fileName, options);
            Matrix mtx = new Matrix();
            ExifInterface exif = new ExifInterface(GetImagesPath(context, imagesFolder) + File.Separator + imageFileName);
            string orientation = exif.GetAttribute(ExifInterface.TagOrientation);

            switch (orientation)
            {
                case "6": // portrait
                    mtx.PreRotate(90);
                    bm = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, mtx, false);
                    mtx.Dispose();
                    mtx = null;
                    break;
                case "1": // landscape
                    break;
                default:
                    mtx.PreRotate(90);
                    bm = Bitmap.CreateBitmap(bm, 0, 0, bm.Width, bm.Height, mtx, false);
                    mtx.Dispose();
                    mtx = null;
                    break;
            }

            return bm;
        }

        public static Bitmap HandleCapturedImage(this Bitmap bitmap, String photoPath)
        {
            var ei = new ExifInterface(photoPath);
            var orientation = ei.GetAttributeInt(ExifInterface.TagOrientation, (int)Orientation.Normal);

            switch (orientation)
            {
                case (int)Orientation.Rotate90:
                    return bitmap.RotateBitmap(90);

                case (int)Orientation.Rotate180:
                    return bitmap.RotateBitmap(180);

                default:
                    return bitmap;
            }
        }

        public static Bitmap RotateBitmap(this Bitmap source, float angle)
        {
            var matrix = new Matrix();
            matrix.PostRotate(angle);
            return Bitmap.CreateBitmap(source, 0, 0, source.Width, source.Height, matrix, true);
        }

        public static Bitmap ResizeBitmap(this Bitmap bm, int newWidth, int newHeight)
        {
            var width = bm.Width;
            var height = bm.Height;
            var scaleWidth = ((float)newWidth) / width;
            var scaleHeight = ((float)newHeight) / height;
            var matrix = new Matrix();
            matrix.PostScale(scaleWidth, scaleHeight);
            var resizedBitmap = Bitmap.CreateBitmap(bm, 0, 0, width, height, matrix, false);
            return resizedBitmap;
        }

        public static Bitmap ResizeBitmapFromPath(this string photoPath, int newWidth, int newHeight)
        {
            try
            {
                var bmOptions = new BitmapFactory.Options { InJustDecodeBounds = true };
                BitmapFactory.DecodeFile(photoPath, bmOptions);
                var photoW = bmOptions.OutWidth;
                var photoH = bmOptions.OutHeight;
                var scaleFactor = 1;

                if ((newWidth > 0) || (newHeight > 0))
                {
                    scaleFactor = Math.Min(photoW / newWidth, photoH / newHeight);
                }

                bmOptions.InJustDecodeBounds = false;
                bmOptions.InSampleSize = scaleFactor;
                bmOptions.InPurgeable = true;
                return BitmapFactory.DecodeFile(photoPath, bmOptions);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Bitmap ResizeBitmapFromUri(this Uri uri, int newWidth, int newHeight)
        {
            try
            {
                var bmOptions = new BitmapFactory.Options { InJustDecodeBounds = true };
                BitmapFactory.DecodeFile(uri.EncodedPath, bmOptions);
                var photoW = bmOptions.OutWidth;
                var photoH = bmOptions.OutHeight;
                var scaleFactor = 1;

                if ((newWidth > 0) || (newHeight > 0))
                {
                    scaleFactor = Math.Min(photoW / newWidth, photoH / newHeight);
                }

                bmOptions.InJustDecodeBounds = false;
                bmOptions.InSampleSize = scaleFactor;
                bmOptions.InPurgeable = true;
                return BitmapFactory.DecodeFile(uri.EncodedPath, bmOptions);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static string GetDominantColor(this Bitmap bitmap)
        {
            if (null == bitmap)
                return "#c7c7cd";

            var redBucket = 0;
            var greenBucket = 0;
            var blueBucket = 0;
            var alphaBucket = 0;

            var hasAlpha = bitmap.HasAlpha;
            var pixelCount = bitmap.Width * bitmap.Height;
            var pixels = new int[pixelCount];
            bitmap.GetPixels(pixels, 0, bitmap.Width, 0, 0, bitmap.Width, bitmap.Height);

            for (int y = 0, h = bitmap.Height; y < h; y++)
            {
                for (int x = 0, w = bitmap.Width; x < w; x++)
                {
                    var color = pixels[x + y * w]; // x + y * width
                    redBucket += (color >> 16) & 0xFF; // Color.red
                    greenBucket += (color >> 8) & 0xFF; // Color.greed
                    blueBucket += (color & 0xFF); // Color.blue
                    if (hasAlpha) alphaBucket += (color >> 24); // Color.alpha
                }
            }

            var intColor = Color.Rgb(
                redBucket / pixelCount,
                greenBucket / pixelCount,
                blueBucket / pixelCount);
            var rs = "#" + Integer.ToHexString(intColor).Substring(2);
            return rs;
        }

        public static byte[] Bytes(this Bitmap bitmap)
        {
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Jpeg, 90, stream);
            return stream.ToArray();
        }

        public static int ByteSizeOf(Bitmap data)
        {
            if (Build.VERSION.SdkInt < Build.VERSION_CODES.HoneycombMr1)
            {
                return data.RowBytes * data.Height;
            }
            else if (Build.VERSION.SdkInt < Build.VERSION_CODES.Kitkat)
            {
                return data.ByteCount;
            }
            else
            {
                return data.AllocationByteCount;
            }
        }

        public static Bitmap Square(this Bitmap bitmap, bool fromTop = false)
        {
            if (!fromTop)
            {
                if (bitmap.Width >= bitmap.Height)
                {
                    return Bitmap.CreateBitmap(
                        bitmap,
                        bitmap.Width / 2 - bitmap.Height / 2,
                        0,
                        bitmap.Height,
                        bitmap.Height
                        );
                }
                else
                {
                    return Bitmap.CreateBitmap(
                        bitmap,
                        0,
                        bitmap.Height / 2 - bitmap.Width / 2,
                        bitmap.Width,
                        bitmap.Width
                        );
                }
            }
            else
            {
                return bitmap.Width >= bitmap.Height
                    ? Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Height, bitmap.Height)
                    : Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Width);
            }
        }

        public static Android.Hardware.Camera.Size GetPictureSize(this Android.Hardware.Camera.Parameters p, int w)
        {
            var sizes = p.SupportedPictureSizes;
            var sizes2 = p.SupportedPreviewSizes;
            var offset = Integer.MaxValue;
            Android.Hardware.Camera.Size rs = null;

            if (sizes != null)
            {
                foreach (var size in sizes)
                {
                    foreach (var size2 in sizes2)
                    {
                        if (size.Width == size2.Width && size.Height == size2.Height)
                        {
                            int curSize = 0;

                            if (size.Width > size.Height)
                            {
                                curSize = size.Width;
                            }
                            else
                            {
                                curSize = size.Height;
                            }

                            if (size.Width >= w && size.Height >= w && (curSize - w) < offset)
                            {
                                offset = curSize - w;
                                rs = size;
                            }
                        }
                    }
                }
            }

            return rs;
        }

        public static Android.Hardware.Camera.Size GetOptimalPictureSize(this Android.Hardware.Camera.Parameters p, int w)
        {
            var sizes = p.SupportedPictureSizes;
            int offset = Integer.MaxValue;
            Android.Hardware.Camera.Size rs = null;

            if (sizes != null)
            {
                foreach (var size in sizes)
                {

                    if (size.Width >= size.Height)
                    {
                        int curSize = size.Width;

                        if (size.Height >= w && size.Height >= w && (curSize - w) < offset)
                        {
                            offset = curSize - w;
                            rs = size;
                        }
                    }
                }
            }

            return rs;
        }

        public static Android.Hardware.Camera.Size GetOptimalPreviewSize(this Android.Hardware.Camera.Parameters p,
            Android.Hardware.Camera.Size pictureSize)
        {
            var sizes = p.SupportedPreviewSizes;
            float pictureRatio = pictureSize.Width / pictureSize.Height;
            int offset = Integer.MaxValue;
            Android.Hardware.Camera.Size rs = null;

            if (sizes != null)
            {
                foreach (var size in sizes)
                {
                    float tempRatio = size.Width / size.Height;

                    if (tempRatio == pictureRatio)
                    {
                        if (rs == null)
                        {
                            rs = size;
                        }
                        else
                        {
                            int tempOffset = Math.Abs(size.Width - pictureSize.Height);

                            if (tempOffset < offset)
                            {
                                offset = tempOffset;
                                rs = size;
                            }
                        }
                    }
                }
            }

            return rs;
        }

        /**
        * Gets the Amount of Degress of rotation using the exif integer to determine how much
        * we should rotate the image.
        * @param exifOrientation - the Exif data for Image Orientation
        * @return - how much to rotate in degress
        */
        public static int ExifToDegrees(Orientation exifOrientation)
        {
            if (exifOrientation == Orientation.Rotate90) { return 90; }
            else if (exifOrientation == Orientation.Rotate180) { return 180; }
            else if (exifOrientation == Orientation.Rotate270) { return 270; }
            return 0;
        }

        /**
         * Handles pre V19 uri's
         * @param context
         * @param contentUri
         * @return
         */
        public static string GetPathForPreV19(Context context, Uri contentUri)
        {
            var proj = new[] { MediaStore.Images.Media.InterfaceConsts.Data };
            var cursor = context.ContentResolver.Query(contentUri, proj, null, null, null);

            if (cursor != null)
            {
                string res = null;

                if (cursor.MoveToFirst())
                {
                    int column_index = cursor.GetColumnIndexOrThrow(MediaStore.Images.Media.InterfaceConsts.Data);
                    res = cursor.GetString(column_index);
                }

                cursor.Close();

                if (string.IsNullOrEmpty(res))
                {
                    return contentUri.Path;
                }
                else
                {
                    return res;
                }

            }
            else
            {
                return contentUri.Path;
            }


        }

        /**
         * Handles V19 and up uri's
         * @param context
         * @param contentUri
         * @return path
         */
        public static string GetPathForV19AndUp(Context context, Uri contentUri)
        {
            var wholeId = DocumentsContract.GetDocumentId(contentUri);

            // Split at colon, use second item in the array
            var id = wholeId.Split(':')[1];
            var column = new[] { MediaStore.Images.Media.InterfaceConsts.Data };

            // where id is equal to
            var sel = MediaStore.Images.Media.InterfaceConsts.Id + "=?";
            var cursor = context.ContentResolver.
                Query(MediaStore.Images.Media.ExternalContentUri,
                    column, sel, new[] { id }, null);

            if (cursor != null)
            {
                var filePath = "";
                var columnIndex = cursor.GetColumnIndex(column[0]);

                if (cursor.MoveToFirst())
                {
                    filePath = cursor.GetString(columnIndex);
                }

                cursor.Close();

                if (string.IsNullOrEmpty(filePath))
                {
                    return contentUri.Path;
                }
                else
                {
                    return filePath;
                }
            }
            else
            {
                return contentUri.Path;
            }
        }

        public static string GetRealPathFromUri(Context context, Uri contentUri)
        {
            var uriString = Java.Lang.String.ValueOf(contentUri);
            var goForKitKat = uriString.Contains("com.android.providers");

            if (Build.VERSION.SdkInt >= Build.VERSION_CODES.Kitkat && goForKitKat)
            {
                return GetPathForV19AndUp(context, contentUri);
            }
            else
            {

                return GetPathForPreV19(context, contentUri);
            }
        }
    }
}