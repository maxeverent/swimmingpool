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
    public partial class Form4 : Form
    {

        public Form4()
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

            string Array = "";
            int row1 = listBox1.SelectedIndex;
            int row2 = listBox2.SelectedIndex;

            string[] data = { "10:00", "10:30", "11:00", "11:30", "12:00" };
            int count = 0;
            try
            {
                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string str = Convert.ToString(sheet.Cells[i, 6].Value2);
                    string[] datatime = str.Split(' ');
                    if (Convert.ToString(sheet.Cells[i, 6].Value2) == datatime[0] + " " + listBox1.Items[row1] + " " + listBox2.Items[row2] + " " + "2022")
                    {
                        count++;
                        if (count > 5)
                        {
                            data = data.Except(new string[] { datatime[0] }).ToArray();
                        }
                    }
                }
                workbook.Close(true, Type.Missing, Type.Missing);
                excel_app.Quit();

                 for (int g = 0; g < data.Length; g++)
                 {
                     Array += Convert.ToString(data[g]) + " ";
                 }
               MessageBox.Show(Array);
            }
            catch(ArgumentOutOfRangeException) 
            {
                workbook.Close(true, Type.Missing, Type.Missing);

                excel_app.Quit();
                MessageBox.Show("Выберите дату");
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string[] moths = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            string[] days = { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31" };
            listBox1.Items.AddRange(moths);
            listBox2.Items.AddRange(days);
        }
    }
}
