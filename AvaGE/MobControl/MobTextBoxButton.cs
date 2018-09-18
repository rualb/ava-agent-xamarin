using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.ControlOperation;
using MobExt.Common;
using MobExt.TableOperation;
using MobExt.Settings;

namespace MobGE.MobControl
{
    public partial class MobTextBoxButton : UserControl, IControlBind, IControlGlobalInit
    {
        string _text = string.Empty;
        public MobTextBoxButton()
        {
            InitializeComponent();


            this.DataBindings.CollectionChanged += new CollectionChangeEventHandler(DataBindings_CollectionChanged);
            this.textBox.Validated += new EventHandler(validated);
            this.textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);

        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                validated(this, EventArgs.Empty);
        }


        void DataBindings_CollectionChanged(object sender, CollectionChangeEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
                if (typeof(Binding).IsAssignableFrom(e.Element.GetType()))
                {
                    Binding b = (Binding)e.Element;
                    if (b.PropertyName == DSProperty)
                        b.DataSourceUpdateMode = DataSourceUpdateMode.Never;
                }
        }

        void validated(object sender, EventArgs e)
        {
            foreach (Binding b in DataBindings)
                if (typeof(DataTable).IsAssignableFrom(b.DataSource.GetType()) && b.IsBinding)
                    if (b.BindingManagerBase.Current != null && b.PropertyName == DSProperty)
                    {
                        object dsVal = ((DataRowView)b.BindingManagerBase.Current)[b.BindingMemberInfo.BindingField];
                        object thisVal = this.GetType().GetProperty(DSProperty).GetValue(this, null);
                        if (!ToolCell.isNull(thisVal))
                            if (!ToolType.isEqual(dsVal, thisVal))
                            {
                                b.WriteValue();
                                b.ReadValue();
                            }
                        b.BindingManagerBase.EndCurrentEdit();
                    }
        }






        private void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (activity != null)
                activity.done();
        }

        public IActivity activity = null;

        private void textBox_SizeChanged(object sender, EventArgs e)
        {
            Height = textBox.Height;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (activity != null)
                activity.done();
        }



        public override string Text
        {
            get
            {
                return textBox.Text;
            }
            set
            {
                textBox.Text = value;
            }
        }

        public virtual int MaxLength
        {
            get
            {
                return textBox.MaxLength;
            }
            set
            {
                textBox.MaxLength = value;
            }
        }
        public virtual bool ReadOnly
        {
            get
            {
                return textBox.ReadOnly;
            }
            set
            {
                textBox.ReadOnly = value;
            }
        }





        string _DSProperty = "Text";
        public string DSProperty
        {
            get
            {
                return _DSProperty;
            }
            set
            {
                _DSProperty = value;
            }
        }
        string _DSTable = string.Empty;
        public string DSTable
        {
            get
            {
                return _DSTable;
            }
            set
            {
                _DSTable = value;
            }
        }
        string _DSSubTable = string.Empty;
        public string DSSubTable
        {
            get
            {
                return _DSSubTable;
            }
            set
            {
                _DSSubTable = value;
            }
        }
        string _DSColumn = string.Empty;
        public string DSColumn
        {
            get
            {
                return _DSColumn;
            }
            set
            {
                _DSColumn = value;
            }
        }
        bool _isBound = false;
        public bool isBound()
        {
            return _isBound;
        }

        public void bound(IEnvironment env)
        {
            string col = DSColumn;
            string tab = (DSSubTable != string.Empty ? DSSubTable : DSTable);
            if (col != string.Empty && tab != string.Empty)
                textBox.MaxLength = env.getColumnLen(tab, col);
        }
        private void MobTextBoxButton_Resize(object sender, EventArgs e)
        {
            this.Height = textBox.Height;
        }



        #region IControlGlobalInit Members

        public virtual void globalRead(IEnvironment pEnv, ISettings pSettings)
        {
            _isGlobalInited = true;
            InitForGlobal.read(this, getGlobalObjactName(), pEnv, pSettings);

        }

        public virtual void globalWrite(IEnvironment pEnv, ISettings pSettings)
        {
            InitForGlobal.write(this, getGlobalObjactName(), pEnv, pSettings);
        }
        public virtual string getGlobalObjactName()
        {
            return this.Name;
        }



        bool _isGlobalInited = false;
        public bool isGlobalInited()
        {
            return _isGlobalInited;
        }
        #endregion
    }
}
