using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

/*
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
*/
namespace Khan_REC_Merger_1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            
        }
      
        private void OriginalFilesFolder(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                string folder = Directory.GetCurrentDirectory().ToString();

                string directoryPath = Path.GetDirectoryName(fbd.SelectedPath);

                this.textBox1.Text = fbd.SelectedPath;
            }

        }        
        
        private void DestinationFIlesFolder(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                string folder = Directory.GetCurrentDirectory().ToString();

                string directoryPath = Path.GetDirectoryName(fbd.SelectedPath);

                this.textBox3.Text = fbd.SelectedPath;

            }
        }
        private void XDataFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "";
            openFileDialog1.Filter = "Khan files (*.REC, *.json)|*.REC;*.json";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string selectedFileName = openFileDialog1.FileName;
                this.textBox2.Text = selectedFileName;
            }
        }

        //****************************************************
        // Mergar REC. json Obj eller array i en flera REC. Json filer
        // IN: Json Obj
        // UT: Nya Updaterade Json filer
        //****************************************************
        void MergeFiles()
        {
            int i = 1;
           
            //Att göra; Use rich file Box. paste Json Object
            var updateFilePath = String.Format($"{this.textBox2.Text}", AppDomain.CurrentDomain.BaseDirectory);
            string jsonUpdateFile = new StreamReader(updateFilePath).ReadToEnd();
            var jsonU = JObject.Parse(jsonUpdateFile);

            //save to file



            //Att göra; Use adressbox to anable the user to choose path.  // jobbDAtorn : C:\Users\basmoh\source\repos\JsonMerger\JsonMerger\bin\Debug\netcoreapp3.1
            //hemma  Parallel.ForEach(Directory.EnumerateFiles(@"C:\Users\abuam\source\repos\ConsoleIronXL\ConsoleIronXL\bin\Debug\net5.0\json", "*.json"), (file) =>
            Parallel.ForEach(Directory.EnumerateFiles($"{this.textBox1.Text}", "*.REC"), (file) =>
            {

              

                string filename = file.Substring(file.LastIndexOf(@"\") -1);
               

                // Välj Mapp
                var OldFilePath = String.Format(file, AppDomain.CurrentDomain.BaseDirectory);
                string jsonOldFile = new StreamReader(OldFilePath).ReadToEnd();
                var jsonO = JObject.Parse(jsonOldFile);


                //merge new json into old json
                jsonO.Merge(jsonU);

                var NewFile = String.Format($"{filename}" , AppDomain.CurrentDomain.BaseDirectory);

                i++;

                FileInfo fileToSave = new FileInfo(NewFile);
                fileToSave.Directory.Create();
                string JsonToSave = JsonConvert.SerializeObject(jsonO);
                System.IO.File.WriteAllText(@NewFile, JsonToSave);
                NewFile = "";
            });
        }


        private void MERGE_Click(object sender, EventArgs e)
        {
            int i = 1;

            //Att göra; Use rich file Box. paste Json Object
            var updateFilePath = String.Format(this.textBox2.Text, AppDomain.CurrentDomain.BaseDirectory);
            string jsonUpdateFile = new StreamReader(updateFilePath).ReadToEnd();
            var jsonU = JObject.Parse(jsonUpdateFile);

            //save to file



            //Att göra; Use adress box to anable the user to choose path.  // jobbDAtorn : C:\Users\basmoh\source\repos\JsonMerger\JsonMerger\bin\Debug\netcoreapp3.1
            //hemma  Parallel.ForEach(Directory.EnumerateFiles(@"C:\Users\abuam\source\repos\ConsoleIronXL\ConsoleIronXL\bin\Debug\net5.0\json", "*.json"), (file) =>
            Parallel.ForEach(Directory.EnumerateFiles(this.textBox1.Text, "*.REC"), (file) =>
            {



                string filename = file.Substring(file.LastIndexOf("\\") - 0);


                var OldFilePath = String.Format(file, AppDomain.CurrentDomain.BaseDirectory);
                string jsonOldFile = new StreamReader(OldFilePath).ReadToEnd();
                var jsonO = JObject.Parse(jsonOldFile);


                //merge new json into old json
                jsonO.Merge(jsonU);

                var NewFile = String.Format(this.textBox3.Text + filename, AppDomain.CurrentDomain.RelativeSearchPath);

                i++;

                FileInfo fileToSave = new FileInfo(NewFile);
                fileToSave.Directory.Create();
                string JsonToSave = JsonConvert.SerializeObject(jsonO);
                System.IO.File.WriteAllText(@NewFile, JsonToSave);
                NewFile = "";
            });

            MessageBox.Show(" KLART! ");
        }
        private void button4_Click(object sender, EventArgs e)
        {
            MergeFiles();
        }
    }
}
