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
using AvaExt.TableOperation;
using AvaAgent;


namespace AvaGE.MobControl
{
    public class MobTreeView : ExpandableListView, IControlGlobalInit, ISelfDestructable, ExpandableListView.IOnChildClickListener, ExpandableListView.IOnGroupClickListener
    {

        ///////////////////////////////////////////////



        public bool OnGroupClick(ExpandableListView parent, View clickedView, int groupPosition, long id)
        {
            if (this.RootNode != null)
            {
                MobTreeView.Node itmGroup = this.RootNode.getChildAt(groupPosition);
                if (itmGroup != null)
                {
                    if (itmGroup.activity != null)
                        itmGroup.activity.done();

                    if (NodeClick != null)
                    {
                        NodeClick.Invoke(this, new EventArgsNode(itmGroup));
                    }
                }
            }

            return false;
        }

        public bool OnChildClick(ExpandableListView parent, View clickedView, int groupPosition, int childPosition, long id)
        {
            if (this.RootNode != null)
            {
                MobTreeView.Node itmGroup = this.RootNode.getChildAt(groupPosition);
                if (itmGroup != null)
                {
                    MobTreeView.Node itmChild = itmGroup.getChildAt(childPosition);
                    if (itmChild != null)
                    {
                        if (itmChild.activity != null)
                            itmChild.activity.done();

                        if (NodeClick != null)
                        {
                            NodeClick.Invoke(this, new EventArgsNode(itmChild));
                        }
                    }
                }
            }

            return false;
        }




        ////////////////////////////////////////////


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
            getTopView = 1,
            getParentNode = 2,
            context = 3,
            environment = 4
        }
        public delegate object RunService(ServiceCmd pCmd, object[] pArgs);

        public MobTreeView(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            init(context);

          //  ToolMobile.log("control constructor [" + GetType().FullName + "]");
        }

        string name;
        public string Name
        {
            get { return name == null ? name = ToolMobile.getFromTagName(this) : name; }

        }

        public Node SelectedNode
        {

            get
            {
                long packed_ = this.SelectedId;

                if (packed_ < 0)
                {
                    int group_ = ExpandableListView.GetPackedPositionGroup(packed_);
                    int child_ = ExpandableListView.GetPackedPositionChild(packed_);

                    if (child_ == 0)//just group selected
                    {
                        return this.rootNode.getChildAt(group_);
                    }
                    else
                    {
                        --child_;

                        Node n_ = this.rootNode.getChildAt(group_);
                        if (n_ != null)
                            return n_.getChildAt(child_);
                    }

                }
                return null;
            }
            set
            {

                if (value == null || object.ReferenceEquals(RootNode, value) || !object.ReferenceEquals(this, value.Tree))
                    return;



                if (value.isChild())
                {
                    Node parent_ = value.Parent;
                    if (parent_ != null)
                    {
                        int group_ = parent_.Index;
                        if (group_ >= 0 && group_ < RootNode.getChildCount())
                        {
                            int child_ = value.Index;
                            if (child_ >= 0 && child_ < parent_.getChildCount())
                            {
                                ExpandGroup(group_);
                                SetSelectedChild(group_, child_, true);
                            }
                        }

                    }

                }
                else
                {

                    int group_ = value.Index;
                    if (group_ >= 0 && group_ < RootNode.getChildCount())
                    {
                        SetSelectedGroup(group_);
                        ExpandGroup(group_);
                    }
                }







            }
        }


        public bool Visible
        {

            get { return this.Visibility == ViewStates.Visible; }
            set { this.Visibility = value ? ViewStates.Visible : ViewStates.Gone; }

        }

        void init(Context context)
        {
            this.rootNode = new Node(null, null);
            this.rootNode.runService = runService;
            //
            this.SetAdapter(new MobTreeViewAdapter(context, this));
            //
            //  this.ChildClick += MobTreeView_ChildClick; //dont use
            // this.ItemClick += MobTreeView_ItemClick;//dont use

            this.SetOnChildClickListener(this);
            this.SetOnGroupClickListener(this); //make tree untackable

            // this.GroupClick += MobTreeView_GroupClick;
        }

