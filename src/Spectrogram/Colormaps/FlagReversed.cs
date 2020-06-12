using System;

namespace Spectrogram.Colormaps
{
    class FlagReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the FlagReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            00000000, 00000053, 00000125, 00000198, 00000255, 03300607, 08040447, 12773631, 
            16776959, 16773071, 16759944, 16739647, 16715264, 13828096, 09109504, 04325376, 
            00131072, 00000040, 00000111, 00000184, 00000249, 02444031, 07119359, 11919359, 
            16186879, 16774620, 16763030, 16744012, 16719882, 14614528, 10027008, 05242880, 
            00851968, 00000028, 00000097, 00000171, 00000238, 01652991, 06198015, 10999551, 
            15465215, 16775656, 16765860, 16748122, 16724758, 15400960, 10944512, 06160384, 
            01638400, 00000016, 00000083, 00000157, 00000226, 00861695, 05276415, 10079231, 
            14677759, 16776436, 16768434, 16751976, 16729378, 16121856, 11862016, 07077888, 
            02424832, 00000005, 00000070, 00000143, 00000214, 00135935, 04354559, 09158655, 
            13890047, 16776959, 16770751, 16755574, 16733999, 16711680, 12713984, 07995392, 
            03276800, 00000000, 00000056, 00000129, 00000201, 00000255, 03498239, 08237823, 
            13036287, 16776959, 16772556, 16759172, 16738620, 16713984, 13565952, 08912896, 
            04128768, 00000000, 00000043, 00000115, 00000188, 00000252, 02641663, 07316735, 
            12116735, 16383487, 16774105, 16762258, 16742985, 16718856, 14417920, 09830400, 
            04980736, 00655360, 00000031, 00000101, 00000174, 00000241, 01850623, 06395391, 
            11262207, 15662079, 16775397, 16765344, 16747095, 16723475, 15204352, 10747904, 
            05898240, 01441792, 00000019, 00000087, 00000160, 00000229, 01059583, 05474047, 
            10342143, 14874623, 16776433, 16767918, 16750949, 16728351, 15990784, 11665408, 
            06815744, 02228224, 00000008, 00000073, 00000146, 00000217, 00333823, 04617727, 
            09421567, 14086911, 16776956, 16770236, 16754803, 16732971, 16711680, 12517376, 
            07733248, 03080192, 00000000, 00000060, 00000132, 00000204, 00001279, 03695871, 
            08500735, 13233407, 16777215, 16772297, 16758145, 16737336, 16712704, 13369344, 
            08650752, 03932160, 00000000, 00000047, 00000118, 00000191, 00000255, 02839551, 
            07579903, 12379391, 16580351, 16773846, 16761487, 16741702, 16717573, 14221312, 
            09568256, 04784128, 00524288, 00000034, 00000104, 00000178, 00000244, 02048511, 
            06658559, 11459583, 15858943, 16775138, 16764573, 16746067, 16722448, 15007744, 
            10485760, 05701632, 01245184, 00000022, 00000090, 00000164, 00000232, 01257215, 
            05737215, 10539519, 15071487, 16776174, 16767147, 16749921, 16727068, 15794176, 
            11403264, 06619136, 02031616, 00000010, 00000076, 00000150, 00000220, 00531711, 
            04815615, 09618943, 14283775, 16776697, 16769720, 16753775, 16731688, 16515072, 
            12320768, 07536640, 02818048, 00000000, 00000063, 00000136, 00000207, 00002559, 
            03959295, 08698367, 13430271, 16776959, 16771782, 16757373, 16736309, 16711680, 
        };
    }
}