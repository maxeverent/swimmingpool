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
    public partial class Form2 : Form
    {
       
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
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

            int row = listBox1.SelectedIndex;
            int row2 = listBox2.SelectedIndex;
            int row3 = listBox3.SelectedIndex;
            int vremya = 0;
            int count = 2;
            int equel = 0;
            try
            {
                string[] users = Convert.ToString(listBox3.Items[row3]).Split(' ');
            
                string[] str = Convert.ToString(listBox1.Items[row]).Split(' ');

                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string[] qwe = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');
                    if (qwe[0] == Convert.ToString(listBox2.Items[row2]) && qwe[1] + " " + qwe[2] + " " + qwe[3] == dateTimePicker1.Text)
                    {
                        vremya++;
                    }
                }
                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string[] qwe1 = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');
                    count++;
                    if (str[0] == Convert.ToString(sheet.Cells[i, 4].Value2) && str[1] == Convert.ToString(sheet.Cells[i, 5].Value2) && str[2] == qwe1[0] && str[3] == qwe1[1] && str[4] == qwe1[2] && str[5] == qwe1[3])
                    {
                        break;
                    }
                }

                for (int i = 2; sheet.Cells[i, 4].Value2 != null; i++)
                {
                    string[] qwe = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');
                    if (sheet.Cells[i, 4].Value2 == users[0] && sheet.Cells[i, 5].Value2 == users[1] && qwe[0] == Convert.ToString(listBox2.Items[row2]) && qwe[1] + " " + qwe[2] + " " + qwe[3] == dateTimePicker1.Text)
                    {
                        equel++;
                    }
                }

                if (equel != 0)
                {
                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();
                    MessageBox.Show("Такая запись уже существует");
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
                        if (DateTime.Compare(Convert.ToDateTime(listBox2.Items[row2] + dateTimePicker1.Text), DateTime.Now) < 0)
                        {
                            workbook.Close(true, Type.Missing, Type.Missing);

                            excel_app.Quit();
                            MessageBox.Show("Нельзя записаться на прошлое");
                        }
                        else
                        {

                            sheet.Cells[count - 1, 4] = users[0];
                            sheet.Cells[count - 1, 5] = users[1];
                            sheet.Cells[count - 1, 6] = listBox2.Items[row2] + " " + dateTimePicker1.Text;

                            workbook.Close(true, Type.Missing, Type.Missing);

                            excel_app.Quit();
                            MessageBox.Show("Запись изменена");
                        }
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                workbook.Close(true, Type.Missing, Type.Missing);

                excel_app.Quit();
                MessageBox.Show("Выберите запись или время");
            }
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "HH:mm MMMM dd yyyy";
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
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
            string[] data = { "10:00", "10:30", "11:00", "11:30", "12:00" };
            listBox2.Items.AddRange(data);

            string f_name;
            string s_name;
            List<string> people = new List<string>();

            for (int i = 2; sheet.Cells[i, 1].Value2 != null; i++)
            {
                f_name = sheet.Cells[i, 1].Value2;
                s_name = sheet.Cells[i, 2].Value2;
                people.Add(f_name + " " + s_name);

            }
            listBox3.Items.AddRange(people.ToArray());
            people.Clear();

            workbook.Close(true, Type.Missing, Type.Missing);

            excel_app.Quit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var exePath = AppDomain.CurrentDomain.BaseDirectory;
            var excel_file = Path.Combine(exePath, "excelfile.xlsx");

            listBox1.Items.Clear();

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
  
            label4.Visible = false;
            listBox2.Visible = false;
            listBox3.Visible = false;
            label7.Visible = false;
            dateTimePicker1.Visible = false;
            button1.Visible = false;
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                label4.Visible = true;
                listBox2.Visible = true;
                listBox3.Visible = true;
                label7.Visible = true;
                dateTimePicker1.Visible = true;
                button1.Visible = true;
            }
        }
    }
}
