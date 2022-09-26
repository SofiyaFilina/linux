using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace linux
{
    public partial class Form1 : Form
    {
        PrintDocument PD = new PrintDocument();
        public Form1()
        {
            InitializeComponent();
            foreach (var printer in PrinterSettings.InstalledPrinters)
                comboBox1.Items.Add(printer);
            comboBox1.Text = comboBox1.Items[0].ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void PD_PrintPage(object sender, PrintPageEventArgs e)
        {
            Font PrintFont = new Font("Times New Roman", 6, FontStyle.Regular,
            GraphicsUnit.Millimeter);
            e.Graphics.DrawImage(pictureBox1.Image, new Rectangle
           (e.MarginBounds.Left, e.MarginBounds.Top, e.MarginBounds.Right - e.MarginBounds.Left,
           pictureBox1.Image.Height));
            string PrintText = textBox1.Text;
            e.Graphics.DrawString(PrintText, PrintFont, Brushes.Black, new PointF
           (e.MarginBounds.Left, pictureBox1.Image.Height + 120));
            // Отступ после изображения
            Graphics g = e.Graphics;
            int x = 0;
            int y = pictureBox1.Image.Height + 150;
            // таблица
            int cell_height = 0;
            int colCount = dataGridView1.ColumnCount;
            int rowCount = dataGridView1.RowCount;
            int current_col = 0;
            int current_row = 0;
            string value = "";
            int width = dataGridView1[current_col, current_row].Size.Width;
            int height = dataGridView1[current_col, current_row].Size.Height;
            Rectangle cell_border;
            SolidBrush brush = new SolidBrush(Color.Black);

            while (current_col < colCount)
            {
                x += dataGridView1[current_col, current_row].Size.Width;
                cell_height = dataGridView1[current_col, current_row].Size.Height;
                cell_border = new Rectangle(x, y, width, height);

                value = dataGridView1.Columns[current_col].HeaderText.ToString();
                g.DrawRectangle(new Pen(Color.Black), cell_border);
                g.DrawString(value, new Font("Tahoma", 10, GraphicsUnit.Point), brush,
               x, y);

                current_col++;
            }
            while (current_row < rowCount)
            {
                while (current_col < colCount)
                {
                    x += dataGridView1[current_col, current_row].Size.Width;
                    cell_height = dataGridView1[current_col, current_row].Size.Height;
                    cell_border = new Rectangle(x, y, width, height);

                    value = dataGridView1[current_col, current_row - 1].Value.ToString
                   ();
                    g.DrawRectangle(new Pen(Color.Black), cell_border);
                    g.DrawString(value, new Font("Tahoma", 10, FontStyle.Regular,
                   GraphicsUnit.Point), brush, x, y);
                    current_col++;
                }
                current_col = 0;
                current_row++;
                x = 0;
                y += cell_height;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Bitmap image;
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.ShowDialog();
            image = new Bitmap(open_dialog.FileName);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Image = image;
            pictureBox1.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
            PrintPreviewDialog diag = new PrintPreviewDialog();
            diag.Document = PD;
            diag.ShowDialog();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PrinterSettings PS = new PrinterSettings();
            PS.PrinterName = comboBox1.SelectedItem.ToString();
            PD.PrintPage += new PrintPageEventHandler(PD_PrintPage);
            PD.Print();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
