using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Styles___Triggers
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach(UIElement element in elementsGroup.Children)
            {
                if (element is Button btn)
                {
                    btn.Click += btn_Click;
                }
            }
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = (e.OriginalSource as Button).Content.ToString();
                if (input == "C")
                {
                    inputDisplay.Text = string.Empty;
                    resultDisplay.Text = "0";
                }
                else if (input == "CE")
                {
                    if (inputDisplay.Text.Length <= 1)
                    {
                        inputDisplay.Text = string.Empty;
                        resultDisplay.Text = "0";
                    }
                    else
                    {
                        inputDisplay.Text = inputDisplay.Text.Substring(0, inputDisplay.Text.Length - 1);
                    }
                }
                else if (input == "=")
                {
                    if (ContainsDivisionByZero(inputDisplay.Text))
                    {
                        throw new DivideByZeroException();
                    }
                    else
                    {
                        resultDisplay.Text = new DataTable().Compute(inputDisplay.Text, null).ToString();
                    }
                }
                else
                {
                    inputDisplay.Text += input;
                }
            }
            catch (DivideByZeroException)
            {
                MessageBox.Show("Ділити на нуль не можна!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Виникла помилка: \"{ex.Message}\"");
            }
        }

        private bool ContainsDivisionByZero(string expression)
        {
            char[] operators = { '+', '-', '*', '/', '%' };
            string[] parts = expression.Split(operators);
            foreach (string part in parts)
            {
                if (part.Trim() == "0" && !IsFirstOperand(expression, part))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsFirstOperand(string expression, string operand)
        {
            int index = expression.IndexOf(operand);
            return index == 0;
        }
    }
}