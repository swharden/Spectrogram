using System;

namespace Spectrogram.Colormaps
{
    class ViridisReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the ViridisReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            16639780, 16442914, 16311841, 16180767, 15983902, 15852828, 15656219, 15524890, 
            15328281, 15197209, 15000344, 14803736, 14672664, 14475800, 14344728, 14148121, 
            13951258, 13820187, 13623580, 13492253, 13295646, 13099039, 12967713, 12771106, 
            12574500, 12443174, 12246567, 12049705, 11918635, 11722028, 11525166, 11394096, 
            11197234, 11000627, 10869301, 10672695, 10475832, 10344762, 10147900, 09951294, 
            09819967, 09623361, 09492035, 09295428, 09164102, 08967495, 08836169, 08639307, 
            08508236, 08311374, 08180303, 07983441, 07852114, 07655508, 07524181, 07392854, 
            07196248, 07064921, 06933595, 06802524, 06605661, 06474335, 06343008, 06211937, 
            06015074, 05883748, 05752421, 05621350, 05490023, 05358696, 05227369, 05096043, 
            04964972, 04833645, 04702318, 04570991, 04505200, 04374129, 04242802, 04111475, 
            04045684, 03914357, 03783030, 03717494, 03586167, 03520376, 03389049, 03323258, 
            03191930, 03126139, 03060348, 02929021, 02863485, 02797694, 02731903, 02666111, 
            02600320, 02534529, 02468737, 02402946, 02337154, 02336899, 02271108, 02205572, 
            02205317, 02139525, 02139269, 02073478, 02073222, 02072967, 02007175, 02006920, 
            02006664, 02006408, 02006153, 02005897, 02005641, 02005386, 02005386, 02005130, 
            02004874, 02070155, 02069899, 02069643, 02069387, 02069132, 02134412, 02134156, 
            02133900, 02199180, 02198924, 02198669, 02263949, 02263693, 02263437, 02328973, 
            02328717, 02328461, 02393741, 02393485, 02393229, 02458509, 02458254, 02523534, 
            02523278, 02523022, 02588302, 02588046, 02587790, 02653070, 02652814, 02652814, 
            02718094, 02717838, 02783118, 02782862, 02782606, 02847886, 02847630, 02912910, 
            02912654, 02912398, 02977678, 02977422, 03042702, 03042446, 03042190, 03107469, 
            03107213, 03172493, 03172237, 03237517, 03237261, 03237005, 03302285, 03302029, 
            03367309, 03367053, 03432333, 03432077, 03497356, 03497100, 03562380, 03562124, 
            03627404, 03627148, 03692428, 03692171, 03757451, 03757195, 03822475, 03822219, 
            03887498, 03887242, 03952266, 03952010, 04017289, 04017033, 04016777, 04082057, 
            04081800, 04147080, 04146567, 04211847, 04211591, 04276870, 04276614, 04341893, 
            04341381, 04341124, 04406404, 04406147, 04405891, 04471170, 04470657, 04535937, 
            04535680, 04535423, 04534911, 04600190, 04599933, 04599676, 04599164, 04664443, 
            04664186, 04663929, 04663416, 04663159, 04662902, 04662645, 04727668, 04727411, 
            04727154, 04726897, 04726384, 04726127, 04725870, 04725356, 04725099, 04659306, 
            04658793, 04658535, 04658278, 04657765, 04657507, 04656994, 04591201, 04590687, 
            04590430, 04589916, 04524123, 04523610, 04523352, 04457303, 04457045, 04456788, 
        };
    }
}