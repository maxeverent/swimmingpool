using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace practice1
{
    public partial class Form6 : Form
    {       
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Пустые окна");
            }
            else
            {
                Excel.Application excel_app = new Excel.Application();

                excel_app.Visible = false;

                Excel.Workbook workbook = excel_app.Workbooks.Open(
                   excel_file,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing);

                Excel.Worksheet sheet = excel_app.ActiveSheet;

                int count = 2;
                int equel = 0;

                for (int i = 2; sheet.Cells[i, 1].Value2 != null; i++)
                {
                    if (Convert.ToString(sheet.Cells[i, 1].Value2) == textBox1.Text && Convert.ToString(sheet.Cells[i, 2].Value2) == textBox2.Text)
                    {
                        equel++;
                    }
                }

                if (equel > 0)
                {
                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();
                    MessageBox.Show("Такой клиент уже существует");
                }
                else
                {
                    for (int i = 2; sheet.Cells[i, 1].Value2 != null; i++)
                    {
                        count++;
                    }

                    sheet.Cells[count, 1] = textBox1.Text;
                    sheet.Cells[count, 2] = textBox2.Text;

                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();

                    MessageBox.Show("Клиент добавлен");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 1)
                textBox1.Text = textBox1.Text.ToUpper();
            textBox1.Select(textBox1.Text.Length, 0);
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 1)
                textBox2.Text = textBox2.Text.ToUpper();
            textBox2.Select(textBox2.Text.Length, 0);
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b')
            {
                e.Handled = true;
            }
        }
    }
}
