
using Android.Runtime;
using Java.Interop;
using Java.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace AvaExt.Reporting
{
    public class RenderingInfo : Java.Lang.Object, ISerializable
    {

        public RenderingInfo()
        {
        }
        public RenderingInfo(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
        public int count = 1;
        public bool isDirect = false;
        public string encoding = string.Empty;
        public string replace = string.Empty;

        //public void Dispose()
        //{

        //}



        [Export("readObject", Throws = new[] {
        typeof (Java.IO.IOException),
        typeof (Java.Lang.ClassNotFoundException)})]
        private void ReadObjectDummy(Java.IO.ObjectInputStream source)
        {
            count = source.ReadInt();
            isDirect = source.ReadBoolean();
            encoding = source.ReadUTF();
            replace = source.ReadUTF();
        }

        [Export("writeObject", Throws = new[] {
        typeof (Java.IO.IOException),
        typeof (Java.Lang.ClassNotFoundException)})]
        private void WriteObjectDummy(Java.IO.ObjectOutputStream destination)
        {
            destination.WriteInt(count);
            destination.WriteBoolean(isDirect);
            destination.WriteUTF(encoding);
            destination.WriteUTF(replace);
        }
    }
}
