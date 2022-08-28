using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Calculator
{
    public partial class CalculatorForm : Form
    {
        public CalculatorForm()
        {
            InitializeComponent();
            radioButton10.Checked = true;
        }
        private double operand1,v;
        private string Op;
        private double Result;
        private bool res = false;
        string s1;


        //Проверка на ввод больших значений.
        bool check()
        {
            if (NumberTextBox.Text.Length >= NumberTextBox.MaxLength) return false;
            else return true;
        }
        private void button16_Click(object sender, EventArgs e)// реализация кнопки Backspace
        {
            int ind = NumberTextBox.Text.Length - 1;
            string str1 = NumberTextBox.Text.Remove(ind);
            if (NumberTextBox.Text[0] == 'н') NumberTextBox.Text = "0";
            if ((NumberTextBox.Text.Length >= 2) && (str1 != "-")) NumberTextBox.Text = NumberTextBox.Text.Remove(ind);
            else NumberTextBox.Text = "0";
            if (NumberTextBox.Text == "") NumberTextBox.Text = "0";

        }

        private void button0_Click(object sender, EventArgs e) // Ввод цифр
        {
            if (check())
            {
                if (res)
                {
                    NumberTextBox.Text = "" + (sender as Button).Text;
                    res = false;
                }
                else if (NumberTextBox.Text == "0") NumberTextBox.Text = "" + (sender as Button).Text;
                else NumberTextBox.Text = NumberTextBox.Text + (sender as Button).Text;
            }
            else NumberTextBox.Text = "ERROR";
        }


        private void Addbutton_Click(object sender, EventArgs e) // Реализация арифметических действий
        {
            operand1 = double.Parse(NumberTextBox.Text);
            NumberTextBox.Text = "0";
            Op = (sender as Button).Text;
        }

        string from = "10";
        private void Radio_Click(object sender, EventArgs e)
        {
            //Тут извлекаю СС из названия кнопки
            RadioButton rb = sender as RadioButton;
            Regex rx = new Regex(@"(\d+)", RegexOptions.Compiled);
            // перевод
            if (rb.Checked == false) from = rx.Match(rb.Text).Value;
            // куда перевожу.
            if (rb.Checked)
            {
                //Беру СС
                int ss = int.Parse(rx.Match(rb.Text).Value);
                // Возможности в системе счисления
                if (ss == 10)
                {
                    panel3.Enabled = true;
                    panel4.Enabled = true;
                    panel5.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    button7.Enabled = true;
                    button8.Enabled = true;
                    button9.Enabled = true;
                    Addbutton.Enabled = true;
                    Substractbutton.Enabled = true;
                    Multiplybutton.Enabled = true;
                    Divbutton.Enabled = true;
                    Pointbutton.Enabled = true;
                    Equalbutton.Enabled = true;
                }
                else if (ss == 2)
                {
                    Addbutton.Enabled = false;
                    Substractbutton.Enabled = false;
                    Multiplybutton.Enabled = false;
                    Divbutton.Enabled = false;
                    Pointbutton.Enabled = false;
                    panel3.Enabled = false;
                    panel4.Enabled = false;
                    panel5.Enabled = true;
                   
                }
                else
                {
                    panel3.Enabled = false;
                    panel4.Enabled = false;
                    panel5.Enabled = false;
                    Addbutton.Enabled = false;
                    Substractbutton.Enabled = false;
                    Multiplybutton.Enabled = false;
                    Divbutton.Enabled = false;
                    Pointbutton.Enabled = false;
                    Equalbutton.Enabled = false;
                }
                //Беру число из бокса и перевожу в 10СС
                long number = Convert.ToInt64(NumberTextBox.Text, Convert.ToInt32(from));
                //Перевожу куда надо
                string result = Convert.ToString(number, ss);
                //Тут настраиваю доступные цифры
                switch (ss)
                {
                    case 2:
                        NumberTextBox.MaxLength = 64;
                        button2.Enabled = false;
                        button3.Enabled = false;
                        button4.Enabled = false;
                        button5.Enabled = false;
                        button6.Enabled = false;
                        button7.Enabled = false;
                        button8.Enabled = false;
                        button9.Enabled = false;
                        break;
                    case 8:
                        NumberTextBox.MaxLength = 21;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                        button8.Enabled = false;
                        button9.Enabled = false;
                        break;
                    case 10:
                        NumberTextBox.MaxLength = 18;
                        break;
                    case 16:
                        NumberTextBox.MaxLength = 16;
                        button2.Enabled = true;
                        button3.Enabled = true;
                        button4.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        button7.Enabled = true;
                        button8.Enabled = true;
                        button9.Enabled = true;
                        break;
                }
                //Вывод результата в бокс
                NumberTextBox.Text = result;
                res = true;
            }
        }


        private void Pointbutton_Click(object sender, EventArgs e)//точка
        {
            if (!NumberTextBox.Text.Contains(','))
                NumberTextBox.Text = NumberTextBox.Text + ",";
        }

        private void Equalbutton_Click(object sender, EventArgs e)//ответ
        {
            double operand2 = double.Parse(NumberTextBox.Text);
            string s2 = NumberTextBox.Text, s3="";
            res = true;
            switch (Op)
            {
                case "+": Result = operand1 + operand2; break;
                case "-": Result = operand1 - operand2; break;
                case "*": Result = operand1 * operand2; break;
                case "/": Result = operand1 / operand2; break;
                case "st": Result = Math.Pow(v, operand2);break;
                case "and":
                    {
                        if (s1.Length > s2.Length) for (int i = s2.Length; i < s1.Length; i++) s2 = "0" + s2;
                        else for (int i = s1.Length; i < s2.Length; i++) s1 = "0" + s1;
                        for (int i = s1.Length - 1; i >= 0; i--) if ((s1[i] == '1') && (s2[i] == '1')) s3 = "1" + s3;
                            else s3="0" + s3;
                        while ((s3.Length > 0) && (s3[0] == '0'))
                               s3= s3.Remove(0, 1);
                        if (s3 == "") s3 = "0";
                        Result = int.Parse(s3);break;
                    }
                case "or":
                    {
                        if (s1.Length > s2.Length) for (int i = s2.Length; i < s1.Length; i++) s2 = "0" + s2;
                        else for (int i = s1.Length; i < s2.Length; i++) s1 = "0" + s1;
                        for (int i = s1.Length - 1; i >= 0; i--) if ((s1[i] == '1') || (s2[i] == '1')) s3 = "1" + s3;
                            else s3 = "0" + s3;
                        while ((s3.Length > 0) && (s3[0] == '0'))
                            s3 = s3.Remove(0, 1);
                        Result = int.Parse(s3); break;
                    }

                 
            }
            NumberTextBox.Text = Result.ToString();
        }

        private void Cancelbutton_Click(object sender, EventArgs e)
        {
            NumberTextBox.Text = "0";
        }

        private void NumberTextBox_TextChanged(object sender, EventArgs e)
        {
            
            if (NumberTextBox.Text.Contains("E") | NumberTextBox.Text.Contains(",")) panel6.Enabled = false;
            else
            {
                panel6.Enabled = true;
            }
                        
        }

        //Фильтрация кнопок.
        public void CalculatorForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            //тут для каждой СС создаю регулярку, чтобы отфильтровать ввод
            Regex rx;

            if (radioButton10.Checked)
            {
                rx = new Regex(@"(\d)", RegexOptions.Compiled);
                
            }
            else if (radioButton2.Checked)
            {
                rx = new Regex(@"[0-1]");
            }
            else if (radioButton8.Checked)
            {
                rx = new Regex(@"[0-7]");
            }
            else
            {
                rx = new Regex(@"\d|[a-f]", RegexOptions.IgnoreCase);
            }
            //Если введенный символ подходит, проверяется возможность добавления и он падает в бокс
            if (rx.IsMatch(e.KeyChar.ToString()))
            {
                if (check())
                {
                    if (res)
                    {
                        NumberTextBox.Text = "" + e.KeyChar;
                        res = false;
                    }
                    else if (NumberTextBox.Text == "0") NumberTextBox.Text = "" + e.KeyChar;
                    else NumberTextBox.Text = NumberTextBox.Text + e.KeyChar;
                }
            }
            int ind = NumberTextBox.Text.Length - 1;
            if (e.KeyChar == 8)
            { button16_Click(Backspacebutton, e); }
            if (e.KeyChar == '+')
            { Addbutton_Click(Addbutton, e); }
            if (e.KeyChar == '-')
            { Addbutton_Click(Substractbutton, e); }
            if (e.KeyChar == '*')
            { Addbutton_Click(Multiplybutton, e); }
            if (e.KeyChar == '/')
            { Addbutton_Click(Divbutton, e); }
            if (e.KeyChar == '=')
            { Equalbutton_Click(Equalbutton, e); }
            if (e.KeyChar == '.')
            { Pointbutton_Click(Pointbutton, e); }
            if (e.KeyChar == ',')
            { Pointbutton_Click(Pointbutton, e); }


        }

        private void znak_button_Click(object sender, EventArgs e)// +/-
        {
            double a = double.Parse(NumberTextBox.Text);
            a = -a;
            NumberTextBox.Text = a.ToString();
        }

        private void fract_button_Click(object sender, EventArgs e)// 1/x
        {
            double a = double.Parse(NumberTextBox.Text);
            a = 1 / a;
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void square_button_Click(object sender, EventArgs e)// ^2
        {
            double a = double.Parse(NumberTextBox.Text);
            a = a * a;
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void sqr_button_Click(object sender, EventArgs e)//корень
        {
            double a = double.Parse(NumberTextBox.Text);
            a = Math.Sqrt(a);
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void fact_button_Click(object sender, EventArgs e)// !
        {
            double a = double.Parse(NumberTextBox.Text);
            long b = 1;
            if ((Math.Round(a,0)!=a) || (a < 0)) MessageBox.Show("ERROR");
            else for (int i = 1; i <= a; i++) b = b * i;
            NumberTextBox.Text = b.ToString();
            res = true;
        }

        private void pi_button_Click(object sender, EventArgs e)
        {
            double a = Math.PI;
            NumberTextBox.Text = a.ToString();
        }

        private void sin_button_Click(object sender, EventArgs e)
        {
            double a = double.Parse(NumberTextBox.Text);
            a = Math.Sin(a);
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void cos_button_Click(object sender, EventArgs e)
        {
            double a = double.Parse(NumberTextBox.Text);
            a = Math.Cos(a);
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void tg_button_Click(object sender, EventArgs e)
        {
            double a = double.Parse(NumberTextBox.Text);
            a = Math.Tan(a);
            NumberTextBox.Text = a.ToString();
            res = true;
        }

        private void e_button_Click(object sender, EventArgs e)
        {
            double a = Math.E;
            NumberTextBox.Text = a.ToString();
        }

        private void exp_button_Click(object sender, EventArgs e)
        {
            double a = double.Parse(NumberTextBox.Text);
            a = Math.Exp(a);
            NumberTextBox.Text = a.ToString();
        }

        private void deg_button_Click(object sender, EventArgs e)
        {
            v = double.Parse(NumberTextBox.Text);
            NumberTextBox.Text = "";
            Op = "st";
        }

        private void or_button_Click(object sender, EventArgs e)
        {
            s1 = NumberTextBox.Text;
            NumberTextBox.Text = "0";
            Op = "or";
        }

        private void and_button_Click(object sender, EventArgs e)
        {
            s1 = NumberTextBox.Text;
            NumberTextBox.Text = "0";
            Op = "and";
        }

        private void not_button_Click(object sender, EventArgs e)
        {
            s1 = NumberTextBox.Text;
            string st2 = "";
            for (int i = 0; i < s1.Length; i++)
            {
                if (s1[i] == '0') st2 = st2 + '1';
                else st2 = st2 + '0';
            }
            while ((st2.Length > 0) && (st2[0] == '0'))
                st2 = st2.Remove(0, 1);
            if (st2 == "") st2 = "0";
           NumberTextBox.Text = st2;
            res = true;
        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {
            
        }


        private void CalculatorForm_Load(object sender, EventArgs e)
        {

        }
    }
}