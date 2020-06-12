import os
import matplotlib.pyplot as plt
import sffLib

if __name__ == "__main__":
    complexValues = sffLib.SpectrogramFile(os.path.dirname(__file__)+"/complex.sff").values

    # values is a 2D numpy array of Complex values
    print("DATA TYPE:", type(complexValues))
    print("DATA SHAPE:", complexValues.shape)

    # you can work with individual Complex data values 
    # using X/Y coordinates (X is time, Y is frequency)
    print("EXAMPLE VALUE:", complexValues[3, 5])
