namespace MobGE.MobControl
{
    partial class MobTextBoxButton
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox = new MobGE.MobControl.MobTextBox();
            this.button = new MobGE.MobControl.MobButton();
            this.SuspendLayout();
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.DSColumn = "";
            this.textBox.DSProperty = "Text";
            this.textBox.DSSubTable = "";
            this.textBox.DSTable = "";
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(133, 23);
            this.textBox.TabIndex = 1;
            // 
            // button
            // 
            this.button.Dock = System.Windows.Forms.DockStyle.Right;
            this.button.Location = new System.Drawing.Point(133, 0);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(25, 40);
            this.button.TabIndex = 0;
            this.button.Text = "...";
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // MobTextBoxButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.button);
            this.Name = "MobTextBoxButton";
            this.Size = new System.Drawing.Size(158, 40);
            this.Resize += new System.EventHandler(this.MobTextBoxButton_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private MobButton button;
        private MobTextBox textBox;

    }
}
