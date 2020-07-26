import os
import numpy as np
import struct
import datetime
import time
import math


class SpectrogramFile:

    def __init__(self, filePath):

        timeStart = time.perf_counter()

        print(f"Spectrogram from file: {os.path.basename(filePath)}")
        self.filePath = os.path.abspath(filePath)

        with open(filePath, 'rb') as f:
            filebytes = f.read()

        # validate file format
        magicNumber = struct.unpack("<l", filebytes[0:4])[0]
        if magicNumber != 1179014099:
            raise Exception("invalid file format (based on first 4 bytes)")

        # read version
        self.versionMajor = int(filebytes[40])
        self.versionMinor = int(filebytes[41])

        # read time information
        self.sampleRate = struct.unpack("<l", filebytes[42:46])[0]
        self.stepSize = struct.unpack("<l", filebytes[46:50])[0]
        self.stepCount = struct.unpack("<l", filebytes[50:54])[0]

        # read frequency information
        self.fftSize = struct.unpack("<l", filebytes[54:58])[0]
        self.fftFirstIndex = struct.unpack("<l", filebytes[58:62])[0]
        self.fftHeight = struct.unpack("<l", filebytes[62:66])[0]
        self.offsetHz = struct.unpack("<l", filebytes[66:70])[0]

        # data format
        self.valuesPerPoint = int(filebytes[70])
        self.isComplex = int(self.valuesPerPoint) == 2
        self.bytesPerValue = int(filebytes[71])
        self.decibels = int(filebytes[72]) == 1

        # new variables
        self.melBinCount = int(filebytes[84])
        self.imageHeight = int(filebytes[88])
        self.imageWidth = int(filebytes[92])

        # useful class properties
        self.secPerPx = self.stepSize / self.sampleRate
        self.hzPerPx = self.sampleRate / self.fftSize

        # recording start time - no longer used
        # dt = datetime.datetime(
        #int(filebytes[74])+2000, int(filebytes[75]), int(filebytes[76]),
        # int(filebytes[77]), int(filebytes[78]), int(filebytes[79]))
        #print(f"Recording start (UTC): {dt}")

        # data storage
        self.firstDataByte = struct.unpack("<l", filebytes[80:84])[0]

        # read data values
        dataShape = (self.imageWidth, self.imageHeight)
        bytesPerPoint = self.bytesPerValue * self.valuesPerPoint
        bytesPerColumn = self.imageHeight * bytesPerPoint

        if (self.isComplex):
            self.values = np.zeros(dataShape, dtype=np.complex_)
            for x in range(self.imageWidth):
                columnOffset = bytesPerColumn * x
                for y in range(self.imageHeight):
                    rowOffset = y * bytesPerPoint
                    valueOffset = self.firstDataByte + columnOffset + rowOffset
                    bytesReal = filebytes[valueOffset:valueOffset+8]
                    bytesImag = filebytes[valueOffset+8:valueOffset+8+8]
                    valueReal = struct.unpack("<d", bytesReal)[0]
                    valueImag = struct.unpack("<d", bytesImag)[0]
                    self.values[x, y] = valueReal + valueImag * 1j
        else:
            self.values = np.zeros(dataShape, dtype=np.float)
            for x in range(self.imageWidth):
                columnOffset = bytesPerColumn * x
                for y in range(self.imageHeight):
                    rowOffset = y * bytesPerPoint
                    valueOffset = self.firstDataByte + columnOffset + rowOffset
                    bytesMag = filebytes[valueOffset:valueOffset+8]
                    self.values[x, y] = struct.unpack("<d", bytesMag)[0]

        self.loadTimeMsec = (time.perf_counter() - timeStart)*1000

    def getDescription(self):
        d = ""
        d += f"SFF version: {self.versionMajor}.{self.versionMinor}"
        d += "\n"
        d += f"\nSample rate: {self.sampleRate} Hz"
        d += f"\nStep size: {self.stepSize} samples"
        d += f"\nStep count: {self.stepCount} steps"
        d += "\n"
        d += f"\nFFT size: {self.fftSize}"
        d += f"\nFFT first index: {self.fftFirstIndex}"
        d += f"\nFFT height: {self.fftHeight}"
        d += f"\nFFT offset: {self.offsetHz} Hz"
        d += "\n"
        d += f"\nValues per point: {self.valuesPerPoint}"
        d += f"\nComplex values: {self.isComplex}"
        d += f"\nBytes per point: {self.bytesPerValue}"
        d += f"\nDecibels: {self.decibels}"
        d += "\n"
        d += f"\nMel bin count: {self.melBinCount}"
        d += f"\nimage width: {self.imageWidth}"
        d += f"\nimage height: {self.imageHeight}"
        d += "\n"
        d += f"\nTime Resolution: {self.secPerPx} sec/px"
        d += f"\nFrequency Resolution: {self.hzPerPx} Hz/px"
        d += "\n"
        d += f"\nFirst data byte: {self.firstDataByte}"
        d += "\n"
        d += f"\nLoaded {os.path.basename(self.filePath)} " +\
            f"({self.valuesPerPoint * self.imageWidth * self.imageHeight:,} values) " +\
            f"in {self.loadTimeMsec:.02f} ms"
        d += "\n"
        return d
