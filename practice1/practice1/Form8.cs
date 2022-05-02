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
    public partial class Form8 : Form
    {
        public Form8()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
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

            int count = 2;
            int vremya = 0;
            int equel = 0;
            int row = listBox1.SelectedIndex;
            int row2 = listBox2.SelectedIndex;
            string datatime = dateTimePicker1.Text;

            string[] data = { "10:00", "10:30", "11:00", "11:30", "12:00" };
            string[] date = Convert.ToString(dateTimePicker1.Text).Split(' ');

            try
            {
                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string[] qwe = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');

                    if (qwe[0] == Convert.ToString(listBox2.Items[row2]) && qwe[1] + " " + qwe[2] + " " + qwe[3] == dateTimePicker1.Text )
                    {
                        vremya++;
                    }

                }

                string listbox = Convert.ToString(listBox1.Items[row]);
                string[] lb = listbox.Split(' ');

                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string[] qwe = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');
                    if (sheet.Cells[i, 4].Value2 == lb[0] && sheet.Cells[i, 5].Value2 == lb[1] && Convert.ToString(listBox2.Items[row2]) == qwe[0] && qwe[1] + " " + qwe[2] + " " + qwe[3] == dateTimePicker1.Text)
                    {
                        equel++;
                    }

                }
                if (equel > 0)
                {
                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();
                    MessageBox.Show("Такая запись уже существует");
                }
                else 
                {
                    if (DateTime.Compare(Convert.ToDateTime(listBox2.Items[row2] + dateTimePicker1.Text), DateTime.Now) < 0)
                    {
                        workbook.Close(true, Type.Missing, Type.Missing);

                        excel_app.Quit();
                        MessageBox.Show("Нельзя записаться на прошлое");
                    }
                    else
                    {
                        if (vremya > 5)
                        {
                            workbook.Close(true, Type.Missing, Type.Missing);

                            excel_app.Quit();
                            MessageBox.Show("Это время занято");
                        }
                        else
                        {
                            for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
                            {
                                count++;
                            }
                            sheet.Cells[count, 4] = lb[0];
                            sheet.Cells[count, 5] = lb[1];
                            sheet.Cells[count, 6] = listBox2.Items[row2] + " " + dateTimePicker1.Text;

                            workbook.Close(true, Type.Missing, Type.Missing);

                            excel_app.Quit();
                            MessageBox.Show("Запись добавлена");
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                workbook.Close(true, Type.Missing, Type.Missing);

                excel_app.Quit();
                MessageBox.Show("Выберите клиента");
            }
        }

        private void Form8_Load(object sender, EventArgs e)
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

            string[] data = { "10:00", "10:30", "11:00", "11:30", "12:00" };
            listBox2.Items.AddRange(data);
        }
    }
}
