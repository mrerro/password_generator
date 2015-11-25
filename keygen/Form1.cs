using System;
using System.Linq;
using System.Windows.Forms;

namespace keygen
{

    public partial class Form1 : Form
    {
        readonly Generator Gen = new Generator();
        private bool codeword = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Gen.keyArray == null && Gen.algoritmArray == null)
            {
                textBox2.Text = "Загрузите файл ключа";
            }
            else if (!codeword)
            {
                textBox2.Text = "Кодовое слово не вводилось";
            }
            else if (Gen.keyArray.Length == 0 && Gen.algoritmArray.Length == 0)
            {
                textBox2.Text = "Файл ключа пуст";
            }
            else
            {
                Gen.stopKey = 0;
                Gen.stepCount = 0;
                var outLine = Gen.generateKey(textBox1.Text);
                var numbers = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                for (var i = 0; i < outLine.Length; i++)
                {
                    if (!numbers.Where(g => g == outLine[i]).Any())
                    {
                        textBox2.Text = outLine.Substring(0, 0 + i) + char.ToUpper(outLine[i]) +
                                        outLine.Substring(i + 1);
                        break;
                    }
                }
            }


        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.ResetText();
            codeword = true;
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
            if (lines[0] == null || lines[1] == null) return;
            {
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
}
