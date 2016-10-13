using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace paradoxSaveEditor
{
    /// <summary>
    /// Form for editing a particular monarch (or heir)
    /// </summary>
    public partial class monarchEditForm : Form
    {
        //monarch's attributes
        public string name;
        public string dip;
        public string adm;
        public string mil;
        //These two not fully supported yet
        public string dynasty;
        public string birthDate;
        /// <summary>
        /// Initialize a monarchEditForm for a specified monarch
        /// </summary>
        /// <param name="monarch">textBlock containing info about specified monarch</param>
        public monarchEditForm(textBlock monarch)
        {
            InitializeComponent();
            name = monarch.getLine(monarch.getPosition("name=")).Split('=')[1].Replace("\"", "").Trim();
            dip = monarch.getLine(monarch.getPosition("DIP=")).Split('=')[1].Trim();
            adm = monarch.getLine(monarch.getPosition("ADM=")).Split('=')[1].Trim();
            mil = monarch.getLine(monarch.getPosition("MIL=")).Split('=')[1].Trim();
            try
            {
                dynasty = monarch.getLine(monarch.getPosition("dynasty=")).Split('=')[1].Replace("\"", "").Trim();
            }
            catch
            {
                dynasty = "";
            }
            try
            {
                birthDate = monarch.getLine(monarch.getPosition("birth_date=")).Split('=')[1].Trim();
            }
            catch
            {
                birthDate = "1.1.1";
            }

            nameLabel.Text = name;
            dipLabel.Text = dip;
            admLabel.Text = adm;
            milLabel.Text = mil;
            dynastyLabel.Text = dynasty;
            birthdateLabel.Text = birthDate;
        }

        /// <summary>
        /// Make changes to monarch and close form
        /// </summary>
        private void Done_Click(object sender, EventArgs e)
        {
            if (nameBox.Text != "" )
            {
                name = nameBox.Text;
            }
            if (dipBox.Text != "")
            {
                dip = dipBox.Text;
            }
            if (admBox.Text != "")
            {
                adm = admBox.Text;
            }
            if (milBox.Text != "")
            {
                mil = milBox.Text;
            }
            if (dynastyBox.Text != "")
            {
                dynasty = dynastyBox.Text;
            }
            if (birthBox.Text != "")
            {
                birthDate = birthBox.Text;
            }
            this.Close();
        }
    }
}
