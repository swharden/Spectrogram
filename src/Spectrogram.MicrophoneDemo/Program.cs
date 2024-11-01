using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Spectrogram.MicrophoneDemo
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMicrophone());
        }
    }
}
