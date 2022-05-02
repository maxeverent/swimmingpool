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
    public partial class Form3 : Form
    {      
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            Excel.Application excel_app = new Excel.Application();

                // Сделать Excel видимым (необязательно).
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
                int row = listBox1.SelectedIndex;

                for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
                {
                    string[] zapis = Convert.ToString(listBox1.Items[row]).Split(' ');
                    if (zapis[0] == sheet.Cells[i, 4].Value2 && zapis[1] == sheet.Cells[i, 5].Value2 && zapis[2] + " " + zapis[3] + " " + zapis[4] + " " + zapis[5] == sheet.Cells[i, 6].Value2)
                    {
                        sheet.Cells[i, 4].Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        sheet.Cells[i, 5].Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                        sheet.Cells[i, 6].Delete(Excel.XlDeleteShiftDirection.xlShiftUp);
                    }
                }
                workbook.Close(true, Type.Missing, Type.Missing);

                excel_app.Quit();
                MessageBox.Show("Запись удалена");
            }
            catch (ArgumentOutOfRangeException)
            {
                workbook.Close(true, Type.Missing, Type.Missing);

                excel_app.Quit();
                MessageBox.Show("Выберите запись");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            listBox1.Items.Clear();
            Excel.Application excel_app = new Excel.Application();

            // Сделать Excel видимым (необязательно).
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
            string date;
            List<string> zapis = new List<string>();

            for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
            {
                fname = sheet.Cells[i, 4].Value2;
                sname = sheet.Cells[i, 5].Value2;
                date = sheet.Cells[i, 6].Value2;
                zapis.Add(fname + " " + sname + " " + date);
            }

            listBox1.Items.AddRange(zapis.ToArray());
            zapis.Clear();

            workbook.Close(true, Type.Missing, Type.Missing);

            excel_app.Quit();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;//path to exe file
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            Excel.Application excel_app = new Excel.Application();

            // Сделать Excel видимым (необязательно).
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
            string date;
            List<string> zapis = new List<string>();

            for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
            {
                fname = sheet.Cells[i, 4].Value2;
                sname = sheet.Cells[i, 5].Value2;
                date = sheet.Cells[i, 6].Value2;
                zapis.Add(fname + " " + sname + " " + date);
            }

            listBox1.Items.AddRange(zapis.ToArray());
            zapis.Clear();

            workbook.Close(true, Type.Missing, Type.Missing);

            excel_app.Quit();
        }
    }
}
