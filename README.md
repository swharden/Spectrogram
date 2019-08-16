# Spectrogram
.NET library for creating spectrograms

Spectrogram is written in C# (.NET Core 3.0) but compiled for net45 too.

https://www.nuget.org/packages/Spectrogram/

**WARNING: This software project is pre-alpha! This repo is just a place to collect ideas and resources.**

## Feature Ideas
* ability to display vertical (waterfall) or horizontal
* optional display of scalebars
* ability to suck in a WAV and output a bitmap
* ability to create bitmaps in realtime from audio data
* advanced color (LUT) and data scaling options (e.g., nonlinear brightness transformation)

## Primary Classes
* `Spectrogram` - the core class to turn time-series data (`double[]`) into a spectrogram (`Bitmap`)
* `AudioSpectrogram` - an extended `Spectrogram` which continuously gets incoming data from the sound card
* `FormsSpectrogram` - a WinForms user control to display a `Spectrogram`
* `WpfSpectrogram` - a WPF user control to display a `Spectrogram`

## Resources
* [microphone spectrograph in C#](https://github.com/swharden/Csharp-Data-Visualization/tree/master/projects/18-01-11_microphone_spectrograph)
* [QRSS Spectrograph in C#](https://github.com/swharden/Csharp-Data-Visualization/tree/master/projects/18-01-14_qrss)

### Introduction to QRSS
  * [What is QRSS?](https://www.qsl.net/m0ayf/What-is-QRSS.html)
  * [QRSS and you](http://www.ka7oei.com/qrss1.html)
  * [QRSS (slow CW)](https://sites.google.com/site/qrssinfo/QRSS-Slow-CW)

### Technical Pages
  * [Simulation of QRSS Signals](https://www.qsl.net/pa2ohh/12qrsssim1.htm)

### QRSS Software
* Argo ([website](http://digilander.libero.it/i2phd/argo/)) - closed-source QRSS viewer for Windows
* SpectrumLab ([website](http://www.qsl.net/dl4yhf/spectra1.html)) - closed-source spectrum analyzer for Windows 
* QrssPIG ([gitlab](https://gitlab.com/hb9fxx/qrsspig)) - open-source spectrograph for Raspberry Pi (C++)
* Lopora ([website](http://www.qsl.net/pa2ohh/11lop.htm)) - open-source spectrograph (Python 3) 
* QRSS VD ([github](https://github.com/swharden/QRSS-VD)) - open source spectrograph (Python 2)
