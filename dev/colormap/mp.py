import matplotlib.pyplot as plt
import pyperclip
import numpy as np


def cmapToCode(cmapName="inferno"):
    cmap = plt.get_cmap(cmapName)

    out = f"""\n        // {cmapName}\n"""
    out += """        public readonly byte[,] RGB =\n        {\n"""
    for i in range(256):
        r, g, b, a = cmap(i/255.0)
        out += "            {%d, %d, %d},\n" % (r*256, g*256, b*256)
    out += "        };\n"

    print(out)
    pyperclip.copy(out)
    print(f"Colormap {cmapName} copied to clipboard.")


def rgbToInt32(r, g, b):
    return int(r * 2**16 + g * 2**8 + b)


def cmapToInt32s(cmapName="viridis"):
    cmap = plt.get_cmap(cmapName)
    for i in range(256):
        r, g, b, a = cmap(i/255.0)
        int32 = rgbToInt32(r*255, g*255, b*255)
        print(f"{int32:010d}, ", end='')
        if i % 8 == 7:
            print()


def plotCmapCurves(cmapName="viridis"):
    cmap = plt.get_cmap(cmapName)
    rgbValues = np.zeros((256, 4))
    for i in range(256):
        r, g, b, a = cmap(i/255.0)
        rgbValues[i, 0] = r*255
        rgbValues[i, 1] = g*255
        rgbValues[i, 2] = b*255
        rgbValues[i, 3] = np.sum(rgbValues[i])

    plt.figure(figsize=(6, 4))
    plt.grid(alpha=.2, ls='--')
    plt.plot(rgbValues[:, 0], 'r-')
    plt.plot(rgbValues[:, 1], 'g-')
    plt.plot(rgbValues[:, 2], 'b-')
    plt.plot(rgbValues[:, 3]/3, 'k:')
    plt.ylabel("Intensity by Color")
    plt.xlabel("Signal Intensity")
    plt.title(f"{cmapName} colormap")


def colormapToIntegerText(cmapName="viridis"):
    txt = f"# colormap {cmapName}\n"

    cmap = plt.get_cmap(cmapName)
    for i in range(256):
        r, g, b, a = cmap(i/255.0)
        r = int(r*255)
        g = int(g*255)
        b = int(b*255)
        val = r << 16 | g << 8 | b
        txt += f"{val:08d}, "
        if i % 8 == 7:
            txt += "\n"

    with open(f"analyzed2/{cmapName}.csv", 'w') as f:
        f.write(txt)


if __name__ == "__main__":

    cmapNames = ["cividis", "gray", "inferno",
                 "jet", "magma", "plasma", "viridis"]

    for cmapName in cmapNames:
        print(f"analyzing {cmapName}...")
        colormapToIntegerText(cmapName)
        plotCmapCurves(cmapName)
        plt.savefig(f"analyzed2/{cmapName}.png")
        plt.close()

print("DONE")
