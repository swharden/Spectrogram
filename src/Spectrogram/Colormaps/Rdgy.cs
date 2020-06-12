using System;

namespace Spectrogram.Colormaps
{
    class Rdgy : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the Rdgy colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            06750239, 06881311, 07078175, 07275040, 07471904, 07668769, 07865633, 08062498, 
            08259362, 08456227, 08653091, 08849956, 09046820, 09243685, 09440549, 09637414, 
            09834278, 10031143, 10162215, 10359079, 10555944, 10752808, 10949673, 11146537, 
            11343402, 11540266, 11671851, 11803693, 11869998, 11936047, 12067889, 12134194, 
            12266035, 12332340, 12464182, 12530231, 12596536, 12728378, 12794683, 12926524, 
            12992574, 13058879, 13190721, 13257026, 13388867, 13455172, 13521222, 13653063, 
            13719369, 13851210, 13917515, 14049357, 14115407, 14181713, 14248019, 14314069, 
            14380375, 14512217, 14578267, 14644573, 14710879, 14777185, 14843235, 14975077, 
            15041384, 15107434, 15173740, 15240046, 15371632, 15437938, 15504244, 15570550, 
            15636600, 15702906, 15834748, 15900798, 15967104, 16033411, 16033926, 16034440, 
            16100491, 16101006, 16101520, 16167571, 16168086, 16234136, 16234907, 16235422, 
            16301473, 16301987, 16302502, 16368553, 16369067, 16369582, 16435889, 16436404, 
            16436918, 16502969, 16503484, 16503998, 16570049, 16570564, 16636871, 16637129, 
            16637387, 16637901, 16638159, 16638673, 16638932, 16639190, 16639704, 16639962, 
            16640476, 16640735, 16640993, 16707043, 16707301, 16707815, 16708074, 16708588, 
            16708846, 16709104, 16709618, 16709877, 16710391, 16710649, 16710907, 16711421, 
            16711422, 16645629, 16514043, 16448250, 16382457, 16316664, 16250871, 16119285, 
            16053492, 15987699, 15921906, 15856113, 15724527, 15658734, 15592941, 15527148, 
            15395562, 15329769, 15263976, 15198183, 15132390, 15000804, 14935011, 14869218, 
            14803425, 14737632, 14606046, 14540253, 14408667, 14342874, 14211288, 14145495, 
            14013909, 13948116, 13816530, 13750737, 13619151, 13553358, 13421772, 13355979, 
            13224393, 13158600, 13027014, 12961221, 12829635, 12763842, 12632256, 12566463, 
            12434877, 12369084, 12237498, 12171705, 12040119, 11908533, 11776947, 11645361, 
            11513775, 11382189, 11250603, 11119017, 10987431, 10855845, 10724259, 10592673, 
            10461087, 10329501, 10197915, 10066329, 09934743, 09803157, 09671571, 09539985, 
            09408399, 09276813, 09145227, 09013641, 08882055, 08684676, 08553090, 08421504, 
            08224125, 08092539, 07960953, 07829367, 07631988, 07500402, 07368816, 07171437, 
            07039851, 06908265, 06776679, 06579300, 06447714, 06316128, 06184542, 05987163, 
            05855577, 05723991, 05526612, 05395026, 05263440, 05131854, 05000268, 04802889, 
            04737096, 04539717, 04473924, 04276545, 04210752, 04013373, 03947580, 03750201, 
            03684408, 03487029, 03421236, 03223857, 03158064, 03026478, 02894892, 02763306, 
            02631720, 02500134, 02368548, 02236962, 02105376, 01973790, 01842204, 01710618, 
        };
    }
}