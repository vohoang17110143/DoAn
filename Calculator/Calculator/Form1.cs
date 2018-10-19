using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Form1 : Form
    {
        double result;
        string sign;
        double val1;
        double val2;
        int trackkeypoint = 0;
      
        public Form1()
        {
            InitializeComponent();
        }





        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
             result = 0;
            label1.Text = "";
        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            val2 = double.Parse(textBox1.Text);
            if (sign == "+")
            {
                result = val1 + val2;
                textBox1.Text = result.ToString();
            }
            else if (sign == "-")
            {
                result = val1 - val2;
                textBox1.Text = result.ToString();
            }
            else if (sign == "*")
            {
                result = val1 * val2;
                textBox1.Text = result.ToString();
            }
            else
            {
                result = val1 / val2;
                textBox1.Text = result.ToString();
            }
        }

        private void button0_Click(object sender, EventArgs e)
        {
            textBox1.Text = textBox1.Text + button0.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button3.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button4.Text;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button5.Text;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button6.Text;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button7.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button8.Text;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button9.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sign = "-";
            val1 = double.Parse(textBox1.Text);
            label1.Text = val1.ToString() + "-";
            textBox1.Text = "";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sign = "+";
            val1 = double.Parse(textBox1.Text);
            label1.Text = val1.ToString() + "+";
            textBox1.Text = "";
        }

        private void button12_Click(object sender, EventArgs e)
        {
            sign = "*";
            val1 = double.Parse(textBox1.Text);
            label1.Text = val1.ToString() + "*";
            textBox1.Text = "";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            sign = "/";
            val1 = double.Parse(textBox1.Text);
            label1.Text = val1.ToString() + "/";
            textBox1.Text = "";
        }

        private void button17_Click(object sender, EventArgs e)   //Lùi phần tử số đã nhập
     
        {
            int lenght = textBox1.TextLength - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
                textBox1.Text = textBox1.Text + text[i];
        }

        private void button_cl_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }
    }
}
