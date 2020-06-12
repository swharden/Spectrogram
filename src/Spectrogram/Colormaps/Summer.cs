using System;

namespace Spectrogram.Colormaps
{
    class Summer : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the Summer colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            00032614, 00098406, 00163942, 00229734, 00295270, 00361062, 00426598, 00492390, 
            00557926, 00623718, 00689254, 00755046, 00820582, 00886374, 00951910, 01017702, 
            01083238, 01149030, 01214566, 01280358, 01345894, 01411686, 01477222, 01543014, 
            01608550, 01674342, 01739878, 01805670, 01871206, 01936998, 02002534, 02068326, 
            02133862, 02134118, 02265190, 02330982, 02396518, 02396774, 02527846, 02593638, 
            02659174, 02659174, 02790502, 02856294, 02921830, 02922086, 03053158, 03118950, 
            03184486, 03184742, 03315814, 03381606, 03447142, 03447398, 03578470, 03644262, 
            03709798, 03710054, 03841126, 03906918, 03972454, 03972710, 04103782, 04169574, 
            04235110, 04300902, 04300902, 04432230, 04497766, 04563558, 04629094, 04694886, 
            04760422, 04825958, 04826214, 04957542, 05023078, 05088870, 05154406, 05220198, 
            05285734, 05351526, 05351526, 05482854, 05548390, 05614182, 05679718, 05745510, 
            05811046, 05876838, 05876838, 06008166, 06073702, 06139494, 06205030, 06270822, 
            06336358, 06402150, 06402150, 06533478, 06599014, 06664806, 06730342, 06796134, 
            06861670, 06927206, 06927462, 07058790, 07124326, 07190118, 07255654, 07321446, 
            07386982, 07452774, 07452774, 07584102, 07649638, 07715430, 07780966, 07846758, 
            07912294, 07978086, 07978086, 08109414, 08174950, 08240742, 08306278, 08372070, 
            08437606, 08503398, 08568934, 08634726, 08634726, 08766054, 08831590, 08897382, 
            08962918, 09028454, 09094246, 09159782, 09225574, 09291366, 09356902, 09422694, 
            09488230, 09554022, 09619558, 09685350, 09685350, 09816678, 09882214, 09948006, 
            10013542, 10079334, 10144870, 10210662, 10276198, 10341990, 10407526, 10473318, 
            10538854, 10604646, 10670182, 10735974, 10735974, 10867302, 10932838, 10998630, 
            11064166, 11129702, 11195494, 11261030, 11326822, 11392614, 11458150, 11523942, 
            11589478, 11655270, 11720806, 11786598, 11786598, 11917926, 11983462, 12049254, 
            12114790, 12180582, 12246118, 12311910, 12377446, 12443238, 12508774, 12574566, 
            12640102, 12705894, 12771430, 12837222, 12837222, 12968550, 13034086, 13099878, 
            13165414, 13230950, 13296742, 13362278, 13428070, 13493862, 13559398, 13625190, 
            13690726, 13756518, 13822054, 13887846, 13887846, 14019174, 14084710, 14150502, 
            14216038, 14281830, 14347366, 14413158, 14478694, 14544486, 14610022, 14675814, 
            14741350, 14807142, 14872678, 14938470, 14938470, 15069798, 15135334, 15201126, 
            15266662, 15332198, 15397990, 15463526, 15529318, 15595110, 15660646, 15726438, 
            15791974, 15857766, 15923302, 15989094, 15989094, 16120422, 16185958, 16251750, 
            16317286, 16383078, 16448614, 16514406, 16579942, 16645734, 16711270, 16777062, 
        };
    }
}