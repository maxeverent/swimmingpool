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
    public partial class Form7 : Form
    {
        public Form7()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
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

                int row = listBox1.SelectedIndex;
                int count_months = 0;
                int price = 300;
                int cost = 0;

                for (int i = 2; sheet.Cells[i, 6].Value2 != null; i++)
                {
                    string[] date = Convert.ToString(sheet.Cells[i, 6].Value2).Split(' ');
                    if (date[1] == Convert.ToString(listBox1.Items[row]))
                    {
                        count_months++;
                    }
                }

                if (count_months > 0)
                {
                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();
                    cost = count_months * price;
                    MessageBox.Show($"{cost}" + " " + "руб");
                }
                else
                {
                    workbook.Close(true, Type.Missing, Type.Missing);

                    excel_app.Quit();
                    MessageBox.Show("Посещений в этом месяце не было");
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                MessageBox.Show("Выберите месяц");
            }
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            string[] moths = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };
            listBox1.Items.AddRange(moths);
        }
    }
}
