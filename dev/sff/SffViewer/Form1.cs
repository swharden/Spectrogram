using Spectrogram;
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
        Colormap[] colormaps = Colormap.GetColormaps();
        public Form1()
        {
            InitializeComponent();
            cbColormap.Items.AddRange(colormaps.Select(x => x.Name).ToArray());
            cbColormap.SelectedIndex = cbColormap.Items.IndexOf("Viridis");

            string startupSffFile = "../../../hal.sff";
            if (System.IO.File.Exists(startupSffFile))
                LoadSFF(startupSffFile);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        SFF sff;
        private void LoadSFF(string filePath)
        {
            sff = new SFF(filePath);
            Redraw();
        }

        private void Redraw()
        {
            if (sff is null)
                return;
            Colormap cmap = colormaps[cbColormap.SelectedIndex];
            Bitmap bmp = Spectrogram.Image.GetBitmap(sff.Ffts, cmap, tbBrightness.Value * .2, cbDecibels.Checked);
            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
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
                    lblFileName.Text = System.IO.Path.GetFileName(path);
                    LoadSFF(path);
                    return;
                }
            }
        }

        private void cbColormap_SelectedIndexChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void cbDecibels_CheckedChanged(object sender, EventArgs e)
        {
            Redraw();
        }

        private void tbBrightness_Scroll(object sender, EventArgs e)
        {
            Redraw();
        }

        private void cbStretch_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = cbStretch.Checked ? PictureBoxSizeMode.Zoom : PictureBoxSizeMode.Normal;
        }
    }
}
