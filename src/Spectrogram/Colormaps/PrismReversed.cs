using System;

namespace Spectrogram.Colormaps
{
    class PrismReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the PrismReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            05570048, 08584960, 11796224, 14941952, 16774656, 16766720, 16756480, 16744704, 
            16732160, 16719872, 16711680, 16711681, 16711752, 14483595, 11272388, 08061169, 
            05046527, 02490623, 00459007, 00007670, 00019916, 00032405, 00109652, 01954829, 
            04387840, 07339776, 10485504, 13696768, 16711424, 16770048, 16760832, 16749568, 
            16737280, 16724736, 16712704, 16711680, 16711724, 15663217, 12583087, 09371873, 
            06226175, 03473663, 01179903, 00002815, 00014815, 00027565, 00039535, 01099050, 
            03336448, 06094592, 09174784, 12386048, 15531776, 16773120, 16764672, 16754176, 
            16742400, 16729856, 16717312, 16711680, 16711695, 16711766, 13828247, 10617038, 
            07471352, 04522239, 02031871, 00131327, 00009967, 00022467, 00034697, 00374086, 
            02415360, 04978944, 07929600, 11140864, 14286592, 16775936, 16768512, 16758784, 
            16747264, 16734720, 16722176, 16711680, 16711680, 16711738, 15073406, 11927737, 
            08716521, 05636351, 02949375, 00852223, 00005117, 00017366, 00029857, 00041826, 
            01494299, 03862016, 06684416, 09830144, 13041408, 16121600, 16771840, 16762880, 
            16751872, 16739840, 16727296, 16715008, 16711680, 16711709, 16318563, 13238435, 
            10027223, 06881534, 03997951, 01638655, 00000511, 00012264, 00024760, 00037244, 
            00769336, 02875904, 05504512, 08519424, 11730688, 14941952, 16774656, 16766720, 
            16756480, 16744704, 16732416, 16719872, 16711680, 16711680, 16711752, 14483594, 
            11272388, 08126704, 05112063, 02490623, 00459007, 00007415, 00019917, 00032406, 
            00109652, 01954829, 04387840, 07274240, 10419968, 13631232, 16711424, 16770304, 
            16760832, 16749568, 16737280, 16724736, 16712704, 16711680, 16711723, 15728752, 
            12583086, 09371872, 06291711, 03473663, 01245439, 00002815, 00014816, 00027309, 
            00039535, 01099050, 03336192, 06094592, 09174784, 12386048, 15531776, 16773376, 
            16764928, 16754432, 16742400, 16729856, 16717568, 16711680, 16711694, 16711765, 
            13893782, 10682574, 07471351, 04587775, 02097407, 00196863, 00009968, 00022211, 
            00034697, 00373831, 02349824, 04913408, 07864064, 11075328, 14286592, 16775936, 
            16768512, 16758784, 16747264, 16734976, 16722432, 16711680, 16711680, 16711737, 
            15138941, 11927737, 08716520, 05701887, 03014911, 00852223, 00005117, 00017367, 
            00029858, 00041826, 01494044, 03862016, 06684416, 09764608, 12975872, 16121600, 
            16771840, 16762880, 16752128, 16740096, 16727296, 16715264, 16711680, 16711708, 
            16318563, 13238434, 10027223, 06881534, 03997951, 01638655, 00000255, 00012264, 
            00024761, 00036989, 00703545, 02810368, 05504512, 08519424, 11730688, 14876416, 
            16774656, 16766720, 16756736, 16744960, 16732416, 16720128, 16711680, 16711680, 
        };
    }
}