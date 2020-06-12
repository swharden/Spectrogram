# Spectrogram
**Spectrogram** is a .NET library for creating spectrograms from pre-recorded signals or live audio from the sound card.  Spectrogram uses FFT algorithms and window functions provided by the [FftSharp](https://github.com/swharden/FftSharp) project, and it targets .NET Standard 2.0 so it can be used in .NET Framework and .NET Core projects.

<div align="center">

![](dev/spectrogram.png)

_"I'm sorry Dave... I'm afraid I can't do that"_

</div>


## Quickstart

_Spectrogram is [available on NuGet](https://www.nuget.org/packages/Spectrogram)_

```cs
double[] audio = Read.WavInt16mono("hal.wav");
int sampleRate = 44100;

var spec = new Spectrogram(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
spec.Add(audio);
spec.SaveImage("hal.png", intensity: .4);
```

This code generates the image displayed at the top of this page.

## Windows Forms

If you're using Spectrogram in a graphical application you may find it helpful to retrieve the output as a Bitmap which can be displayed on a Picturebox:

```cs
Bitmap bmp = spec.GetBitmap();
pictureBox1.Image = bmp;
```

I find it helpful to put the Picturebox inside a Panel with auto-scroll enabled, so large spectrograms which are bigger than the size of the window can be interactively displayed.

## Real-Time Spectrogram

An example program is included in this repository which demonstrates how to use [NAudio](https://github.com/naudio/NAudio) to get samples from the sound card and display them as a spectrogram. Spectrogram was designed to be able to display spectrograms with live or growing data, so this is exceptionally easy to implement.

![](dev/microphone-spectrogram.gif)

To do this, keep your Spectrogram at the class level:
```cs
Spectrogram spec;

public Form1()
{
    InitializeComponent();
    spec = new Spectrogram(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
}
```

Whenever an audio buffer gets filled, add the data to your Spectrogram:
```cs
private void GotNewBuffer(double[] audio)
{
    spec.Add(audio);
}
```

Then set up a timer to trigger rendering:
```cs
private void timer1_Tick(object sender, EventArgs e){
    Bitmap bmp = spec.GetBitmap(intensity: .4);
    pictureBox1.Image?.Dispose();
    pictureBox1.Image = bmp;
}
```

Review the source code of the demo application for additional details and considerations. You'll found I abstracted the audio interfacing code into its own class, isolating it from the GUI code.

## Song-to-Spectrogram

This example demonstrates how to convert a MP3 file to a spectrogram image. A sample MP3 audio file in the [data folder](data) contains the audio track from Ken Barker's excellent piano performance of George Frideric Handel's Suite No. 5 in E major for harpsichord ([_The Harmonious Blacksmith_](https://en.wikipedia.org/wiki/The_Harmonious_Blacksmith)). This audio file is included [with permission](dev/Handel%20-%20Air%20and%20Variations.txt), and the [original video can be viewed on YouTube](https://www.youtube.com/watch?v=Mza-xqk770k).

```cs
double[] audio = Read.WavInt16mono("Handel.wav");
int sampleRate = 44100;

var spec = new Spectrogram(sampleRate, fftSize: 16384, stepSize: 2500, maxFreq: 2200);
spec.Add(audio);
spec.SaveImage("spectrogram-song.jpg", intensity: 5, dB: true);
```

Notice the optional conversion to Decibels while saving the image.

![](dev/spectrogram-song.jpg)

If you [listen to the audio track](https://www.youtube.com/watch?v=Mza-xqk770k) while closely inspecting the spectrogram you can identify individual piano notes and chords, and may be surprised by the interesting patterns that emerge around trills and glissandos.

## Spectrogram Information

The Spectrogram's `ToString()` method displays detailed information about the spectrogram:

```cs
Console.WriteLine(spec);
```

```
Spectrogram (2993, 817)
  Vertical (817 px): 0 - 2,199 Hz, FFT size: 16,384 samples, 2.69 Hz/px
  Horizontal (2993 px): 2.96 min, window: 0.37 sec, step: 0.06 sec, overlap: 84%
```

## Colormaps

These examples demonstrate the identical spectrogram analyzed with a variety of different colormaps. Spectrogram colormaps can be changed by calling the `SetColormap()` method:

```cs
double[] audio = Read.WavInt16mono("hal.wav");
int sampleRate = 44100;
int fftSize = 8192;
var spec = new Spectrogram(sampleRate, fftSize, stepSize: 200, maxFreq: 3000);
spec.Add(audio);
spec.SetColormap(Colormap.Jet);
spec.SaveImage($"hal-Jet.png", intensity: .5);
```

Colormap Name | Color Curves | Example Spectrogram
---|---|---
Argo | ![](dev/colormap/analyzed2/argo.png) | ![](dev/graphics/hal-Argo.png)
Blues | ![](dev/colormap/analyzed2/blues.png) | ![](dev/graphics/hal-Blues.png)
Cividis | ![](dev/colormap/analyzed2/cividis.png) | ![](dev/graphics/hal-Cividis.png)
Gray | ![](dev/colormap/analyzed2/gray.png) | ![](dev/graphics/hal-Grayscale.png)
GrayReversed | ![](dev/colormap/analyzed2/gray_r.png) | ![](dev/graphics/hal-GrayscaleReversed.png)
Greens | ![](dev/colormap/analyzed2/greens.png) | ![](dev/graphics/hal-Greens.png)
Inferno | ![](dev/colormap/analyzed2/inferno.png) | ![](dev/graphics/hal-Inferno.png)
Jet | ![](dev/colormap/analyzed2/jet.png) | ![](dev/graphics/hal-Jet.png)
Magma | ![](dev/colormap/analyzed2/magma.png) | ![](dev/graphics/hal-Magma.png)
Plasma | ![](dev/colormap/analyzed2/plasma.png) | ![](dev/graphics/hal-Plasma.png)
Viridis | ![](dev/colormap/analyzed2/viridis.png) | ![](dev/graphics/hal-Viridis.png)

## All Colormaps
![](dev/graphics/hal-Afmhot.png)
![](dev/graphics/hal-AfmhotReversed.png)
![](dev/graphics/hal-Argo.png)
![](dev/graphics/hal-Autumn.png)
![](dev/graphics/hal-AutumnReversed.png)
![](dev/graphics/hal-Binary.png)
![](dev/graphics/hal-BinaryReversed.png)
![](dev/graphics/hal-Blues.png)
![](dev/graphics/hal-BluesReversed.png)
![](dev/graphics/hal-Bone.png)
![](dev/graphics/hal-BoneReversed.png)
![](dev/graphics/hal-Brbg.png)
![](dev/graphics/hal-BrbgReversed.png)
![](dev/graphics/hal-Brg.png)
![](dev/graphics/hal-BrgReversed.png)
![](dev/graphics/hal-Bugn.png)
![](dev/graphics/hal-BugnReversed.png)
![](dev/graphics/hal-Bupu.png)
![](dev/graphics/hal-BupuReversed.png)
![](dev/graphics/hal-Bwr.png)
![](dev/graphics/hal-BwrReversed.png)
![](dev/graphics/hal-Cividis.png)
![](dev/graphics/hal-CividisReversed.png)
![](dev/graphics/hal-Cmrmap.png)
![](dev/graphics/hal-CmrmapReversed.png)
![](dev/graphics/hal-Cool.png)
![](dev/graphics/hal-CoolReversed.png)
![](dev/graphics/hal-Coolwarm.png)
![](dev/graphics/hal-CoolwarmReversed.png)
![](dev/graphics/hal-Copper.png)
![](dev/graphics/hal-CopperReversed.png)
![](dev/graphics/hal-Cubehelix.png)
![](dev/graphics/hal-CubehelixReversed.png)
![](dev/graphics/hal-Flag.png)
![](dev/graphics/hal-FlagReversed.png)
![](dev/graphics/hal-GistReversedainbow.png)
![](dev/graphics/hal-GistReversedainbowReversed.png)
![](dev/graphics/hal-Gnbu.png)
![](dev/graphics/hal-GnbuReversed.png)
![](dev/graphics/hal-Gnuplot.png)
![](dev/graphics/hal-Gnuplot2.png)
![](dev/graphics/hal-Gnuplot2Reversed.png)
![](dev/graphics/hal-GnuplotReversed.png)
![](dev/graphics/hal-Gray.png)
![](dev/graphics/hal-GrayReversed.png)
![](dev/graphics/hal-Greens.png)
![](dev/graphics/hal-GreensReversed.png)
![](dev/graphics/hal-Greys.png)
![](dev/graphics/hal-GreysReversed.png)
![](dev/graphics/hal-Hot.png)
![](dev/graphics/hal-HotReversed.png)
![](dev/graphics/hal-Hsv.png)
![](dev/graphics/hal-HsvReversed.png)
![](dev/graphics/hal-Inferno.png)
![](dev/graphics/hal-InfernoReversed.png)
![](dev/graphics/hal-Jet.png)
![](dev/graphics/hal-JetReversed.png)
![](dev/graphics/hal-Magma.png)
![](dev/graphics/hal-MagmaReversed.png)
![](dev/graphics/hal-Ocean.png)
![](dev/graphics/hal-OceanReversed.png)
![](dev/graphics/hal-Oranges.png)
![](dev/graphics/hal-OrangesReversed.png)
![](dev/graphics/hal-Orrd.png)
![](dev/graphics/hal-OrrdReversed.png)
![](dev/graphics/hal-Pink.png)
![](dev/graphics/hal-PinkReversed.png)
![](dev/graphics/hal-Piyg.png)
![](dev/graphics/hal-PiygReversed.png)
![](dev/graphics/hal-Plasma.png)
![](dev/graphics/hal-PlasmaReversed.png)
![](dev/graphics/hal-Prgn.png)
![](dev/graphics/hal-PrgnReversed.png)
![](dev/graphics/hal-PrismReversed.png)
![](dev/graphics/hal-Pubu.png)
![](dev/graphics/hal-Pubugn.png)
![](dev/graphics/hal-PubugnReversed.png)
![](dev/graphics/hal-PubuReversed.png)
![](dev/graphics/hal-Puor.png)
![](dev/graphics/hal-PuorReversed.png)
![](dev/graphics/hal-Purd.png)
![](dev/graphics/hal-PurdReversed.png)
![](dev/graphics/hal-Purples.png)
![](dev/graphics/hal-PurplesReversed.png)
![](dev/graphics/hal-Rainbow.png)
![](dev/graphics/hal-RainbowReversed.png)
![](dev/graphics/hal-Rdbu.png)
![](dev/graphics/hal-RdbuReversed.png)
![](dev/graphics/hal-Rdgy.png)
![](dev/graphics/hal-RdgyReversed.png)
![](dev/graphics/hal-Rdpu.png)
![](dev/graphics/hal-RdpuReversed.png)
![](dev/graphics/hal-Rdylbu.png)
![](dev/graphics/hal-RdylbuReversed.png)
![](dev/graphics/hal-Rdylgn.png)
![](dev/graphics/hal-RdylgnReversed.png)
![](dev/graphics/hal-Reds.png)
![](dev/graphics/hal-RedsReversed.png)
![](dev/graphics/hal-Seismic.png)
![](dev/graphics/hal-SeismicReversed.png)
![](dev/graphics/hal-Spectral.png)
![](dev/graphics/hal-SpectralReversed.png)
![](dev/graphics/hal-Spring.png)
![](dev/graphics/hal-SpringReversed.png)
![](dev/graphics/hal-Summer.png)
![](dev/graphics/hal-SummerReversed.png)
![](dev/graphics/hal-Terrain.png)
![](dev/graphics/hal-TerrainReversed.png)
![](dev/graphics/hal-Twilight.png)
![](dev/graphics/hal-TwilightReversed.png)
![](dev/graphics/hal-Viridis.png)
![](dev/graphics/hal-ViridisReversed.png)
![](dev/graphics/hal-Winter.png)
![](dev/graphics/hal-WinterReversed.png)
![](dev/graphics/hal-Wistia.png)
![](dev/graphics/hal-WistiaReversed.png)
![](dev/graphics/hal-Ylgn.png)
![](dev/graphics/hal-Ylgnbu.png)
![](dev/graphics/hal-YlgnbuReversed.png)
![](dev/graphics/hal-YlgnReversed.png)
![](dev/graphics/hal-Ylorbr.png)
![](dev/graphics/hal-YlorbrReversed.png)
![](dev/graphics/hal-Ylorrd.png)
![](dev/graphics/hal-YlorrdReversed.png)

## Resources
* [FftSharp](https://github.com/swharden/FftSharp) - the module which actually performs the FFT and related transformations
* [MP3Sharp](https://github.com/ZaneDubya/MP3Sharp) - a library I use to read MP3 files during testing
* [FSKview](https://github.com/swharden/FSKview) - a real-time spectrogram for viewing frequency-shift-keyed (FSK) signals from audio transmitted over radio frequency.
* [NAudio](https://github.com/naudio/NAudio) - an open source .NET library which makes it easy to get samples from the microphone or sound card in real time