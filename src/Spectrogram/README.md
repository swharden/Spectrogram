**Spectrogram is a .NET library for creating frequency spectrograms from pre-recorded signals, streaming data, or microphone audio from the sound card.**  Spectrogram uses FFT algorithms and window functions provided by the [FftSharp](https://github.com/swharden/FftSharp) project, and it targets .NET Standard so it can be used in .NET Framework and .NET Core projects.

[![](https://raw.githubusercontent.com/swharden/Spectrogram/master/dev/graphics/hal-spectrogram.png)](https://github.com/swharden/Spectrogram)

## Quickstart

```cs
(double[] audio, int sampleRate) = ReadWavMono("hal.wav");
var sg = new SpectrogramGenerator(sampleRate, fftSize: 4096, stepSize: 500, maxFreq: 3000);
sg.Add(audio);
sg.SaveImage("hal.png");
```

This example generates the image at the top of the page.


## How to Read a WAV File

There are many excellent libraries that read audio files. Consult the documentation _for those libraries_ to learn how to do this well. Here's an example method I use to read audio values from mono WAV files using the NAudio package:

```cs
(double[] audio, int sampleRate) ReadWavMono(string filePath, double multiplier = 16_000)
{
    using var afr = new NAudio.Wave.AudioFileReader(filePath);
    int sampleRate = afr.WaveFormat.SampleRate;
    int bytesPerSample = afr.WaveFormat.BitsPerSample / 8;
    int sampleCount = (int)(afr.Length / bytesPerSample);
    int channelCount = afr.WaveFormat.Channels;
    var audio = new List<double>(sampleCount);
    var buffer = new float[sampleRate * channelCount];
    int samplesRead = 0;
    while ((samplesRead = afr.Read(buffer, 0, buffer.Length)) > 0)
        audio.AddRange(buffer.Take(samplesRead).Select(x => x * multiplier));
    return (audio.ToArray(), sampleRate);
}
```