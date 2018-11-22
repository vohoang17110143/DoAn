using System;
using System.Collections.Generic;
using System.Collections;
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
       
      
        public Form1()
        {
            InitializeComponent();
        }


        bool NhapLai;
        void ThemKiTu(string c)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            if (NhapLai)
            {
                textBox1.Text = "";
                Invalidate();
            }
            textBox1.Text += c;
            NhapLai = false;
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
            TinhToan();
        }

        public static int GetPriority(string op)
        {
            if (op == "sin" || op == "cos" || op == "tan" || op == "cotg" || op == "ln" || op == "log" || op == "sqrt")
                return 3;
            if (op == "*" || op == "/" || op == "%" )
                return 2;
            if (op == "+" || op == "-")
                return 1;
            return 0;
        }
        void TinhToan()
        {
            textBox1.Text += ' ';
            Stack sh = new Stack();
            Stack st = new Stack();
            int i = 0, j = 0;
            double ketQua = 0, a, b;
            string str1;

            while (i < textBox1.Text.Length - 1)
            {
                //nạp sin,cos,tan,cotg,ln,log vào toán tử st
                if(textBox1.Text[i]>='a' && textBox1.Text[i]<='z')
                {
                    j = i;
                    while (textBox1.Text[i] >= 'a' && textBox1.Text[i] <= 'z')
                        i++;
                    string str = textBox1.Text.Substring(j, i - j);
                    st.Push((str.ToString()));
                }

                //nạp toán hạng vào sh
                if (textBox1.Text[i] >= '0' && textBox1.Text[i] <= '9')
                {
                    j = i;
         
                    while (textBox1.Text[i + 1] >= '0' && textBox1.Text[i + 1] <= '9' 
                        || textBox1.Text[i + 1] == '.' && textBox1.Text[i + 1] != ' ' && i + 1 < textBox1.Text.Length)
                        i++;
                    string str = textBox1.Text.Substring(j, i + 1 - j);
                    sh.Push((str.ToString()));
                }

                //nạp dấu ( vào st
                if (textBox1.Text[i] == '(')
                {
                    st.Push(textBox1.Text[i]);
                    if (textBox1.Text[i + 1] == '-' || textBox1.Text[i + 1] == '+')
                        sh.Push(0);
                }

                //nạp toán tử vào st
                if (textBox1.Text[i] == '+' || textBox1.Text[i] == '-' || textBox1.Text[i] == '*' || textBox1.Text[i] == '/' || textBox1.Text[i] == '%')
                {
                    while (st.Count > 0 && GetPriority(textBox1.Text[i].ToString()) <= GetPriority(st.Peek().ToString()))
                    {
                        str1 = st.Pop().ToString();

                        //nếu là các phép tính
                        if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%")
                        {
                            a = double.Parse((sh.Pop().ToString()));
                            b = double.Parse((sh.Pop().ToString()));
                            switch (str1)
                            {
                                case "+":
                                    ketQua = (a + b);
                                    break;
                                case "-":
                                    ketQua = (b - a);
                                    break;
                                case "*":
                                    ketQua = (a * b);
                                    break;
                                case "/":
                                    if (a == 0)
                                    {
                                        MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                    }
                                    ketQua = (b / a);
                                    break;
                                case "%":
                                    if (a == 0)
                                    {
                                        MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                    }
                                    ketQua = (b % a);
                                    break;
                            }                            
                            sh.Push(ketQua);
                        }

                        //Nếu là sin,cos,tan,cotg,ln,log

                        else
                            if( str1=="sin" || str1=="cos" || str1=="tan" || str1=="cotg" || str1=="ln" ||str1=="log" ||str1=="sqrt")
                        {
                            switch(str1)
                            {
                                case "sin":
                                    sh.Push(Math.Sin(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "scos":
                                    sh.Push(Math.Cos(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "tan":
                                    sh.Push(Math.Tan(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "cotg":
                                    sh.Push(1/Math.Tan(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "ln":
                                    sh.Push(Math.Log10(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "log":
                                    sh.Push(Math.Log(double.Parse(sh.Pop().ToString())));
                                    break;
                                case "sqrt":
                                    sh.Push(Math.Sqrt(double.Parse(sh.Pop().ToString())));
                                    break;
                            }
                        }
                    }
                

                    //xử lí nhiều dấu + hoặc - hoặc + - hoặc - + liên tiếp(số âm)

                    if (textBox1.Text[i] == '-' && textBox1.Text[i + 1] == '-' || textBox1.Text[i] == '-' && textBox1.Text[i + 1] == '+' || textBox1.Text[i] == '+' && textBox1.Text[i + 1] == '+' || textBox1.Text[i] == '+' && textBox1.Text[i + 1] == '-')
                    {
                        if (textBox1.Text[i] == '-')
                            j = 1;
                        while (textBox1.Text[i + 1] == '-' || textBox1.Text[i + 1] == '+')
                        {
                            if (textBox1.Text[i + 1] == '-')
                                j++;
                            i++;
                        }
                        if ((j) % 2 == 0)
                            str1 = "+";
                        else
                            str1 = "-";
                        st.Push((str1.ToString()));
                    }
                    else
                        st.Push(textBox1.Text[i]);
                }



                //xử lí các phép toán trong dấu ngoặc
                if (textBox1.Text[i] == ')')
                {
                    str1 = "+"; // khai báo str1 ảo(tránh stack rỗng)
                    while (str1 != "(")
                    {

                        a = double.Parse((sh.Pop().ToString()));
                        str1 = st.Pop().ToString();
                        if (str1 == "(")
                        {
                            sh.Push(a);
                            break;
                        }
                        if (sh.Count == 0 && (str1 == "+" || str1 == "-")) b = 0;
                        else
                            if (sh.Count == 0 && (str1 == "*" || str1 == "/"))
                        {
                            MessageBox.Show("Loi!");
                            break;
                        }
                        else
                            b = double.Parse((sh.Pop().ToString()));
                        switch (str1)
                        {
                            case "+":
                                ketQua = (a + b);
                                break;
                            case "-":
                                ketQua = (b - a);
                                break;
                            case "*":
                                ketQua = (a * b);
                                break;
                            case "/":
                                if (a == 0)
                                {
                                    MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                }
                                ketQua = (b / a);
                                break;
                            case "%":
                                if (a == 0)
                                {
                                    MessageBox.Show("Không thể chia cho 0", "Thông báo");
                                }
                                ketQua = (b % a);
                                break;
                        }                        
                        sh.Push(ketQua);
                        if (str1 != "(")
                            str1 = st.Pop().ToString();
                    }
                    if (textBox1.Text[i] == ')' && (textBox1.Text[i] >= '0' && textBox1.Text[i] <= '9'))
                        st.Push("*");

                }

                i++;
            }

            //xử lí các phép toán còn lại
            while (st.Count > 0)
            {
                a = double.Parse(sh.Pop().ToString()); ;
                str1 = st.Pop().ToString();
                if (sh.Count == 0 && (str1 == "+" || str1 == "-")) b = 0;
                else
                    if (sh.Count == 0 && (str1 == "*" || str1 == "/"))
                {
                    MessageBox.Show("Error");
                    textBox1.Text = "0";
                    return;
                }

                if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%" )
                {
                    b = double.Parse((sh.Pop().ToString()));
                    switch (str1)
                    {
                        case "+":
                            ketQua = (a + b);
                            break;
                        case "-":
                            ketQua = (b - a);
                            break;
                        case "*":
                            ketQua = (a * b);
                            break;
                        case "/":
                            if (a == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0", "Thông báo");
                            }
                            ketQua = (b / a);
                            break;
                        case "%":
                            if (a == 0)
                            {
                                MessageBox.Show("Không thể chia cho 0", "Thông báo");
                            }
                            ketQua = (b % a);
                            break;
                    }
                    sh.Push(ketQua);
                }
                else
                     if (str1 == "sin" || str1 == "cos" || str1 == "tan" || str1 == "cotg" || str1 == "ln" || str1 == "log" || str1 == "sqrt")
                {

                    switch (str1)
                    {
                        case "sin":
                            sh.Push(Math.Sin(a));
                            break;
                        case "cos":
                            sh.Push(Math.Cos(a));
                            break;
                        case "tan":
                            sh.Push(Math.Tan(a));
                            break;
                        case "cotg":
                            sh.Push(1.00 / Math.Tan(a));
                            break;
                        case "ln":
                            sh.Push(Math.Log(a));
                            break;
                        case "log":
                            sh.Push(Math.Log10(a));
                            break;
                        case "sqrt":
                            sh.Push(Math.Sqrt(a));
                            break;
                    }
                }
            }
            textBox1.Text = sh.Pop().ToString();

        }

       
        bool IsShiftKeyPress = false;

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            IsShiftKeyPress = false;
        }


        private void button0_Click(object sender, EventArgs e)
        {
            ThemKiTu("0");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            ThemKiTu("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
           
            ThemKiTu("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
            ThemKiTu("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
            ThemKiTu("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("9");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text += '-';
            NhapLai = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text += '+';
            NhapLai = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            textBox1.Text += '*';
            NhapLai = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            textBox1.Text += '/';
            NhapLai = false;
        }

        private void button17_Click(object sender, EventArgs e)   //Lùi phần tử số đã nhập
     
        {
            int lenght = textBox1.TextLength - 1;
            string text = textBox1.Text;
            textBox1.Clear();
            for (int i = 0; i < lenght; i++)
                textBox1.Text = textBox1.Text + text[i];
        }

        private void button18_Click(object sender, EventArgs e)
        {
            
            ThemKiTu("%");
        }
        private void button_cl_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
        }

        private void button_MoNg_Click(object sender, EventArgs e)
        {
            ThemKiTu("(");
        }

        private void button_DongNG_Click(object sender, EventArgs e)
        {
            ThemKiTu(")");
        }

        private void buttom_Cham_Click(object sender, EventArgs e)
        {
            ThemKiTu(".");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {
            ThemKiTu("sin");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            ThemKiTu("cos");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            ThemKiTu("tan");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            ThemKiTu("cotg");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            ThemKiTu("sqrt");
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {
            ThemKiTu("log");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            ThemKiTu("ln");
        }
    }
}
