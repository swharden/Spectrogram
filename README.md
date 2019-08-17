# Spectrogram
**Spectrogram** is a .NET library which makes it easy to create spectrograms from pre-recorded signals or live audio from the sound card. This library supports .NET Framework (4.5) and .NET Core (3.0).

![](data/mozart.jpg)

**Quickstart:** This example program converts [/data/mozart.wav](/data/mozart.wav) (Mozart's Piano Sonata No. 11 in A major) to a spectrograph (above) and saves it as an image file.

```cs
var spec = new Spectrogram.Spectrogram();
float[] values = Spectrogram.WavFile.Read("mozart.wav");
spec.Add(values);
spec.SaveBitmap("mozart.png");
```

## Realtime Audio Monitor

A demo program is included which monitors the sound card and continuously creates spectrograms from microphone input.

![](data/screenshot3.gif)

## TODO:
* render horizontally or vertically
* optional display of axis labels (scales)
* create bitmaps in real time from audio input
* advanced color (LUT) options
* advanced intensity options (nonlinear scaling)
* create a user control to display a spectrogram
* create a user control to adjust spectrogram settings

## Resources
* [microphone spectrograph in C#](https://github.com/swharden/Csharp-Data-Visualization/tree/master/projects/18-01-11_microphone_spectrograph)
* [QRSS Spectrograph in C#](https://github.com/swharden/Csharp-Data-Visualization/tree/master/projects/18-01-14_qrss)
* [Simulation of QRSS Signals](https://www.qsl.net/pa2ohh/12qrsssim1.htm)

### QRSS

#### Introduction
  * [What is QRSS?](https://www.qsl.net/m0ayf/What-is-QRSS.html)
  * [QRSS and you](http://www.ka7oei.com/qrss1.html)
  * [QRSS (slow CW)](https://sites.google.com/site/qrssinfo/QRSS-Slow-CW)

#### Software
* Argo ([website](http://digilander.libero.it/i2phd/argo/)) - closed-source QRSS viewer for Windows
* SpectrumLab ([website](http://www.qsl.net/dl4yhf/spectra1.html)) - closed-source spectrum analyzer for Windows 
* QrssPIG ([gitlab](https://gitlab.com/hb9fxx/qrsspig)) - open-source spectrograph for Raspberry Pi (C++)
* Lopora ([website](http://www.qsl.net/pa2ohh/11lop.htm)) - open-source spectrograph (Python 3) 
* QRSS VD ([github](https://github.com/swharden/QRSS-VD)) - open source spectrograph (Python 2)

### Spectrogram vs ~~Spectrograph~~
* A spectrogram is an image
* A spectrograph is a machine
* Stop using the word spectrograph in software!
