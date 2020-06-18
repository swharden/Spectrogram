import os
import matplotlib.pyplot as plt
import numpy as np
import sffLib

if __name__ == "__main__":

    # hal.sff is a file of stored FFT magnitude (not complex data)
    sf = sffLib.SpectrogramFile(os.path.dirname(__file__)+"/hal.sff")

    # plot the spectrogram as a heatmap
    freqs = np.arange(sf.values.shape[1]) * sf.hzPerPx / 1000
    times = np.arange(sf.values.shape[0]) * sf.secPerPx
    plt.pcolormesh(freqs, times, sf.values)

    # decorate the plot
    plt.colorbar()
    plt.title("Spectrogram Magnitude (RMS)")
    plt.ylabel("Time (seconds)")
    plt.xlabel("Frequency (kHz)")
    plt.savefig(os.path.dirname(__file__)+"/hal.png")
    plt.show()
