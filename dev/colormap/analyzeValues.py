
import matplotlib.pyplot as plt
import numpy as np


def analyze(valuesTextBlock, title):
    values = [x.strip() for x in valuesTextBlock.replace(",", " ").split(" ")]
    values = [int(x) for x in values if len(x)]
    assert (len(values) == 256)

    rgbValues = np.zeros((256, 4))
    for i, intVal in enumerate(values):
        rgbValues[i, 2] = intVal % 256
        intVal = intVal >> 8
        rgbValues[i, 1] = intVal % 256
        intVal = intVal >> 8
        rgbValues[i, 0] = intVal
        intVal = intVal >> 8
        assert (intVal == 0)
        rgbValues[i, 3] = np.sum(rgbValues[i])

    #rgbValues = rgbValues[rgbValues[:,3].argsort()]

    plt.figure(figsize=(6, 4))
    plt.grid(alpha=.2, ls='--')
    plt.plot(rgbValues[:, 0], 'r-')
    plt.plot(rgbValues[:, 1], 'g-')
    plt.plot(rgbValues[:, 2], 'b-')
    plt.plot(rgbValues[:, 3]/3, 'k:')
    plt.ylabel("Intensity by Color")
    plt.xlabel("Signal Intensity")
    plt.title(title)


if __name__ == "__main__":
    import analyzed.argo
    #analyze(analyzed.argo.myScreenshot, "Argo Local Screenshot")
    #analyze(analyzed.argo.webScreenshot, "Argo Website Screenshot")
    analyze(analyzed.argo.myScreenshot, "Argo Colormap")
    plt.show()
