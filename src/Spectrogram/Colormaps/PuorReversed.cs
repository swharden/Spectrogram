using System;

namespace Spectrogram.Colormaps
{
    class PuorReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the PuorReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            02949195, 03014989, 03146575, 03212370, 03343956, 03409750, 03541337, 03607131, 
            03738718, 03804512, 03936098, 04001893, 04133479, 04199274, 04330860, 04396654, 
            04528241, 04659827, 04725622, 04857208, 04923002, 05054589, 05120383, 05251970, 
            05317764, 05449350, 05515400, 05647242, 05779083, 05910924, 05977230, 06109071, 
            06240913, 06307218, 06439060, 06570901, 06702742, 06769048, 06900889, 07032731, 
            07164572, 07230877, 07362719, 07494560, 07560866, 07692707, 07824548, 07956390, 
            08022695, 08154537, 08286378, 08418220, 08484269, 08615854, 08747440, 08879025, 
            09010611, 09142452, 09274038, 09405623, 09537209, 09668794, 09800636, 09932221, 
            10063807, 10195392, 10326978, 10458819, 10590405, 10721990, 10853576, 10985161, 
            11117003, 11248588, 11380174, 11511759, 11643345, 11709394, 11840979, 11907028, 
            12038613, 12104662, 12236247, 12302040, 12433625, 12499674, 12631259, 12697308, 
            12828893, 12894942, 13026271, 13092320, 13223905, 13289954, 13421539, 13487588, 
            13618917, 13684966, 13816551, 13882600, 14014185, 14080234, 14211819, 14277611, 
            14343403, 14409196, 14474988, 14606317, 14672109, 14737902, 14803950, 14869743, 
            15001071, 15066864, 15132656, 15198449, 15329777, 15395826, 15461618, 15527411, 
            15593203, 15724531, 15790324, 15856116, 15922165, 15987957, 16119286, 16185078, 
            16250613, 16250355, 16250096, 16249838, 16315115, 16315112, 16314854, 16380131, 
            16379873, 16379614, 16379356, 16444633, 16444375, 16444116, 16443858, 16509391, 
            16509132, 16508874, 16574151, 16573893, 16573634, 16573376, 16638653, 16638395, 
            16638136, 16703670, 16637618, 16637103, 16636844, 16636328, 16636069, 16635554, 
            16635295, 16634779, 16634264, 16634005, 16633490, 16633230, 16632715, 16632456, 
            16631941, 16631425, 16631166, 16630651, 16630392, 16629876, 16629617, 16629102, 
            16628587, 16628327, 16627812, 16561761, 16495710, 16429659, 16363608, 16232021, 
            16165969, 16099918, 16033867, 15967816, 15901509, 15835458, 15703871, 15637820, 
            15571769, 15505718, 15439666, 15373615, 15307308, 15175721, 15109670, 15043619, 
            14977568, 14911517, 14845466, 14779415, 14713364, 14581779, 14450194, 14318866, 
            14187281, 14121233, 13989904, 13858320, 13726735, 13660943, 13529358, 13397773, 
            13266445, 13200396, 13068812, 12937483, 12805899, 12740106, 12608522, 12476937, 
            12345609, 12214024, 12147975, 12016647, 11885062, 11753478, 11622150, 11490822, 
            11359494, 11228166, 11096582, 10965254, 10833926, 10702598, 10571270, 10439942, 
            10308614, 10177030, 10045702, 09914375, 09783047, 09651719, 09520391, 09389063, 
            09257479, 09126151, 08994823, 08863495, 08732167, 08600839, 08469511, 08338184, 
        };
    }
}