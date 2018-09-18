using System;
using System.Collections.Generic;
using System.Text;
using AvaExt.AndroidEnv.ControlsBase;
using AvaExt.ControlOperation;
using AvaExt.Settings;
using AvaExt.Common;
using AvaExt.Translating.Tools;
using Android.Content;
using Android.Util;
using Android.Widget;
using Android.Views;
using AvaAgent;


namespace AvaGE.MobControl
{
    public class MobListView : ListView, IControlGlobalInit, ISelfDestructable
    {
        public IUserImage images;

        public class EventArgsNode : EventArgs
        {

            public EventArgsNode(Node pNode)
            {
                Node = pNode;
            }

            public Node Node { get; set; }
        }

        public event EventHandler<EventArgsNode> NodeClick;

        public enum ServiceCmd
        {
            invalidate = 0,
            getTop = 1,
            context = 3,
            environment = 4
        }
        public delegate object RunService(ServiceCmd pCmd, object[] pArgs);

        public MobListView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init(context);


        }

        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }

        void init(Context context)
        {
            this.rootNode = new Node(null, null);
            this.rootNode.runService = runService;
            //
            this.Adapter = (new MobListViewAdapter(context, this));
            //
            this.ItemClick += MobListView_ItemClick;
        }

        void MobListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            if (this.RootNode == null)
                return;

            MobListView.Node itmChild = this.RootNode.getChildAt(e.Position);
            if (itmChild == null)
                return;


            if (itmChild.activity != null)
                itmChild.activity.done();

            if (NodeClick != null)
            {
                NodeClick.Invoke(this, new EventArgsNode(itmChild));
            }
        }




        object runService(ServiceCmd pCmd, object[] pArgs)
        {
            if (RootNode == null)
                return null;

            switch (pCmd)
            {
                case ServiceCmd.getTop:
                    return this;

                case ServiceCmd.invalidate:
                    this.Invalidate();
                    break;


            }


            return null;
        }

        void rootNode_Changed(object sender, EventArgs e)
        {

        }

        Node rootNode;
        public Node RootNode { get { return rootNode; } }

        ///////////////////////////////////////////////////////////////////
        public static bool ContainsKey(Node pNode, string pCode)
        {
            return pNode.Search(pCode) != null;
        }


        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            //
            images = pEnv.getImages();

            //
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);
            //Text = pEnv.translate(Text, pSettings);
        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
        }

        public virtual string getGlobalObjactName()
        {
            return Name;
        }



        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }







        public void fillList(object[] arr)
        {
            if (this.RootNode == null)
                return;

            this.RootNode.Clear();

            foreach (object item in arr)
            {
                Node res_ = this.RootNode.Search(item.ToString());
                if (res_ == null)
                    this.RootNode.Add(new Node(item.ToString(), item.ToString()));
            }

        }


        ///////////////////////////////////////////////////////////////////





        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (rootNode != null)
                rootNode.Dispose();

            rootNode = null;

            if (this.Adapter != null)
                this.Adapter.Dispose();
        }


        public object[] selfDestruct()
        {
            return this.RootNode.getChildNodes();
        }



        public class Node : IDisposable, IControlGlobalInit, ITranslateable
        {

            public IActivity activity;


            public override string ToString()
            {
                return Code + (string.IsNullOrEmpty(Text) ? string.Empty : "/" + Text);
            }

            public Node(string pCode, string pText)
            {
                Text = pText;
                Code = pCode;
            }

            public RunService runService;

            List<Node> list = new List<Node>();



            void cmdInvalidate(Node pSender)
            {
                if (runService != null)
                    runService(ServiceCmd.invalidate, new object[] { pSender });
            }

            public Node Add(string pCode, string pText)
            {
                return Add(new Node(pCode, pText));
            }
            public Node Add(Node pNode)
            {
                if (pNode == null)
                    throw new ArgumentNullException();

                list.Add(pNode); //add to collection

                pNode.runService = runService; //service

                cmdInvalidate(this);

                return pNode;
            }




            public void Remove(Node pNode)
            {
                pNode.Dispose();

                list.Remove(pNode);

                cmdInvalidate(pNode);
            }
            public Node Search(string pCode)
            {
                if (pCode == null)
                    throw new ArgumentNullException();

                object[] nodes_ = this.getChildNodes();

                foreach (Node n in nodes_)
                    if (n.Code.ToLowerInvariant() == pCode.ToLowerInvariant())
                        return n;

                return null;
            }

            public Node[] getChildNodes()
            {
                return list.ToArray();
            }

            public int getChildCount()
            {
                return list.Count;
            }

            public Node getChildAt(int pIndx)
            {
                return pIndx >= 0 && pIndx < list.Count ? list[pIndx] : null;
            }




            string code;
            public string Code
            {
                get
                {
                    return code == null ? string.Empty : code;
                }
                set
                {
                    code = value;
                }
            }

            string text;
            public string Text
            {
                get
                {
                    return text == null ? string.Empty : text;
                }
                set
                {
                    text = value; cmdInvalidate(this);
                }
            }




            string _ImageKey = string.Empty;



            public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
            {
                _isGlobalInited = true;
                InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);
                //Text = pEnv.translate(Text, pSettings);
                Text = pEnv.translate(Text);
            }

            public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
            {
                InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
            }

            public virtual string getGlobalObjactName()
            {
                return Code;
            }


            bool _isGlobalInited = false;
            public bool isGlobalInited()
            {
                return _isGlobalInited;
            }


            public string ImageKey
            {
                get { return _ImageKey; }
                set
                {
                    _ImageKey = value;

                    MobListView mt = this.TopVeiw as MobListView;
                    if (mt != null && mt.images != null)
                    {
                        //  mt.images.getImage(ImageKey);
                    }

                }



            }

            public MobListView TopVeiw
            {

                get
                {

                    MobListView t = runService(ServiceCmd.getTop, null) as MobListView;
                    return t;
                }

            }



            public virtual void Dispose()
            {

                runService = null; //service

                if (list != null)
                {
                    if (list.Count > 0)
                        foreach (Node n in list)
                            n.Dispose();

                    list.Clear();
                }

                list = null;

                if (activity != null)
                    activity.Dispose();

                activity = null;
            }

            public void Clear()
            {

                if (list == null)
                    return;

                foreach (Node n in list)
                    n.Dispose();

                list.Clear();

                runService(ServiceCmd.invalidate, null);

            }

            string cmd;
            public string CmdText
            {
                get { return cmd == null ? string.Empty : cmd; }
                set
                {
                    cmd = value; if (!string.IsNullOrEmpty(cmd))
                    {

                        if (activity != null)
                            activity.Dispose();

                        activity = new ImplActivity(cmd);

                    }
                }
            }

            public string getTranslatingText()
            {
                return Text;
            }

            public void setTranslatingText(string pText)
            {
                Text = pText;
                runService(ServiceCmd.invalidate, new object[] { this });
            }

            class ImplActivity : IActivity
            {
                string cmd;
                IActivity activity;

                public ImplActivity(string pCmd)
                {
                    cmd = pCmd;
                }


                public object done()
                {
                    if (activity == null && !string.IsNullOrEmpty(cmd) && ToolMobile.getEnvironment() != null)
                        activity = ToolMobile.getEnvironment().toActivity(cmd, null);

                    if (activity != null)
                        return activity.done();


                    return null;
                }

                public void Dispose()
                {
                    if (activity != null)
                        activity.Dispose();

                    activity = null;
                }
            }

        }

    


        public class MobListViewAdapter : BaseAdapter
        {
            MobListView _list;
            Context _context;

            public MobListViewAdapter(Context context, MobListView tree)
            {
                _list = tree;
                _context = context;

            }

 
             
             View getView(int pId)
            {

                LayoutInflater inf = (_context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater);
                if (inf != null)
                    return inf.Inflate(pId, null);

                return null;

            }

     
 
            public override bool HasStableIds
            {
                get
                {
                    return true;
                }
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);

                _list = null;
                _context = null;

            }


            public override int Count
            {
                get { return _list.RootNode.getChildCount(); }
            }

            public override Java.Lang.Object GetItem(int position)
            {
                return null;
            }

            public override long GetItemId(int position)
            {
                return position;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var item = _list.RootNode.getChildAt(position).Text;

                if (convertView == null)
                    convertView = getView(Resource.Layout.MobListViewItem);

                var textBox = convertView.FindViewById<TextView>(Resource.Id.text);
                textBox.SetText(item, TextView.BufferType.Normal);

                return convertView;

                //MobLabel l = convertView as MobLabel;

                //if (l == null)
                //    l = new MobLabel(_context);

                //l.Text = item;

                //return l;
            }
        }





    }


}
