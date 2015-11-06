using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace keygen
{

    public partial class Form1 : Form
    {
        readonly Generator Gen = new Generator();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox2.Text = Gen.generateKey(textBox1.Text);
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;
            var lines = new string[2];
            var file = new System.IO.StreamReader(openFileDialog1.FileName);
            for (var i = 0; i < lines.Length; i++)
            {
                string line;
                if ((line = file.ReadLine()) != null)
                {
                    lines[i] = line;
                }
            }
            Gen.arrayGenerator(lines);

            var outString = "";
            for (var i = 0; i < Gen.keyArray.GetLength(0); i++)
            {
                for (var j = 0; j < Gen.keyArray.GetLength(1); j++)
                {
                    outString = outString + Gen.keyArray[i, j] + " ";
                }
                outString = outString + "\n";
            }
            label1.Text = outString;
            var arrow = new string[8] { "↑", "↗", "→", "↘", "↓", "↙", "←", "↖" };
            var temp = Gen.algoritmArray.Aggregate("", (current, item) => current + arrow[item] + " ");
            label2.Text = temp;
        }
    }
}
