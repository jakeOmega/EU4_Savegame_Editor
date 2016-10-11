namespace paradoxSaveEditor
{
    partial class provinceEditor
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
            this.editByButton = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.editInputLabel = new System.Windows.Forms.Label();
            this.editByInputBox = new System.Windows.Forms.TextBox();
            this.newOwnerBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newCultureBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.newCoreBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.newReligionBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.newTaxBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.newManpowerBox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.runButton = new System.Windows.Forms.Button();
            this.taxMultCheck = new System.Windows.Forms.CheckBox();
            this.manpowerMultCheck = new System.Windows.Forms.CheckBox();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.doneButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // editByButton
            // 
            this.editByButton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.editByButton.FormattingEnabled = true;
            this.editByButton.Items.AddRange(new object[] {
            "Name",
            "Province ID",
            "Owner",
            "Culture",
            "Religion",
            "All"});
            this.editByButton.Location = new System.Drawing.Point(58, 49);
            this.editByButton.Name = "editByButton";
            this.editByButton.Size = new System.Drawing.Size(147, 21);
            this.editByButton.TabIndex = 0;
            this.editByButton.SelectedValueChanged += new System.EventHandler(this.comboBox1_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Edit by";
            // 
            // editInputLabel
            // 
            this.editInputLabel.AutoSize = true;
            this.editInputLabel.Location = new System.Drawing.Point(205, 52);
            this.editInputLabel.Name = "editInputLabel";
            this.editInputLabel.Size = new System.Drawing.Size(0, 13);
            this.editInputLabel.TabIndex = 13;
            // 
            // editByInputBox
            // 
            this.editByInputBox.Location = new System.Drawing.Point(311, 49);
            this.editByInputBox.Name = "editByInputBox";
            this.editByInputBox.Size = new System.Drawing.Size(221, 20);
            this.editByInputBox.TabIndex = 1;
            // 
            // newOwnerBox
            // 
            this.newOwnerBox.Location = new System.Drawing.Point(140, 159);
            this.newOwnerBox.Name = "newOwnerBox";
            this.newOwnerBox.Size = new System.Drawing.Size(100, 20);
            this.newOwnerBox.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "New Owner tag";
            // 
            // newCultureBox
            // 
            this.newCultureBox.Location = new System.Drawing.Point(140, 207);
            this.newCultureBox.Name = "newCultureBox";
            this.newCultureBox.Size = new System.Drawing.Size(100, 20);
            this.newCultureBox.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 214);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 16;
            this.label4.Text = "New culture identifier";
            // 
            // newCoreBox
            // 
            this.newCoreBox.Location = new System.Drawing.Point(432, 159);
            this.newCoreBox.Name = "newCoreBox";
            this.newCoreBox.Size = new System.Drawing.Size(100, 20);
            this.newCoreBox.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(299, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "New Core tag";
            // 
            // newReligionBox
            // 
            this.newReligionBox.Location = new System.Drawing.Point(432, 211);
            this.newReligionBox.Name = "newReligionBox";
            this.newReligionBox.Size = new System.Drawing.Size(100, 20);
            this.newReligionBox.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(299, 214);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "New religion identifier";
            // 
            // newTaxBox
            // 
            this.newTaxBox.Location = new System.Drawing.Point(140, 258);
            this.newTaxBox.Name = "newTaxBox";
            this.newTaxBox.Size = new System.Drawing.Size(100, 20);
            this.newTaxBox.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(21, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "New base tax";
            // 
            // newManpowerBox
            // 
            this.newManpowerBox.Location = new System.Drawing.Point(432, 261);
            this.newManpowerBox.Name = "newManpowerBox";
            this.newManpowerBox.Size = new System.Drawing.Size(100, 20);
            this.newManpowerBox.TabIndex = 7;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(299, 264);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(107, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "New base manpower";
            // 
            // runButton
            // 
            this.runButton.Location = new System.Drawing.Point(680, 214);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(181, 23);
            this.runButton.TabIndex = 10;
            this.runButton.Text = "Run Edit";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.runButton_Click);
            // 
            // taxMultCheck
            // 
            this.taxMultCheck.AutoSize = true;
            this.taxMultCheck.Location = new System.Drawing.Point(680, 158);
            this.taxMultCheck.Name = "taxMultCheck";
            this.taxMultCheck.Size = new System.Drawing.Size(148, 17);
            this.taxMultCheck.TabIndex = 8;
            this.taxMultCheck.Text = "Multipy base tax by input?";
            this.taxMultCheck.UseVisualStyleBackColor = true;
            // 
            // manpowerMultCheck
            // 
            this.manpowerMultCheck.AutoSize = true;
            this.manpowerMultCheck.Location = new System.Drawing.Point(680, 181);
            this.manpowerMultCheck.Name = "manpowerMultCheck";
            this.manpowerMultCheck.Size = new System.Drawing.Size(185, 17);
            this.manpowerMultCheck.TabIndex = 9;
            this.manpowerMultCheck.Text = "Multiply base manpower by input?";
            this.manpowerMultCheck.UseVisualStyleBackColor = true;
            // 
            // outputBox
            // 
            this.outputBox.Location = new System.Drawing.Point(680, 44);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.Size = new System.Drawing.Size(197, 108);
            this.outputBox.TabIndex = 20;
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(680, 254);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(181, 23);
            this.doneButton.TabIndex = 11;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
            // 
            // provinceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(929, 345);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.manpowerMultCheck);
            this.Controls.Add(this.taxMultCheck);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.newManpowerBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.newTaxBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.newReligionBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.newCoreBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.newCultureBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.newOwnerBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editByInputBox);
            this.Controls.Add(this.editInputLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.editByButton);
            this.Name = "provinceEditor";
            this.Text = "provinceEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox editByButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label editInputLabel;
        private System.Windows.Forms.TextBox editByInputBox;
        private System.Windows.Forms.TextBox newOwnerBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox newCultureBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox newCoreBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox newReligionBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox newTaxBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox newManpowerBox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.CheckBox taxMultCheck;
        private System.Windows.Forms.CheckBox manpowerMultCheck;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Button doneButton;
    }
}