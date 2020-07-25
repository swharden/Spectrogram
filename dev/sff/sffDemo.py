import os
import matplotlib.pyplot as plt
import numpy as np
import sffLib

def plotSFF(filePath, show = False):

    sf = sffLib.SpectrogramFile(filePath)

    # rotate values for display as a pseudocolor mesh
    rotatedValues = np.rot90(sf.values, 1)[::-1]

    plt.figure()
    plt.pcolormesh(rotatedValues)
    plt.colorbar()
    plt.title(f"{filePath} Spectrogram")
    plt.savefig(filePath.replace(".sff", ".png"))
    if show:
        plt.show()
    plt.close()

if __name__ == "__main__":
    plotSFF("hal.sff")
    plotSFF("halMel.sff")


