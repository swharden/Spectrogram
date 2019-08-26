**Spectrogram** is a .NET library which makes it easy to create spectrograms from pre-recorded signals or live audio from the sound card.

### Quickstart

```cs
// load audio and process FFT
var spec = new Spectrogram.Spectrogram(sampleRate: 8000, fftSize: 2048, step: 700);
float[] values = Spectrogram.Tools.ReadWav("mozart.wav");
spec.AddExtend(values);

// convert FFT to an image and save it
Bitmap bmp = spec.GetBitmap(intensity: 2, freqHigh: 2500);
spec.SaveBitmap(bmp, "mozart.jpg");
```

![](https://raw.githubusercontent.com/swharden/Spectrogram/master/data/mozartSmall.jpg)

### Additional Resources
Much more is on the Spectrogram project page:\
**[https://github.com/swharden/Spectrogram](https://github.com/swharden/Spectrogram)**