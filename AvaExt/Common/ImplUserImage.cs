using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;


using Android.Graphics;

namespace AvaExt.Common
{
    public class ImplUserImage : IUserImage
    {

        // ImageList _images = new ImageList();

        List<Bitmap> _images = new List<Bitmap>();
        List<string> _names = new List<string>();

        public ImplUserImage()
        {

        }
        public ImplUserImage(string folder)
        {
            if (ToolMobile.existsDir(folder))
            {
                string[] files = ToolMobile.getFiles(folder);
                foreach (string file in files)
                    if (System.IO.Path.GetExtension(file).ToLowerInvariant() == ".png")
                    {
                        byte[] arr_ = ToolMobile.readFileData(file);
                        setImage(System.IO.Path.GetFileName(file), BitmapFactory.DecodeByteArray(arr_, 0, arr_.Length));
                    }
            }
        }
        public bool containts(string name)
        {
            return _names.Contains(name.ToLower());
        }
        public int getIndx(string name)
        {
            return _names.IndexOf(name);
        }
        public Bitmap getImage(string name)
        {
            if (containts(name))
                return _images[getIndx(name)];
            return null;
        }

        public void setImage(string name, Bitmap image)
        {
            _names.Add(name);
            _images.Add(image);
        }


        //public ImageList getImageList()
        //{
        //    return _images;
        //}


    }
}
