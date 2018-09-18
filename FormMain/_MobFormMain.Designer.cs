namespace MobAgent.FormMain
{
    partial class MobFormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mobMainMenu = new MobGE.MobControl.MobMainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.mmExit = new System.Windows.Forms.MenuItem();
            this.menuItem2 = new System.Windows.Forms.MenuItem();
            this.mmData = new System.Windows.Forms.MenuItem();
            this.mmDataSend = new System.Windows.Forms.MenuItem();
            this.mmDataReceive = new System.Windows.Forms.MenuItem();
            this.mmInfo = new MobGE.MobControl.MobMenuItemInfo();
            this.cTree = new MobGE.MobControl.MobTreeView();
            this.mobContextMenuMain = new MobGE.MobControl.MobContextMenu();
            this.cCMTOpen = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // mobMainMenu
            // 
            this.mobMainMenu.MenuItems.Add(this.menuItem1);
            this.mobMainMenu.MenuItems.Add(this.mmInfo);
            // 
            // menuItem1
            // 
            this.menuItem1.MenuItems.Add(this.mmExit);
            this.menuItem1.MenuItems.Add(this.menuItem2);
            this.menuItem1.MenuItems.Add(this.mmData);
            this.menuItem1.Text = "T_FILE";
            // 
            // mmExit
            // 
            this.mmExit.Text = "T_EXIT";
            this.mmExit.Click += new System.EventHandler(this.menuItem2_Click);
            // 
            // menuItem2
            // 
            this.menuItem2.Text = "-";
            // 
            // mmData
            // 
            this.mmData.MenuItems.Add(this.mmDataSend);
            this.mmData.MenuItems.Add(this.mmDataReceive);
            this.mmData.Text = "T_DATA";
            // 
            // mmDataSend
            // 
            this.mmDataSend.Text = "T_SEND";
            this.mmDataSend.Click += new System.EventHandler(this.menuSend_Click);
            // 
            // mmDataReceive
            // 
            this.mmDataReceive.Text = "T_RECEIVE";
            this.mmDataReceive.Click += new System.EventHandler(this.menuReceive_Click);
            // 
            // mmInfo
            // 
            this.mmInfo.InfoSource = "INFO_SOURCE";
            this.mmInfo.Text = "T_INFO";
            // 
            // cTree
            // 
            this.cTree.ContextMenu = this.mobContextMenuMain;
            this.cTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cTree.Location = new System.Drawing.Point(0, 0);
            this.cTree.Name = "cTree";
            this.cTree.Size = new System.Drawing.Size(240, 268);
            this.cTree.TabIndex = 0;
            this.cTree.Validating += new System.ComponentModel.CancelEventHandler(this.cTree_Validating);
            this.cTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.cTree_AfterSelect);
            this.cTree.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cTree_KeyPress);
            this.cTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cTree_KeyDown);
            // 
            // mobContextMenuMain
            // 
            this.mobContextMenuMain.MenuItems.Add(this.cCMTOpen);
            // 
            // cCMTOpen
            // 
            this.cCMTOpen.Text = "T_OPEN";
            this.cCMTOpen.Popup += new System.EventHandler(this.cCMTOpen_Popup);
            this.cCMTOpen.Click += new System.EventHandler(this.cCMTOpen_Click);
            // 
            // MobFormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.cTree);
            this.Menu = this.mobMainMenu;
            this.Name = "MobFormMain";
            this.ResumeLayout(false);

        }

        #endregion

        private MobGE.MobControl.MobMainMenu mobMainMenu;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.MenuItem mmExit;
        private System.Windows.Forms.MenuItem mmData;
        private System.Windows.Forms.MenuItem mmDataSend;
        private System.Windows.Forms.MenuItem mmDataReceive;
        private MobGE.MobControl.MobTreeView cTree;
        private MobGE.MobControl.MobContextMenu mobContextMenuMain;
        private System.Windows.Forms.MenuItem cCMTOpen;
        private System.Windows.Forms.MenuItem menuItem2;
        private MobGE.MobControl.MobMenuItemInfo mmInfo;
    }
}
