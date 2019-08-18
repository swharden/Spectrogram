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
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            waterfall1.StartListening(deviceIndex: 0);
        }

        private void UpdateWaterfallPlot()
        {
            scottPlotUC1.plt.Clear();

            var fft = waterfall1.spec.fftList[waterfall1.spec.fftList.Count - 1];
            double[] fft2 = new double[fft.Length];
            for (int i = 0; i < fft.Length; i++)
                fft2[i] = fft[i];

            scottPlotUC1.plt.PlotSignal(fft2, 1.0 / waterfall1.spec.fftSettings.fftResolution,
                markerSize: 0, color: Color.Black);
            scottPlotUC1.plt.AxisAuto(0, .01, xExpandOnly: false, yExpandOnly: true);

            scottPlotUC1.plt.Grid(false);
            scottPlotUC1.plt.Style(dataBg: SystemColors.Control);
            scottPlotUC1.plt.Frame(drawFrame: false);
            scottPlotUC1.plt.TightenLayout(padding: 0);

            scottPlotUC1.Render();
        }

        private void UpdateSignalPlot()
        {
            scottPlotUC2.plt.Clear();

            int pointCount = 1000;
            var sigList = waterfall1.spec.signal;
            var lastSamples = sigList.GetRange(sigList.Count - pointCount, pointCount);

            double[] pcm = new double[pointCount];
            for (int i = 0; i < pointCount; i++)
                pcm[i] = lastSamples[i];

            scottPlotUC2.plt.PlotSignal(pcm, 1.0 / waterfall1.spec.fftSettings.fftResolution,
                markerSize: 0, color: Color.Black);
            scottPlotUC1.plt.AxisAuto(0, .01, xExpandOnly: false, yExpandOnly: true);

            scottPlotUC2.plt.Grid(false);
            scottPlotUC2.plt.Style(dataBg: SystemColors.Control);
            scottPlotUC2.plt.Frame(drawFrame: false);
            scottPlotUC2.plt.TightenLayout(padding: 0);

            scottPlotUC2.Render();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if ((waterfall1.spec.fftList == null) || (waterfall1.spec.fftList.Count == 0))
                return;

            UpdateSignalPlot();
            UpdateWaterfallPlot();
        }
    }
}
