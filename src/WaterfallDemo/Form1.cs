using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterfallDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            waterfall1.intensity = 10;

            scottPlotUC1.plt.Ticks(displayTicksY: false);
            scottPlotUC1.plt.TightenLayout(0);
            scottPlotUC1.plt.Grid(false);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waterfall1.StartListening(deviceIndex: 0);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if ((waterfall1.spec.fftList == null) || (waterfall1.spec.fftList.Count == 0))
                return;

            scottPlotUC1.plt.Clear();

            var fft = waterfall1.spec.fftList[waterfall1.spec.fftList.Count - 1];
            double[] fft2 = new double[fft.Length];
            for (int i = 0; i < fft.Length; i++)
                fft2[i] = fft[i];

            scottPlotUC1.plt.PlotSignal(fft2, 1.0/waterfall1.spec.fftSettings.fftResolution, markerSize: 0);
            scottPlotUC1.plt.AxisAuto();
            scottPlotUC1.Render();
        }
    }
}
