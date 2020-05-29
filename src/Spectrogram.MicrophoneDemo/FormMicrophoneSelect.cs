using System;
using System.Windows.Forms;

namespace Spectrogram.MicrophoneDemo
{
    public partial class FormMicrophoneSelect : Form
    {
        int[] deviceIndex;

        public FormMicrophoneSelect(int[] deviceIndex)
        {
            InitializeComponent();

            if (deviceIndex.Length == 1)
                this.deviceIndex = deviceIndex;
            else
                throw new ArgumentException("deviceIndex should have a length of 1");

            if (NAudio.Wave.WaveIn.DeviceCount == 0)
                NoDevicesFound();
            else
                ListDevices();
        }

        private void NoDevicesFound()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("ERROR: no audio input devices found");
            deviceIndex[0] = -1;
            btnSelect.Enabled = false;
        }

        private void ListDevices()
        {
            listBox1.Items.Clear();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                string name = NAudio.Wave.WaveIn.GetCapabilities(i).ProductName;
                if (name.Length == 31)
                    name += "...";
                listBox1.Items.Add(name);
            }

            listBox1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            deviceIndex[0] = -1;
            Close();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            deviceIndex[0] = listBox1.SelectedIndex;
            Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSelect.Enabled = (listBox1.SelectedIndex >= 0);
        }
    }
}
