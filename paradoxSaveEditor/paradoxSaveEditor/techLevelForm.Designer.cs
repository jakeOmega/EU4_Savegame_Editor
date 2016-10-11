namespace paradoxSaveEditor
{
    partial class techLevelForm
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
            this.admBox = new System.Windows.Forms.TextBox();
            this.dipBox = new System.Windows.Forms.TextBox();
            this.milBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.doneButton = new System.Windows.Forms.Button();
            this.oldAdmLabel = new System.Windows.Forms.Label();
            this.oldDipLabel = new System.Windows.Forms.Label();
            this.oldMilLabel = new System.Windows.Forms.Label();
            this.instructionText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // admBox
            // 
            this.admBox.Location = new System.Drawing.Point(212, 73);
            this.admBox.Name = "admBox";
            this.admBox.Size = new System.Drawing.Size(60, 20);
            this.admBox.TabIndex = 0;
            // 
            // dipBox
            // 
            this.dipBox.Location = new System.Drawing.Point(212, 99);
            this.dipBox.Name = "dipBox";
            this.dipBox.Size = new System.Drawing.Size(60, 20);
            this.dipBox.TabIndex = 1;
            // 
            // milBox
            // 
            this.milBox.Location = new System.Drawing.Point(212, 125);
            this.milBox.Name = "milBox";
            this.milBox.Size = new System.Drawing.Size(60, 20);
            this.milBox.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 73);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Adm Tech Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Dip Tech Level";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Mil Tech Level";
            // 
            // doneButton
            // 
            this.doneButton.Location = new System.Drawing.Point(111, 194);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(63, 30);
            this.doneButton.TabIndex = 6;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            this.doneButton.Click += new System.EventHandler(this.onDoneButton);
            // 
            // oldAdmLabel
            // 
            this.oldAdmLabel.AutoSize = true;
            this.oldAdmLabel.Location = new System.Drawing.Point(139, 73);
            this.oldAdmLabel.Name = "oldAdmLabel";
            this.oldAdmLabel.Size = new System.Drawing.Size(35, 13);
            this.oldAdmLabel.TabIndex = 7;
            this.oldAdmLabel.Text = "label4";
            // 
            // oldDipLabel
            // 
            this.oldDipLabel.AutoSize = true;
            this.oldDipLabel.Location = new System.Drawing.Point(139, 99);
            this.oldDipLabel.Name = "oldDipLabel";
            this.oldDipLabel.Size = new System.Drawing.Size(35, 13);
            this.oldDipLabel.TabIndex = 8;
            this.oldDipLabel.Text = "label5";
            // 
            // oldMilLabel
            // 
            this.oldMilLabel.AutoSize = true;
            this.oldMilLabel.Location = new System.Drawing.Point(139, 125);
            this.oldMilLabel.Name = "oldMilLabel";
            this.oldMilLabel.Size = new System.Drawing.Size(35, 13);
            this.oldMilLabel.TabIndex = 9;
            this.oldMilLabel.Text = "label6";
            // 
            // instructionText
            // 
            this.instructionText.AutoSize = true;
            this.instructionText.Location = new System.Drawing.Point(24, 13);
            this.instructionText.Name = "instructionText";
            this.instructionText.Size = new System.Drawing.Size(76, 13);
            this.instructionText.TabIndex = 10;
            this.instructionText.Text = "instructionText";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(139, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Old Tech";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(209, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "New Tech";
            // 
            // techLevelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(387, 261);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.instructionText);
            this.Controls.Add(this.oldMilLabel);
            this.Controls.Add(this.oldDipLabel);
            this.Controls.Add(this.oldAdmLabel);
            this.Controls.Add(this.doneButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.milBox);
            this.Controls.Add(this.dipBox);
            this.Controls.Add(this.admBox);
            this.Name = "techLevelForm";
            this.Text = "techLevelForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox admBox;
        private System.Windows.Forms.TextBox dipBox;
        private System.Windows.Forms.TextBox milBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.Label oldAdmLabel;
        private System.Windows.Forms.Label oldDipLabel;
        private System.Windows.Forms.Label oldMilLabel;
        private System.Windows.Forms.Label instructionText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}