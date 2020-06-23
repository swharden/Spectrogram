"""
Attempt to mimic LOPORA colormap from
https://github.com/swharden/Lopora/blob/20afe72416579f8b7d3c8861532c71a95b904066/src/LOPORA-v5a.py#L828-L872
"""
import numpy
import matplotlib.pyplot as plt


def doit(intensityCompression=2, pointCount=256):

    Rs = numpy.ones(pointCount)
    Gs = numpy.ones(pointCount)
    Bs = numpy.ones(pointCount)

    for n in range(pointCount):

        frac = n / len(Rs)
        v = frac ** (1.0 / (1.0 + float(intensityCompression) / 2.0))

        R = int(v * v * 255 + 0.5)
        Rs[n] = int(min(max(R, 0), 255))

        G = int(v * 255 + 0.5)
        Gs[n] = int(min(max(G, 0), 255))

        B = int(255 * numpy.sqrt(v) + 0.5)
        Bs[n] = int(min(max(B, 0), 255))

    return [Rs, Gs, Bs]


def rgbToInt32(r, g, b):
    return int(r * 2**16 + g * 2**8 + b)


if __name__ == "__main__":

    [r, g, b] = doit()

    for i in range(256):
        int32 = rgbToInt32(r[i], g[i], b[i])
        print(f"{int32:010d}, ", end='')
        if i % 8 == 7:
            print()

    # plt.plot(r, 'r-', alpha=.5)
    # plt.plot(g, 'g-', alpha=.5)
    # plt.plot(b, 'b-', alpha=.5)

    # plt.grid(alpha=.2, ls='--')
    # plt.title("Lopora Style Colormap")
    # plt.xlabel("input value")
    # plt.ylabel("color intensity")
    # plt.tight_layout()
    # plt.show()

    print("DONE")
