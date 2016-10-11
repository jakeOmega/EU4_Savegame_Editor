using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Windows.Forms;

namespace paradoxSaveEditor
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            string fileName;
            Application.EnableVisualStyles();
            var isoEncoding = Encoding.GetEncoding("iso-8859-1");
            string currentDirectory = System.Environment.CurrentDirectory;
            if(args.Length > 0)
            {
                fileName = args[0];
                EU4SaveGameEditor editor = new EU4SaveGameEditor(fileName);
                editor.ShowDialog();
            }
            else
            {
                bool hitOK = false;
                OpenFileDialog fileDialog = new OpenFileDialog();
                fileDialog.InitialDirectory = currentDirectory;
                hitOK = fileDialog.ShowDialog() == DialogResult.OK;
                if ( hitOK )
                {
                    fileName = fileDialog.FileName;
                    EU4SaveGameEditor editor = new EU4SaveGameEditor(fileName);
                    editor.ShowDialog();
                }
            }
            
        }
    }
}
