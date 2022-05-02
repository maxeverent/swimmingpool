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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            Excel.Application excel_app = new Excel.Application();

                excel_app.Visible = false;

                Excel.Workbook workbook = excel_app.Workbooks.Open(
                   excel_file,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                   Type.Missing, Type.Missing);

                Excel.Worksheet sheet = excel_app.ActiveSheet;
            try
            {
                string Array = "";
                int row = listBox1.SelectedIndex;

                for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
                {
                    string fname = sheet.Cells[i, 4].Value2;
                    string sname = sheet.Cells[i, 5].Value2;
                    if (fname + " " + sname == Convert.ToString(listBox1.Items[row]))
                    {
                        Array += sheet.Cells[i, 6].Value2 + "\r\n";
                    }
                }
                workbook.Close(true, Type.Missing, Type.Missing);
                excel_app.Quit();
                if (Array == "")
                {
                    MessageBox.Show("Посещений не было");
                }
                else
                {
                    MessageBox.Show(Array);
                    Array = "";
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                workbook.Close(true, Type.Missing, Type.Missing);
                excel_app.Quit();
                MessageBox.Show("Выберите из списка");
            }            
        }
      
        private void Form5_Load(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            Excel.Application excel_app = new Excel.Application();

            excel_app.Visible = false;

            Excel.Workbook workbook = excel_app.Workbooks.Open(
               excel_file,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing);

            Excel.Worksheet sheet = excel_app.ActiveSheet;


            string fname;
            string sname;
            List<string> people = new List<string>();

            for (int i = 2; sheet.Cells[i, 1].Value2 != null; i++)
            {
                fname = sheet.Cells[i, 1].Value2;
                sname = sheet.Cells[i, 2].Value2;
                people.Add(fname + " " + sname);

            }
            listBox1.Items.AddRange(people.ToArray());
            people.Clear();

            workbook.Close(true, Type.Missing, Type.Missing);

            excel_app.Quit();
        }

        private int lastFoundIndex = -1;

        private void button2_Click(object sender, EventArgs e)
        {
            int i;
            for (i = lastFoundIndex + 1; i < listBox1.Items.Count; i++)
            {
                var currVal = listBox1.Items[i].ToString();
                if (currVal.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) > -1)
                {
                    listBox1.SetSelected(i, true);
                    lastFoundIndex = i;
                    break;
                }
            }
            if (lastFoundIndex > -1 && i == listBox1.Items.Count)
            {
                for (i = 0; i <= lastFoundIndex; i++)
                {
                    var currVal = listBox1.Items[i].ToString();
                    if (currVal.IndexOf(textBox1.Text, StringComparison.OrdinalIgnoreCase) > -1)
                    {
                        listBox1.SetSelected(i, true);
                        lastFoundIndex = i;
                        break;
                    }
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char l = e.KeyChar;
            if ((l < 'А' || l > 'я') && l != '\b' && l != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
