# The Spectrogram File Format (SFF)

The SFF header isn't fully documented yet, but it's easy to figure out by looking at the source code of the SFF module.

Briefly, key values are stored in fixed memory positions. Notable points are:
* The first 4 bytes, interpreted as an Int32, equal the magic number `1179014099`
* The UInt32 at byte `80` indicates the byte position of the first data point
* `Width` and `Height` represent dimensions of the original Spectrogram, but if Mel conversion has been applied `FftSize` represents the height of the Mel-transformed spectrogram

## Resources
* [Designing File Formats](https://fadden.com/tech/file-formats.html)
* [A brief look at file format design](http://decoy.iki.fi/texts/filefd/filefd)
* [Standard Flowgram Format (SFF)](https://www.ncbi.nlm.nih.gov/Traces/trace.cgi?cmd=show&f=formats&m=doc&s=format#sff)