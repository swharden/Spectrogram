namespace Spectrogram.Colormaps
{
    public interface IColormap
    {
        string GetName();
        void Apply(System.Drawing.Bitmap bmp);
    }
}
