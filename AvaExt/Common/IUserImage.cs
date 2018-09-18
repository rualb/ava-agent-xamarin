using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Android.Graphics;


namespace AvaExt.Common
{
    public interface IUserImage
    {

        bool containts(string name);
        Bitmap getImage(string name);
        int getIndx(string name);
        void setImage(string name, Bitmap image);
       // ImageList getImageList();
    }
}
