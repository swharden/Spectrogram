using System;

namespace Spectrogram.Colormaps
{
    class Cubehelix : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the Cubehelix colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            00000000, 00065537, 00196867, 00262404, 00393734, 00524808, 00590601, 00656395, 
            00787469, 00853263, 00919057, 00984595, 01115925, 01181719, 01247513, 01313307, 
            01313565, 01379103, 01444897, 01510691, 01510949, 01576743, 01577257, 01643051, 
            01643309, 01643567, 01709360, 01709618, 01710132, 01710390, 01710648, 01711161, 
            01711419, 01711932, 01712190, 01712703, 01712961, 01647938, 01648195, 01648709, 
            01648966, 01583943, 01584456, 01584713, 01519690, 01520202, 01520715, 01520972, 
            01455948, 01456461, 01456717, 01391693, 01392206, 01392718, 01393230, 01393486, 
            01393998, 01328974, 01329486, 01329741, 01395789, 01396301, 01396556, 01397068, 
            01397579, 01463371, 01463882, 01464393, 01530185, 01530696, 01596487, 01662534, 
            01728325, 01794372, 01794627, 01860418, 01992002, 02057793, 02123584, 02189375, 
            02320957, 02386748, 02518075, 02583866, 02715194, 02846521, 02977848, 03109175, 
            03240502, 03371573, 03502900, 03634227, 03765299, 03962162, 04093489, 04290097, 
            04421424, 04618032, 04749359, 04945967, 05142575, 05339438, 05470510, 05667118, 
            05863726, 06060590, 06257199, 06453807, 06650415, 06847024, 07043632, 07240241, 
            07436850, 07633458, 07895603, 08092212, 08288821, 08485431, 08682040, 08878649, 
            09075003, 09271612, 09468222, 09664832, 09861441, 10058051, 10189125, 10385735, 
            10582346, 10778700, 10909774, 11106385, 11237459, 11434070, 11565144, 11696219, 
            11892830, 12023904, 12154979, 12286310, 12417385, 12548460, 12679535, 12745074, 
            12876405, 13007480, 13073020, 13204351, 13269890, 13335685, 13401224, 13467020, 
            13532559, 13598354, 13664149, 13729689, 13729948, 13795743, 13861538, 13861797, 
            13861801, 13927596, 13927855, 13928370, 13928629, 13928888, 13929146, 13929405, 
            13929664, 13864643, 13864901, 13865160, 13800139, 13800397, 13800911, 13735634, 
            13670612, 13671126, 13605848, 13606362, 13541084, 13476062, 13476576, 13411554, 
            13346275, 13346789, 13281766, 13216743, 13217001, 13151978, 13086955, 13087468, 
            13022445, 12957422, 12957679, 12892655, 12893168, 12828145, 12828657, 12763634, 
            12763890, 12764402, 12699379, 12699891, 12700403, 12700659, 12701171, 12701683, 
            12701939, 12702451, 12702963, 12703219, 12769266, 12769522, 12835570, 12835826, 
            12901873, 12902129, 12968177, 13033969, 13099760, 13165808, 13166064, 13297391, 
            13363183, 13428975, 13494767, 13560815, 13692142, 13757934, 13823726, 13955054, 
            14020846, 14151918, 14283246, 14349038, 14480367, 14611695, 14677231, 14808560, 
            14939888, 15071216, 15202289, 15268082, 15399154, 15530483, 15661812, 15792885, 
            15924214, 16055287, 16121080, 16252153, 16383482, 16514556, 16645885, 16777215, 
        };
    }
}