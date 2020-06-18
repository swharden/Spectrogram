"""
This script converts matplotlib colormaps to a format C# can use.
"""

import matplotlib.pyplot as plt
import numpy as np

TEMPLATE = """using System;

namespace Spectrogram.Colormaps
{
    class CLASSNAME : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the CLASSNAME colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            VALUES
        };
    }
}
"""


def plotCmap(rgbValues, title="colormap"):
    assert isinstance(rgbValues, np.ndarray)
    assert rgbValues.shape == (256, 3)

    plt.figure(figsize=(6, 4))
    plt.grid(alpha=.2, ls='--')
    plt.plot(rgbValues[:, 0], 'r-')
    plt.plot(rgbValues[:, 1], 'g-')
    plt.plot(rgbValues[:, 2], 'b-')
    plt.plot(np.sum(rgbValues, 1)/3, 'k:')
    plt.ylabel("Intensity by Color")
    plt.xlabel("Signal Intensity")
    plt.title(title)
    plt.show()


def cmapToRGB(cmapName="jet"):
    cmap = plt.get_cmap(cmapName)
    rgbValues = np.zeros((256, 3))
    for i in range(256):
        r, g, b, a = cmap(i/255)
        rgbValues[i, 0] = int(r*255)
        rgbValues[i, 1] = int(g*255)
        rgbValues[i, 2] = int(b*255)
    return rgbValues


def rgbToInt(rgbValues):
    assert isinstance(rgbValues, np.ndarray)
    assert rgbValues.shape == (256, 3)
    ints = np.zeros(256, dtype=int)
    for i in range(256):
        r, g, b = rgbValues[i]
        ints[i] = int(r * 2**16 + g * 2**8 + b)
    return ints


def isStepped(cmapName):
    cmap = plt.get_cmap(cmapName)
    return cmap(0/255) == cmap(1/255)


def makeCsClass(cmapName, intValues):
    className = cmapName.capitalize()
    className = className.replace("_r", "Reversed")
    txt = TEMPLATE.replace("CLASSNAME", className)

    valBlock = ""
    for i in range(256):
        valBlock += f"{intValues[i]:08d}, "
        if i % 8 == 7 and i < 255:
            valBlock += "\n            "
    txt = txt.replace("VALUES", valBlock)

    with open(f"src/Spectrogram/Colormaps/{className}.cs", 'w') as f:
        f.write(txt)

    return f"public static Colormap {className} => new Colormap(new Colormaps.{className}());"


if __name__ == "__main__":

    statics = []
    for cmapName in plt.colormaps():
        if "_" in cmapName.replace("_r", ""):
            continue
        if isStepped(cmapName):
            continue
        print(f"analyzing {cmapName}...")
        rgbValues = cmapToRGB(cmapName)
        intValues = rgbToInt(rgbValues)
        statics.append(makeCsClass(cmapName, intValues))
    statics.sort()
    print("\n".join(statics))
    print("DONE")
