import sffLib
import os
import matplotlib.pyplot as plt
import numpy as np
np.set_printoptions(precision=3)


def plotSFF(filePath, show=False):

    # access SFF details and data like this
    sf = sffLib.SpectrogramFile(filePath)
    print(sf.getDescription())
    print(sf.values)

    # rotate values for display as a pseudocolor mesh
    rotatedValues = np.rot90(sf.values, 1)[::-1]

    # plot the spectrogram
    plt.figure()
    plt.pcolormesh(rotatedValues)
    plt.colorbar()
    plt.title(f"{os.path.basename(filePath)} Spectrogram")
    plt.savefig(os.path.basename(filePath)+".png")
    if show:
        plt.show()
    plt.close()


if __name__ == "__main__":
    plotSFF("../hal.sff", True)
    plotSFF("../halMel.sff", True)
