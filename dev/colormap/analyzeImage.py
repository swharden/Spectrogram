"""
This script reverse-engineers the pallette for a spectrogram.
It takes in an image, explodes it into pixels, finds all the
unique values, then attempts to sort them by intensity.
"""

from matplotlib.image import imread
import numpy as np


def rgbToInt32(r, g, b):
    return int(r * 2**16 + g * 2**8 + b)


def valuesInImage(filePath):
    im = imread(filePath)
    pixelValues = np.zeros((im.shape[0], im.shape[1]), dtype=int)
    for y in range(im.shape[0]):
        for x in range(im.shape[1]):
            r, g, b = im[y, x]
            pixelValues[y, x] = rgbToInt32(r * 255, g * 255, b * 255)
    pixelValues = np.unique(pixelValues)
    for i, val in enumerate(pixelValues):
        print(f"{val:08d}", end=", ")
        if (i % 8 == 7):
            print()
    print(f"Found {len(pixelValues)} unique pixel values")


if __name__ == "__main__":
    assert rgbToInt32(111, 222, 121) == 7331449
    pixelValues = valuesInImage("analyzed/argo1.png")
    print("DONE")
