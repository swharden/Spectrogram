using System;

namespace Spectrogram.Colormaps
{
    class Wistia : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the Wistia colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            15007610, 15007352, 15007350, 15072629, 15072627, 15138162, 15137904, 15137903, 
            15203437, 15203180, 15268714, 15268713, 15333991, 15333990, 15333732, 15399267, 
            15399265, 15464544, 15464542, 15530077, 15529819, 15529818, 15595352, 15595095, 
            15660629, 15660372, 15725906, 15725905, 15725647, 15791182, 15791180, 15856459, 
            15856457, 15856456, 15921734, 15921733, 15987267, 15987010, 16052544, 16052287, 
            16052285, 16117820, 16117562, 16183097, 16183095, 16248374, 16248372, 16248371, 
            16313649, 16313648, 16378926, 16378925, 16444459, 16444202, 16444200, 16509735, 
            16509477, 16575012, 16575010, 16574753, 16640287, 16640030, 16705564, 16705563, 
            16770841, 16770841, 16770585, 16770328, 16770328, 16770071, 16769815, 16769815, 
            16769558, 16769302, 16769301, 16769045, 16768789, 16768788, 16768532, 16768275, 
            16768275, 16768018, 16767762, 16767762, 16767505, 16767249, 16766992, 16766992, 
            16766736, 16766479, 16766479, 16766222, 16765966, 16765966, 16765709, 16765453, 
            16765452, 16765196, 16764940, 16764939, 16764683, 16764426, 16764426, 16764169, 
            16763913, 16763913, 16763656, 16763400, 16763399, 16763143, 16762887, 16762886, 
            16762630, 16762373, 16762373, 16762117, 16761860, 16761860, 16761603, 16761347, 
            16761347, 16761090, 16760834, 16760833, 16760577, 16760321, 16760320, 16760064, 
            16759808, 16759808, 16759552, 16759552, 16759296, 16759296, 16759296, 16759040, 
            16759040, 16758784, 16758784, 16758528, 16758528, 16758272, 16758272, 16758016, 
            16758016, 16758016, 16757760, 16757760, 16757504, 16757504, 16757248, 16757248, 
            16756992, 16756992, 16756736, 16756736, 16756736, 16756480, 16756480, 16756224, 
            16756224, 16755968, 16755968, 16755712, 16755712, 16755456, 16755456, 16755456, 
            16755200, 16755200, 16754944, 16754944, 16754688, 16754688, 16754432, 16754432, 
            16754176, 16754176, 16754176, 16753920, 16753920, 16753664, 16753664, 16753408, 
            16753408, 16753152, 16753152, 16752896, 16752896, 16752896, 16752640, 16752640, 
            16686848, 16686848, 16686592, 16686592, 16686336, 16686336, 16686080, 16685824, 
            16685824, 16685568, 16685568, 16685312, 16685312, 16685056, 16685056, 16684800, 
            16684800, 16684544, 16684544, 16684288, 16684288, 16618496, 16618496, 16618240, 
            16618240, 16617984, 16617984, 16617728, 16617728, 16617472, 16617472, 16617216, 
            16617216, 16616960, 16616960, 16616704, 16616448, 16616448, 16616192, 16616192, 
            16615936, 16615936, 16550144, 16550144, 16549888, 16549888, 16549632, 16549632, 
            16549376, 16549376, 16549120, 16549120, 16548864, 16548864, 16548608, 16548608, 
            16548352, 16548352, 16548096, 16548096, 16547840, 16547840, 16547584, 16547584, 
        };
    }
}