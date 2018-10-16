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
        string sign;
        double val1;
        double val2;
        int trackkeypoint = 0;
      
        public Form1()
        {
            InitializeComponent();
        }


        bool NhapLai;
        void ThemKiTu(string c)
        {
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

                //nạp toán hạng vào sh
                if (textBox1.Text[i] >= '0' && textBox1.Text[i] <= '9')
                {
                    j = i;
                    //if (manhinh.Text[i + 1] == '(')
                    //    st.Push("*"); 
                    while (textBox1.Text[i + 1] >= '0' && textBox1.Text[i + 1] <= '9' || textBox1.Text[i + 1] == '.' && textBox1.Text[i + 1] != ' ' && i + 1 < textBox1.Text.Length)
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
                if (textBox1.Text[i] == '+' || textBox1.Text[i] == '-' || textBox1.Text[i] == '*' || textBox1.Text[i] == '/' || textBox1.Text[i] == '%' || textBox1.Text[i] == '^')
                {
                    while (st.Count > 0 && GetPriority(textBox1.Text[i].ToString()) <= GetPriority(st.Peek().ToString()))
                    {
                        str1 = st.Pop().ToString();

                        //nếu là các phép tính
                        if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%" || str1 == "^")
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
                                case "^":
                                    ketQua = Math.Pow(b, a);
                                    break;
                            }
                            sh.Push(ketQua);
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
                            case "^":
                                ketQua = Math.Pow(b, a);
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

                if (str1 == "+" || str1 == "-" || str1 == "*" || str1 == "/" || str1 == "%" || str1 == "^")
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
                        case "^":
                            ketQua = Math.Pow(b, a);
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
                IsShiftKeyPress = true;
            if (e.KeyCode == Keys.D0 && !IsShiftKeyPress || e.KeyCode == Keys.NumPad0)
                button0_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1)
                button1_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
                button2_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
                button3_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
                button4_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
                button5_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
                button6_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
                button7_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D8 && !IsShiftKeyPress || e.KeyCode == Keys.NumPad8)
                button8_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.D9 && !IsShiftKeyPress || e.KeyCode == Keys.NumPad9)
                button9_Click(textBox1, new EventArgs());

            if (e.KeyCode == Keys.Back)
                button17_Click(textBox1, new EventArgs());

            if (e.KeyCode == Keys.Add || (IsShiftKeyPress && e.KeyCode == Keys.Oemplus))
                button10_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
                button11_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.Multiply || IsShiftKeyPress && e.KeyCode == Keys.D8)
                button12_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.OemQuestion)
                button13_Click(textBox1, new EventArgs());
            if (e.KeyCode == Keys.OemPeriod)
                buttom_Cham_Click(textBox1, new EventArgs());

            if (IsShiftKeyPress && e.KeyCode == Keys.D9)
                button_MoNg_Click(textBox1, new EventArgs());
            if (IsShiftKeyPress && e.KeyCode == Keys.D0)
                button_DongNG_Click(textBox1, new EventArgs());

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
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("1");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("2");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("3");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("4");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("5");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("6");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            ThemKiTu("1");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            textBox1.Text += '-';
            NhapLai = false;
        }

        private void button10_Click(object sender, EventArgs e)
        {
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
            textBox1.Text += '%';
            NhapLai = false;
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
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button_MoNg.Text;
        }

        private void button_DongNG_Click(object sender, EventArgs e)
        {
            if ((textBox1.Text == "0"))
                textBox1.Clear();
            textBox1.Text = textBox1.Text + button_DongNG.Text;
        }

        private void buttom_Cham_Click(object sender, EventArgs e)
        {
            ThemKiTu(".");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      
    }
}
