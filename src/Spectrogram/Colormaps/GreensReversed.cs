using System;

namespace Spectrogram.Colormaps
{
    class GreensReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the GreensReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            00017435, 00017691, 00017948, 00018204, 00018717, 00018973, 00019230, 00019742, 
            00019999, 00020255, 00020512, 00021024, 00021281, 00021537, 00022050, 00022307, 
            00022563, 00022820, 00023332, 00023589, 00023845, 00024358, 00024614, 00024871, 
            00025127, 00025640, 00025896, 00026153, 00026665, 00026922, 00027179, 00027435, 
            00027948, 00093740, 00159277, 00225070, 00290863, 00356656, 00422448, 00488241, 
            00554034, 00685363, 00751155, 00816948, 00882741, 00948534, 01014327, 01080119, 
            01145912, 01211705, 01277498, 01408570, 01474363, 01540156, 01605949, 01671742, 
            01737534, 01803327, 01869120, 01934913, 02000706, 02066498, 02197827, 02263620, 
            02329413, 02395205, 02460998, 02526791, 02592584, 02592840, 02658633, 02724426, 
            02790219, 02856011, 02921804, 02987597, 03053390, 03119182, 03184975, 03250768, 
            03316561, 03382353, 03448146, 03513939, 03579732, 03579989, 03645781, 03711574, 
            03777367, 03843160, 03908952, 03974745, 04040538, 04106331, 04172123, 04237916, 
            04303709, 04435038, 04500574, 04631903, 04763232, 04829025, 04960353, 05025890, 
            05157219, 05288548, 05354341, 05485413, 05551206, 05682535, 05813864, 05879657, 
            06010729, 06076522, 06207851, 06339180, 06404716, 06536045, 06601838, 06733167, 
            06864496, 06930032, 07061361, 07127154, 07258483, 07389812, 07455348, 07586677, 
            07652470, 07783543, 07849336, 07915130, 08046203, 08111996, 08243325, 08308862, 
            08440191, 08505985, 08571522, 08702851, 08768644, 08899717, 08965510, 09031303, 
            09162377, 09228170, 09359499, 09425036, 09490829, 09622158, 09687696, 09819025, 
            09884818, 10015891, 10081684, 10147477, 10278551, 10344344, 10475673, 10541210, 
            10607003, 10672540, 10803870, 10869407, 10935200, 11000737, 11066530, 11197603, 
            11263397, 11328934, 11394727, 11460264, 11591593, 11657130, 11722923, 11788461, 
            11854254, 11985327, 12051120, 12116657, 12182450, 12247988, 12313781, 12444854, 
            12510647, 12576184, 12641977, 12707515, 12838844, 12904381, 12970174, 13035711, 
            13101504, 13167041, 13232834, 13298371, 13363908, 13429701, 13495238, 13560775, 
            13626568, 13692105, 13757898, 13823435, 13888972, 13889229, 13954766, 14020303, 
            14086096, 14151633, 14217426, 14282963, 14348500, 14414293, 14479830, 14545367, 
            14611160, 14676697, 14742490, 14808027, 14873564, 14939357, 14939358, 15004895, 
            15070688, 15136225, 15136225, 15201762, 15202019, 15267555, 15267556, 15333093, 
            15398629, 15398886, 15464423, 15464423, 15529960, 15530217, 15595753, 15595754, 
            15661291, 15726827, 15727084, 15792621, 15792621, 15858158, 15858415, 15923951, 
            15989488, 15989489, 16055025, 16055282, 16120819, 16120819, 16186356, 16252149, 
        };
    }
}