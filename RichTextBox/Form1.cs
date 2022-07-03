using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace RichTextBox
{
    public partial class Form1 : Form
    {
        List<string> KeyWords = new List<string>();
        string temp = "";
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Title = "Зберегти як";
            saveFileDialog1.Filter = "rtf | *.rtf";
            saveFileDialog1.DefaultExt = ".rtf";
            saveFileDialog1.InitialDirectory = "C:\\";
            richTextBox1.Font = new Font("Arial",36);
            LoadKey();
        }
        private void LoadKey()
        {
            try
            {
                using (Stream stream = new FileStream("Key.txt", FileMode.Open))
                {
                    using (StreamReader sr=new StreamReader(stream,Encoding.UTF8))
                    {
                        KeyWords.AddRange(sr.ReadToEnd().Split(' '));
                    }
                }
            }
            catch { }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {    
            if(saveFileDialog1.ShowDialog()==DialogResult.OK)
            {
                var s = saveFileDialog1.OpenFile();
                StreamWriter sw = new StreamWriter(s);
                sw.Write(richTextBox1.Text);
                sw.Dispose();
                s.Dispose();
                
            }
            
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var s = openFileDialog1.OpenFile();
                StreamReader sw = new StreamReader(s);
                richTextBox1.Text=sw.ReadToEnd();
                sw.Dispose();
                s.Dispose();
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                if(richTextBox1.Text.ToCharArray()[richTextBox1.TextLength - 1]!=' ')
                temp += richTextBox1.Text[richTextBox1.Text.Length - 1];
                else if (KeyWords.Contains(temp)&& richTextBox1.Text[richTextBox1.Text.Length - 1] == ' ')
                {
                    richTextBox1.Select(richTextBox1.TextLength - temp.Length-1, temp.Length);
                    richTextBox1.SelectionColor = Color.Blue;
                    richTextBox1.DeselectAll();
                    richTextBox1.SelectionColor = Color.Black; 
                    richTextBox1.Select(richTextBox1.Text.Length, 0);
                    temp = "";
                }
                else if(!KeyWords.Contains(temp) && richTextBox1.Text[richTextBox1.Text.Length - 1] == ' ')
                {
                    temp = "";
                }
            }

        }
    }
}
