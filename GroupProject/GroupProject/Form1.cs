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
        public static bool DEBUGGING = true;
        StreamWriter sw;
        public Form1()
        {
            Directory.SetCurrentDirectory(Directory.GetCurrentDirectory() + "\\Material Lists\\");

            InitializeComponent();
            Name = "Material List Builder";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MainForm = this;

            if (DEBUGGING)
            {
                WriteDebugTools();
            }

        }
        private void WriteDebugTools()
        {
            
            //Creates new directory (Debug Tools) on users desktop. 
            var indent = new string(' ', 1 * 4);

            string newdir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//DebugTools";
            Directory.CreateDirectory(newdir);
            string maindir = Directory.GetCurrentDirectory();
            sw = new StreamWriter(newdir + "//ScriptInformation.txt");
            sw.WriteLine(Directory.GetCurrentDirectory());
            sw.Close();
            //Closes StreamWriter

            //Writes DeleteAllLists.py script. 
            sw = new StreamWriter(newdir + "//DeleteAllLists.py");
            sw.WriteLine("import os");
            //sw.WriteLine("from sys import argv");
            //sw.WriteLine("script, filename = argv");
            //sw.WriteLine("myfile = open(filename)");
            sw.WriteLine("newdir = " + "r" + "\"" + maindir + "\"");
            sw.WriteLine("os.chdir(newdir.rstrip())");
           // sw.WriteLine("myfile.close()");
            sw.WriteLine("dirPath = os.getcwd()");
            sw.WriteLine("fileList = os.listdir(dirPath)");
            sw.WriteLine("for fileName in fileList: os.remove(dirPath + \"/\" + fileName)");

            sw.Close();
            //Closes StreamWriter

            //Creates Example List Object
            Estimate.WriteXml("Example", "ExampleList", "123", "IN", "Westfield", "46074", new List<Tuple<string, string, string>>(),true);
           
            //Writes CreateRandom.py script.
            
            sw = new StreamWriter(newdir + "//CreateRandom.py");
            sw.WriteLine("from sys import argv");
            sw.WriteLine("from shutil import copyfile");
            sw.WriteLine("numberOfFiles = int(argv[1])");
            sw.WriteLine("newdir = " + "r" + "\"" + maindir + "\"");
            sw.WriteLine("def WriteXml(name):");
            sw.WriteLine(indent + "copyfile(\"ExampleList.xml\" , newdir + \"\\\\\"\"\" + name + \".xml\")");
            sw.WriteLine();
            sw.WriteLine("for x in xrange(0,numberOfFiles):");
            sw.WriteLine(indent + "WriteXml(\"example\" + str(x))");

            sw.Close();
            //Closes StreamWriter
        }
        private void Form1_Load(object sender, EventArgs e)
        {

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

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
