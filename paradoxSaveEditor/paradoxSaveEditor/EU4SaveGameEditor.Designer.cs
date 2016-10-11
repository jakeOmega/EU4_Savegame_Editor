namespace paradoxSaveEditor
{
    partial class EU4SaveGameEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EU4SaveGameEditor));
            this.aggExpButton = new System.Windows.Forms.Button();
            this.techLevelButton = new System.Windows.Forms.Button();
            this.monarchAbilitiesButton = new System.Windows.Forms.Button();
            this.countryResourcesButton = new System.Windows.Forms.Button();
            this.provinceButton = new System.Windows.Forms.Button();
            this.writeButton = new System.Windows.Forms.Button();
            this.inputTextLeft = new System.Windows.Forms.TextBox();
            this.inputTextRight = new System.Windows.Forms.TextBox();
            this.instructionText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // aggExpButton
            // 
            this.aggExpButton.Location = new System.Drawing.Point(305, 110);
            this.aggExpButton.Name = "aggExpButton";
            this.aggExpButton.Size = new System.Drawing.Size(214, 23);
            this.aggExpButton.TabIndex = 0;
            this.aggExpButton.Text = "Aggressive Expansion";
            this.aggExpButton.UseVisualStyleBackColor = true;
            this.aggExpButton.Visible = false;
            this.aggExpButton.Click += new System.EventHandler(this.aggExpButton_Click);
            // 
            // techLevelButton
            // 
            this.techLevelButton.Location = new System.Drawing.Point(305, 139);
            this.techLevelButton.Name = "techLevelButton";
            this.techLevelButton.Size = new System.Drawing.Size(214, 23);
            this.techLevelButton.TabIndex = 2;
            this.techLevelButton.Text = "Tech Level";
            this.techLevelButton.UseVisualStyleBackColor = true;
            this.techLevelButton.Visible = false;
            this.techLevelButton.Click += new System.EventHandler(this.techLevel_click);
            // 
            // monarchAbilitiesButton
            // 
            this.monarchAbilitiesButton.Location = new System.Drawing.Point(305, 168);
            this.monarchAbilitiesButton.Name = "monarchAbilitiesButton";
            this.monarchAbilitiesButton.Size = new System.Drawing.Size(214, 23);
            this.monarchAbilitiesButton.TabIndex = 3;
            this.monarchAbilitiesButton.Text = "Monarch Abilities";
            this.monarchAbilitiesButton.UseVisualStyleBackColor = true;
            this.monarchAbilitiesButton.Visible = false;
            this.monarchAbilitiesButton.Click += new System.EventHandler(this.monarchAbilitiesButton_Click);
            // 
            // countryResourcesButton
            // 
            this.countryResourcesButton.Location = new System.Drawing.Point(305, 197);
            this.countryResourcesButton.Name = "countryResourcesButton";
            this.countryResourcesButton.Size = new System.Drawing.Size(214, 23);
            this.countryResourcesButton.TabIndex = 4;
            this.countryResourcesButton.Text = "Country Resources";
            this.countryResourcesButton.UseVisualStyleBackColor = true;
            this.countryResourcesButton.Visible = false;
            this.countryResourcesButton.Click += new System.EventHandler(this.countryResourcesButton_Click);
            // 
            // provinceButton
            // 
            this.provinceButton.Location = new System.Drawing.Point(305, 226);
            this.provinceButton.Name = "provinceButton";
            this.provinceButton.Size = new System.Drawing.Size(214, 23);
            this.provinceButton.TabIndex = 5;
            this.provinceButton.Text = "Provinces";
            this.provinceButton.UseVisualStyleBackColor = true;
            this.provinceButton.Visible = false;
            this.provinceButton.Click += new System.EventHandler(this.provinceButton_Click);
            // 
            // writeButton
            // 
            this.writeButton.Location = new System.Drawing.Point(305, 255);
            this.writeButton.Name = "writeButton";
            this.writeButton.Size = new System.Drawing.Size(214, 23);
            this.writeButton.TabIndex = 6;
            this.writeButton.Text = "Write to Disk";
            this.writeButton.UseVisualStyleBackColor = true;
            this.writeButton.Visible = false;
            this.writeButton.Click += new System.EventHandler(this.writeButton_click);
            // 
            // inputTextLeft
            // 
            this.inputTextLeft.BackColor = System.Drawing.SystemColors.Menu;
            this.inputTextLeft.Enabled = false;
            this.inputTextLeft.Location = new System.Drawing.Point(12, 110);
            this.inputTextLeft.Name = "inputTextLeft";
            this.inputTextLeft.ReadOnly = true;
            this.inputTextLeft.Size = new System.Drawing.Size(37, 20);
            this.inputTextLeft.TabIndex = 7;
            this.inputTextLeft.Text = "Input:";
            // 
            // inputTextRight
            // 
            this.inputTextRight.Enabled = false;
            this.inputTextRight.Location = new System.Drawing.Point(56, 110);
            this.inputTextRight.Name = "inputTextRight";
            this.inputTextRight.Size = new System.Drawing.Size(158, 20);
            this.inputTextRight.TabIndex = 8;
            this.inputTextRight.TextChanged += new System.EventHandler(this.inputTextRight_TextChanged);
            this.inputTextRight.KeyUp += new System.Windows.Forms.KeyEventHandler(this.inputEntered);
            // 
            // instructionText
            // 
            this.instructionText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.instructionText.BackColor = System.Drawing.SystemColors.Menu;
            this.instructionText.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.instructionText.Location = new System.Drawing.Point(12, 12);
            this.instructionText.Multiline = true;
            this.instructionText.Name = "instructionText";
            this.instructionText.ReadOnly = true;
            this.instructionText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.instructionText.Size = new System.Drawing.Size(507, 72);
            this.instructionText.TabIndex = 1;
            // 
            // EU4SaveGameEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 349);
            this.Controls.Add(this.inputTextRight);
            this.Controls.Add(this.inputTextLeft);
            this.Controls.Add(this.writeButton);
            this.Controls.Add(this.provinceButton);
            this.Controls.Add(this.countryResourcesButton);
            this.Controls.Add(this.monarchAbilitiesButton);
            this.Controls.Add(this.techLevelButton);
            this.Controls.Add(this.instructionText);
            this.Controls.Add(this.aggExpButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EU4SaveGameEditor";
            this.Text = "EU4 Save Game Editor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button aggExpButton;
        private System.Windows.Forms.Button techLevelButton;
        private System.Windows.Forms.Button monarchAbilitiesButton;
        private System.Windows.Forms.Button countryResourcesButton;
        private System.Windows.Forms.Button provinceButton;
        private System.Windows.Forms.Button writeButton;
        private System.Windows.Forms.TextBox inputTextLeft;
        private System.Windows.Forms.TextBox inputTextRight;
        private System.Windows.Forms.TextBox instructionText;
    }
}