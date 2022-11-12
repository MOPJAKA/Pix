using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programm
{
    public partial class Form1 : Form
    {

        private List<Bitmap> _bitmap = new List<Bitmap>();

        public Form1()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (_bitmap == null || _bitmap.Count == 0)
                return;

            pictureBox1.Image = _bitmap[trackBar1.Value - 1];
        }

        private async void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (_bitmap != null || _bitmap.Count != 0)
                    _bitmap.Clear();

                await Task.Run(() => { pictureBox1.Image = new Bitmap(openFileDialog1.FileName); });

                if (pictureBox1.Image.Width < 100 && pictureBox1.Image.Height < 100)
                {
                    pictureBox1.Image = null;
                    MessageBox.Show("Min Size Image: 100x100px");
                    return;
                }

                trackBar1.Enabled = false;

                _bitmap.Add(new Bitmap(openFileDialog1.FileName));

                pic Original = new pic(pictureBox1.Image);

                await Task.Run(() => { Original.Pixilization(pictureBox1.Image, _bitmap); });

                trackBar1.Enabled = true;
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try {pictureBox1.Image.Save(saveFileDialog1.FileName);}

                catch {MessageBox.Show("Невохможно сохранить изображение");}
            }
        }

        
    }
}
