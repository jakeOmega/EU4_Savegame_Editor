namespace paradoxSaveEditor
{
    partial class monarchAbilitiesForm
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
            this.monarchBox = new System.Windows.Forms.ListBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.heirBox = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // monarchBox
            // 
            this.monarchBox.FormattingEnabled = true;
            this.monarchBox.Location = new System.Drawing.Point(12, 29);
            this.monarchBox.Name = "monarchBox";
            this.monarchBox.Size = new System.Drawing.Size(120, 160);
            this.monarchBox.TabIndex = 0;
            this.monarchBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.monarchBox_MouseDoubleClick_1);
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(103, 211);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(75, 23);
            this.doneButton.TabIndex = 1;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.onDoneButton);
            // 
            // heirBox
            // 
            this.heirBox.FormattingEnabled = true;
            this.heirBox.Location = new System.Drawing.Point(152, 29);
            this.heirBox.Name = "heirBox";
            this.heirBox.Size = new System.Drawing.Size(120, 160);
            this.heirBox.TabIndex = 2;
            this.heirBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.heirBox_MouseDoubleClick);
            // 
            // monarchAbilitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.heirBox);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.monarchBox);
            this.Name = "monarchAbilitiesForm";
            this.Text = "techLevelForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox monarchBox;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.ListBox heirBox;

    }
}