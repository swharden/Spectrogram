using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRSS_Viewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            spectrogramViewer1.Start(preLoadWavFile: "qrss.wav");

            //spectrogramViewer1.Start();
            spectrogramViewer1.SetIntensity(5);
        }
    }
}
