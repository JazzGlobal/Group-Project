using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace GroupProject
{
    public class CustomForm : Form
    {
        private static Form1 mainForm; 
        private static CustomForm previousForm;

        public static Form1 MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }
        
        public static CustomForm PreviousForm
        {
            get { return previousForm; }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // CustomForm
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "CustomForm";
            this.Load += new System.EventHandler(this.CustomForm_Load);
            this.ResumeLayout(false);
        }

        public static void SetPreviousForm(CustomForm pCustomForm)
        {
            previousForm = pCustomForm; 
        }
        
        public static void SetFormDimensions(CustomForm pCustomForm)
        {
            pCustomForm.StartPosition = FormStartPosition.Manual;
            pCustomForm.Left = previousForm.Left;
            pCustomForm.Top = previousForm.Top;
        }
    }
}
