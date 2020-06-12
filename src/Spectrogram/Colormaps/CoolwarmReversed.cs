using System;

namespace Spectrogram.Colormaps
{
    class CoolwarmReversed : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the CoolwarmReversed colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            11731750, 11864103, 11930920, 12063017, 12129834, 12261931, 12328748, 12460845, 
            12527662, 12659248, 12725553, 12857394, 12923443, 12989748, 13121589, 13187895, 
            13253944, 13385529, 13451834, 13517884, 13583933, 13715774, 13781823, 13847872, 
            13913922, 14045763, 14111812, 14177862, 14243911, 14309960, 14376010, 14442059, 
            14508108, 14574158, 14640207, 14706256, 14772306, 14838355, 14904404, 14970454, 
            15036503, 15102553, 15103066, 15168860, 15234909, 15300958, 15367008, 15367521, 
            15433571, 15499364, 15499878, 15565927, 15631977, 15632234, 15698284, 15764333, 
            15764847, 15830640, 15831154, 15896947, 15897461, 15963510, 15963768, 15964281, 
            16030331, 16030588, 16031102, 16096895, 16097409, 16097666, 16163716, 16163974, 
            16164487, 16164745, 16165258, 16165516, 16165773, 16231823, 16232081, 16232594, 
            16232852, 16233109, 16233367, 16233880, 16168602, 16168860, 16169373, 16169631, 
            16169888, 16170146, 16170404, 16105381, 16105639, 16105896, 16040618, 16040875, 
            16041133, 15975855, 15976112, 15910834, 15911091, 15911349, 15846070, 15846328, 
            15781049, 15715771, 15716028, 15650750, 15585216, 15585473, 15520195, 15520452, 
            15455174, 15389639, 15324361, 15259082, 15193804, 15193805, 15128527, 15063248, 
            14997713, 14932435, 14866900, 14801622, 14736087, 14670809, 14605274, 14539995, 
            14474461, 14408926, 14343391, 14277856, 14212065, 14146530, 14080996, 14015461, 
            13884390, 13818599, 13753064, 13687529, 13621738, 13556203, 13490668, 13424877, 
            13293806, 13228014, 13162479, 13096688, 13031153, 12965362, 12834290, 12768499, 
            12702964, 12637173, 12506101, 12440310, 12374518, 12308983, 12177656, 12111864, 
            12046329, 11980537, 11849210, 11783418, 11717627, 11586555, 11520763, 11454972, 
            11323644, 11257852, 11192061, 11060733, 10994941, 10929149, 10797822, 10732030, 
            10666238, 10534910, 10469118, 10337790, 10271998, 10206206, 10074878, 10009086, 
            09943294, 09811966, 09745918, 09614590, 09548798, 09483006, 09351677, 09285629, 
            09154301, 09088509, 09022716, 08891132, 08825340, 08759547, 08627963, 08562171, 
            08430842, 08364794, 08299001, 08167673, 08101624, 08035832, 07904247, 07838454, 
            07706870, 07641077, 07575028, 07443700, 07377907, 07311858, 07180529, 07114481, 
            07048688, 06917103, 06851054, 06785261, 06653676, 06587883, 06521834, 06390506, 
            06324456, 06258407, 06127078, 06061029, 05994980, 05929187, 05797602, 05731809, 
            05665760, 05599710, 05468381, 05402332, 05336283, 05270490, 05138904, 05072855, 
            05007062, 04941012, 04809427, 04743377, 04677584, 04611535, 04545485, 04414156, 
            04348106, 04282057, 04216007, 04150214, 04084164, 03952579, 03886529, 03820736, 
        };
    }
}