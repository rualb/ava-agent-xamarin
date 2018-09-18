using System;
using System.Collections.Generic;
using System.Text;
using MobExt.AndroidEnv.ControlsBase;
using MobExt.Common;
using MobExt.ControlOperation;
using MobExt.Common.Const;
using System.ComponentModel;
using System.Data;
using MobExt.ObjectSource;
using MobExt.TableOperation;
using MobExt.Settings;

namespace MobGE.MobControl
{
    public class MobNumEditListButton : UserControl, IControlBind, IObjectSource, IControlGlobalInit
    {
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
        public MobNumEditListButton()
        {
            InitializeComponent();


            this.DataBindings.CollectionChanged += new CollectionChangeEventHandler(DataBindings_CollectionChanged);
            this.numBox.Validated += new EventHandler(validated);
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

        public MobComboBox getComoBox()
        {
            return numBox;
        }
        public MobButton getButton()
        {
            return button;
        }



        private void textBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (activity != null)
                activity.done();
        }

        MobNumEditList numBox;
        MobButton button;

        public IActivity activity = null;

        private void textBox_SizeChanged(object sender, EventArgs e)
        {
            Height = numBox.Height;
        }

        private void button_Click(object sender, EventArgs e)
        {
            if (activity != null)
                activity.done();
        }


        public virtual double Value
        {
            get
            {
                return numBox.Value;
            }
            set
            {
                numBox.Value = value;
            }
        }

        public virtual double Increment
        {
            get { return numBox.Increment; }
            set { numBox.Increment = value; }
        }
        public virtual double Maximum
        {
            get { return numBox.Maximum; }
            set { numBox.Maximum = value; }
        }
        public virtual double Minimum
        {
            get { return numBox.Minimum; }
            set { numBox.Minimum = value; }
        }

        public virtual bool ReadOnly
        {
            get { return numBox.ReadOnly; }
            set { numBox.ReadOnly = value; }
        }





        string _DSProperty = "Value";
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

        }
        private void MobTextBoxButton_Resize(object sender, EventArgs e)
        {
            this.Height = numBox.Height;
        }
        public object get()
        {
            return Value;
        }
        private void InitializeComponent()
        {
            this.numBox = new MobGE.MobControl.MobNumEditList();
            this.button = new MobGE.MobControl.MobButton();
            this.SuspendLayout();
            // 
            // numBox
            // 
            this.numBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.numBox.DSColumn = "";
            this.numBox.DSProperty = "Value";
            this.numBox.DSSubTable = "";
            this.numBox.DSTable = "";
            this.numBox.Increment = 0;
            this.numBox.Location = new System.Drawing.Point(0, 0);
            this.numBox.Maximum = 0;
            this.numBox.Minimum = 0;
            this.numBox.Name = "numBox";
            this.numBox.ReadOnly = false;
            this.numBox.Size = new System.Drawing.Size(133, 22);
            this.numBox.TabIndex = 3;
            this.numBox.Value = 0;
            // 
            // button
            // 
            this.button.Dock = System.Windows.Forms.DockStyle.Right;
            this.button.Location = new System.Drawing.Point(133, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(25, 49);
            this.button.TabIndex = 2;
            this.button.Text = "...";
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // MobNumEditListButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.numBox);
            this.Controls.Add(this.button);
            this.Name = "MobNumEditListButton";
            this.Size = new System.Drawing.Size(158, 49);
            this.Resize += new System.EventHandler(this.MobTextBoxButton_Resize);
            this.ResumeLayout(false);

        }
    }
}
