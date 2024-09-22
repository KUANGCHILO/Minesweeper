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

namespace WindowsFormsApp1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\data.csv");
            filePath = Path.GetFullPath(filePath); // 確保獲取絕對路徑

            if (!File.Exists(filePath))
            {
                using (StreamWriter writer = new StreamWriter(filePath, false, Encoding.UTF8))
                {
                    writer.WriteLine("姓名,成績,時間");
                }
            }
            List<string> lines = new List<string>(File.ReadAllLines(filePath));
            for (int i = 1; i < lines.Count; i++) {
                string[] columns = lines[i].Split(',');
                label1.Text = $"{label1.Text}\n{columns[0]}";
                label2.Text = $"{label2.Text}\n{columns[1]}";
                label3.Text = $"{label3.Text}\n{columns[2]}";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }
    }
}