        //void MobTreeView_GroupClick(object sender, ExpandableListView.GroupClickEventArgs e)
        //{
        //    OnGroupClick(this, e.ClickedView, e.GroupPosition, e.Id);
        //}



        //void MobTreeView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        //{
        //    Toast.MakeText(ToolMobile.getContext(), "itm", ToastLength.Short);
        //}

        //void MobTreeView_ChildClick(object sender, ExpandableListView.ChildClickEventArgs e)
        //{



        //}



        object runService(ServiceCmd pCmd, object[] pArgs)
        {
            if (RootNode == null)
                return null;

            switch (pCmd)
            {
                case ServiceCmd.getTopView:
                    return this;

                case ServiceCmd.invalidate:
                    this.Invalidate();
                    break;

                case ServiceCmd.getParentNode:
                    {
                        Node node_ = pArgs != null && pArgs.Length > 0 ? pArgs[0] as Node : null;

                        Node parent_ = null;

                        foreach (Node n in RootNode.getAllSubNodes())
                        {
                            if (!n.isChild())
                                parent_ = n;

                            if (object.ReferenceEquals(n, node_) && !object.ReferenceEquals(node_, parent_))
                                return parent_;

                        }

                    }
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
        //public static Node[] Find(Node pNode, string pCode, bool pSub)
        //{
        //    List<Node> list = new List<Node>();

        //    foreach (Node node in pNode.getChildNodes())
        //    {
        //        if (node.Code == pCode)
        //            list.Add(node);
        //        if (node.getChildCount() > 0)
        //            list.AddRange(Find(node, pCode, pSub));
        //    }

        //    return list.ToArray();
        //}

        //List<string> listNodes = new List<string>();
        //public string[] getNodes()
        //{
        //    return listNodes.ToArray();
        //}
        //public void addNode(string nodeCode)
        //{
        //    listNodes.Add(nodeCode);
        //}
        //public void addNode(string[] nodeCodes)
        //{
        //    listNodes.AddRange(nodeCodes);
        //}


        void addNode(ISettings pSettings)
        {

            if (string.IsNullOrEmpty(getGlobalObjactName()))
                return;

          //  ToolMobile.log("tree [" + getGlobalObjactName() + "] read struct");

            Dictionary<string, string> dic = getNodes(pSettings);

            foreach (string nodeCode in dic.Keys)
            {
                string[] parents = ToolString.explodeList(dic[nodeCode]);
                Node lastNodes = RootNode;
                foreach (string nodeParentCode in parents)
                {
                    Node res_ = lastNodes.Search(nodeParentCode, true);

                    if (res_ == null)
                    {
                        res_ = new Node(nodeParentCode, string.Empty);
                        lastNodes.Add(res_);
                    }

                    lastNodes = res_;
                }

                if (lastNodes != null)
                    lastNodes.Add(new Node(nodeCode, string.Empty));
            }
        }




        Dictionary<string, string> getNodes(ISettings pSettings)
        {
            Dictionary<string, string> dic_ = new Dictionary<string, string>();

            if (!string.IsNullOrEmpty(getGlobalObjactName()))
            {
                ISettings s_ = pSettings.fork(getGlobalObjactName());
                s_.enumarate();
                while (s_.moveNext())
                    dic_[s_.getNameEnumer()] = s_.getStringEnumer();
            }
            return dic_;
        }

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            //
            addNode(pSettings);
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







        public void fillTree(object[][] arrGroups)
        {
            //object[]{object[]{1,2,3},object[]{5,6},object[]{6,9}}

            this.RootNode.Clear();

            foreach (object[] arr in arrGroups)
            {
                Node curNode = this.RootNode;
                for (int i = 0; i < Math.Min(2, arr.Length); ++i)
                {

                    string item = ToolCell.isNull(arr[i], string.Empty).ToString();

                    Node res_ = curNode.Search(item); // Find(curCollect, item.ToString(), false);
                    if (res_ != null)
                        curNode = res_;
                    else
                    {
                        curNode = curNode.Add(new Node(item, item));
                        curNode.Tag = arr[i];
                    }
                }
            }
        }

        public static string[] getNodeTextTree(Node treeNode)
        {

            var list = new List<string>();
            foreach (Node n in getNodeNodesTree(treeNode))
                list.Add(n.Text);

            return list.ToArray();
        }
        public static object[] getNodeTagTree(Node treeNode)
        {

            var list = new List<object>();
            foreach (Node n in getNodeNodesTree(treeNode))
                list.Add(n.Tag);

            return list.ToArray();
        }
        public static Node[] getNodeNodesTree(Node treeNode)
        {
            Node cur = treeNode;
            List<Node> list = new List<Node>();
            while (cur != null)
            {
                list.Add(cur);
                cur = cur.Parent;
            }
            list.Reverse();
            return list.ToArray();
        }
        ///////////////////////////////////////////////////////////////////





        protected override void Dispose(bool disposing)
        {
            try
            {
                base.Dispose(disposing);

                if (rootNode != null)
                    rootNode.Dispose();

                rootNode = null;

                if (this.ExpandableListAdapter != null)
                    this.ExpandableListAdapter.Dispose();
            }
            catch
            {

            }
        }


        public object[] selfDestruct()
        {
            return this.RootNode.getAllSubNodes();
        }



        public class Node : IDisposable, IControlGlobalInit, ITranslateable
        {

            public Node(string pCode, string pText)
            {
                Text = pText;
                Code = pCode;
            }

            public IActivity activity;

            public object Tag { get; set; }

            string cmd;
            public string Cmd
            {
                get
                {
                    return cmd == null ? string.Empty : cmd;
                }
                set
                {
                    cmd = value;
                    if (!string.IsNullOrEmpty(cmd))
                    {

                        if (activity != null)
                            activity.Dispose();

                        activity = new ImplActivity(cmd);

                    }
                }
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
            public string ImageKey
            {
                get { return _ImageKey; }
                set
                {
                    _ImageKey = value;

                    MobTreeView mt = this.Tree as MobTreeView;
                    if (mt != null && mt.images != null)
                    {
                        //  mt.images.getImage(ImageKey);
                    }

                }



            }


            public RunService runService;

            List<Node> list = new List<Node>();


            int level = 0;
            public int Level { get { return level; } }


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
                pNode.level = this.level + 1; //+1 level
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
            public Node Search(string pCode, bool pDeepSearch)
            {
                if (pCode == null)
                    throw new ArgumentNullException();

                object[] nodes_ = pDeepSearch ? this.getAllSubNodes() : this.getChildNodes();

                foreach (Node n in nodes_)
                    if (n.Code.ToLowerInvariant() == pCode.ToLowerInvariant())
                        return n;

                return null;
            }
            public Node Search(string pCode)
            {
                return Search(pCode, false);
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

            public Node[] getAllSubNodes()
            {
                var list_ = new List<Node>();
                _getAllSubNodes(list_);
                return list_.ToArray();
            }

            void _getAllSubNodes(List<Node> pList)
            {
                foreach (Node n in list)
                {
                    pList.Add(n);
                    n._getAllSubNodes(pList);
                }

            }

            public bool isChild()
            {
                return list.Count == 0;
            }
            public int IndexOfChild(Node pNode)
            {
                int indx_ = -1;
                foreach (Node n in list)
                {
                    ++indx_;
                    if (object.ReferenceEquals(n, pNode))
                        return indx_;
                }
                return indx_;
            }
            public int Index
            {
                get
                {
                    int indx_ = -1;

                    Node parent_ = Parent;

                    if (parent_ == null && Tree != null)
                        parent_ = Tree.RootNode;

                    if (parent_ != null)
                        return parent_.IndexOfChild(this);

                    return indx_;

                }

            }


            public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
            {
                _isGlobalInited = true;
                InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);
                //Text = pEnv.translate(Text, pSettings);

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




            public MobTreeView Tree
            {

                get
                {

                    MobTreeView t = runService(ServiceCmd.getTopView, null) as MobTreeView;
                    return t;
                }

            }

            public Node Parent
            {

                get
                {

                    Node t = runService(ServiceCmd.getParentNode, new object[] { this }) as Node;
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



            public string getTranslatingText()
            {
                return Text;
            }

            public void setTranslatingText(string pText)
            {
                Text =( pText);

                 
                runService(ServiceCmd.invalidate, new object[] { this });
            }


            public override string ToString()
            {
                return Code + (string.IsNullOrEmpty(Text) ? string.Empty : "/" + Text);
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




        public class MobTreeViewAdapter : BaseExpandableListAdapter
        {
            MobTreeView _tree;
            Context _context;

            public MobTreeViewAdapter(Context context, MobTreeView tree)
            {
                _tree = tree;
                _context = context;

            }

            public override Java.Lang.Object GetChild(int groupPosition, int childPosition)
            {
                return null;
            }
            public override long GetChildId(int groupPosition, int childPosition)
            {
                return ExpandableListView.GetPackedPositionForChild(groupPosition, childPosition + 1);
            }
            public override int GetChildrenCount(int groupPosition)
            {
                Node node_ = _tree.RootNode.getChildAt(groupPosition);

                return node_ != null ? node_.getChildCount() : 0;
            }
            public override View GetChildView(int groupPosition,
                                                int childPosition,
                                                bool isLastChild,
                                                View convertView,
                                                ViewGroup parent)
            {
                try
                {
                    Node group_ = _tree.RootNode.getChildAt(groupPosition);
                    if (group_ != null)
                    {
                        Node child_ = group_.getChildAt(childPosition);

                        if (child_ != null)
                        {
                            var item = child_.Text;

                            if (convertView == null)
                                convertView = getView(Resource.Layout.MobTreeViewChild);

                            var textBox = convertView.FindViewById<TextView>(Resource.Id.cTreeNodeText);

                            textBox.SetText(item, TextView.BufferType.Normal);

                            //textBox.SetTypeface(Android.Graphics.Typeface.Monospace, Android.Graphics.TypefaceStyle.Bold);

                            return convertView;

                            //MobLabel l = convertView as MobLabel;

                            //if (l == null)
                            //    l = new MobLabel(_context);

                            //l.Text = item;

                            //return l;
                        }
                    }
                }
                catch (Exception exc)
                {
                    ToolMobile.setExceptionInner(exc);
                }

                return null;
            }

            View getView(int pId)
            {

                LayoutInflater inf = (_context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater);
                if (inf != null)
                    return inf.Inflate(pId, null);

                return null;

            }

            public override Java.Lang.Object GetGroup(int groupPosition)
            {
                return null;
            }
            public override long GetGroupId(int groupPosition)
            {
                return ExpandableListView.GetPackedPositionForChild(groupPosition, 0);
            }
            public override View GetGroupView(int groupPosition, bool isExpanded, View convertView, ViewGroup parent)
            {
                try
                {
                    Node group_ = _tree.RootNode.getChildAt(groupPosition);
                    if (group_ != null)
                    {
                        var item = group_.Text;

                        if (convertView == null)
                            convertView = getView(Resource.Layout.MobTreeViewGroup);
                        // convertView = getView(group_.getChildCount() > 0 ? Resource.Layout.MobTreeViewGroup : Resource.Layout.MobTreeViewChild);

                        var textBox = convertView.FindViewById<TextView>(Resource.Id.cTreeNodeText);
                        textBox.Text = item;

                        return convertView;

                        //MobLabel l = convertView as MobLabel;

                        //if (l == null)
                        //    l = new MobLabel(_context);

                        //l.Text = item;

                        //return l;
                    }
                }
                catch (Exception exc)
                {
                    ToolMobile.setExceptionInner(exc);
                }

                return null;

            }
            public override bool IsChildSelectable(int groupPosition, int childPosition)
            {
                return true;
            }
            public override int GroupCount
            {
                get
                {
                    return _tree.RootNode.getChildCount();
                }
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
                try
                {
                    base.Dispose(disposing);

                    _tree = null;
                    _context = null;
                }
                catch
                {

                }
            }

        }








    }


}
