using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace GroupProject
{
    public partial class Form1 : CustomForm
    {
        InformationInput informationInputForm;
        LoadingForm loadingForm; 

        public Form1()
        {
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Material Lists\\");
            InitializeComponent();
            Name = "Material List Builder";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MainForm = this;
        }
        /// <summary>
        /// Sends user to InformationInput.cs . 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            informationInputForm = new InformationInput(this);
            informationInputForm.Show(); 
        }
        /// <summary>
        /// Sends user to LoadingForm.cs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {        
            Hide();
            loadingForm = new LoadingForm(this);
            loadingForm.Show();            
        }
    }
}
