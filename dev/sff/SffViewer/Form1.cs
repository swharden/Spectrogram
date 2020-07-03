using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SffViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            string defaultPath = "../../../hal.sff";
            LoadSFF(defaultPath);
        }

        private void LoadSFF(string filePath)
        {
            var sff = new Spectrogram.SFF(filePath);
            pictureBox1.Width = sff.FftWidth;
            pictureBox1.Height = sff.FftHeight;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] paths = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string path in paths)
            {
                if (path.EndsWith(".sff", StringComparison.OrdinalIgnoreCase))
                {
                    LoadSFF(path);
                    return;
                }
            }
        }
    }
}
