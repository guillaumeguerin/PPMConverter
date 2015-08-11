using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PNMConverter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "All Images|*.BMP;*.DIB;*.RLE;*.JPG;*.JPEG;*.JPE;*.JFIF;*.GIF;*.TIF;*.TIFF;*.PNG";
            openFileDialog1.FilterIndex = 1;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBoxPath.Text = openFileDialog1.FileName;
            }
        }

        private void buttonConvert_Click(object sender, EventArgs e)
        {
            if (textBoxPath.Text == String.Empty) {
                MessageBox.Show("You have to select an image");
            }
            else {
                System.Drawing.Bitmap b = new System.Drawing.Bitmap(textBoxPath.Text);
                StringBuilder ppm = processImage(b);
                using (System.IO.StreamWriter outfile = new System.IO.StreamWriter(textBoxPath.Text + ".ppm"))
                {
                    outfile.Write(ppm.ToString());
                }
                MessageBox.Show("Done");
            }
        }

        private StringBuilder processImage(System.Drawing.Bitmap bmp) {
            StringBuilder res = new StringBuilder();
            res.AppendLine("P3");
            res.AppendLine(bmp.Width + " " + bmp.Height);
            res.AppendLine("255");
            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    Color clr = bmp.GetPixel(x, y);
                    int red = clr.R;
                    int green = clr.G;
                    int blue = clr.B;
                    if (x > 0) {
                        res.Append(" ");
                    }
                    res.Append(red + " " + green + " " + blue);
                }
                res.AppendLine();
            }
            return res;
        }
    }
}
