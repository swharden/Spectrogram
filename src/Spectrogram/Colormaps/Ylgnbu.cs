using System;

namespace Spectrogram.Colormaps
{
    class Ylgnbu : IColormap
    {
        public (byte r, byte g, byte b) GetRGB(byte value)
        {
            byte[] bytes = BitConverter.GetBytes(rgb[value]);
            return (bytes[2], bytes[1], bytes[0]);
        }

        // RGB values are derived from the Ylgnbu colormap in Matplotlib 3.2.1 (https://matplotlib.org)
        private readonly int[] rgb =
        {
            16777177, 16711383, 16645846, 16645845, 16580307, 16580050, 16514513, 16514512, 
            16448974, 16383437, 16383180, 16317643, 16317641, 16252104, 16251847, 16186310, 
            16120772, 16120771, 16055234, 16054977, 15989439, 15989438, 15923901, 15923644, 
            15858106, 15792569, 15792568, 15727031, 15726773, 15661236, 15661235, 15595698, 
            15529905, 15464369, 15398833, 15333041, 15267505, 15136177, 15070641, 15004849, 
            14939313, 14873521, 14742449, 14676658, 14611122, 14545330, 14479794, 14348466, 
            14282930, 14217138, 14151602, 14086066, 14020274, 13889202, 13823411, 13757875, 
            13692083, 13626547, 13495219, 13429683, 13363891, 13298355, 13232563, 13101491, 
            13035700, 12904372, 12707764, 12576436, 12445108, 12313781, 12116917, 11985589, 
            11854261, 11722934, 11526070, 11394998, 11263670, 11132342, 10935479, 10804151, 
            10672823, 10541495, 10344632, 10213560, 10082232, 09950904, 09754040, 09622713, 
            09491385, 09294521, 09163193, 09032121, 08900794, 08703930, 08572602, 08441274, 
            08309947, 08178875, 08047547, 07916219, 07784892, 07653820, 07522492, 07391165, 
            07259837, 07128765, 06997437, 06866110, 06735038, 06603710, 06537919, 06406591, 
            06275519, 06144191, 06012864, 05881792, 05750464, 05619137, 05487809, 05356737, 
            05225409, 05094082, 04962754, 04831682, 04700355, 04569027, 04437955, 04306627, 
            04240835, 04175043, 04109251, 04043203, 03911875, 03846083, 03780291, 03714499, 
            03648706, 03582658, 03516866, 03451074, 03319746, 03253954, 03188162, 03122370, 
            03056321, 02990529, 02924737, 02793409, 02727617, 02661825, 02595777, 02529985, 
            02464192, 02398400, 02332608, 02201280, 02135232, 02069440, 02003648, 01937856, 
            01937599, 01937086, 01936574, 01936317, 01935804, 01935547, 02000571, 02000058, 
            01999801, 01999288, 01999032, 01998519, 01998006, 02063285, 02062772, 02062516, 
            02062003, 02061490, 02061233, 02126257, 02126000, 02125487, 02124974, 02124718, 
            02124205, 02189484, 02188971, 02188459, 02188202, 02187689, 02187432, 02186920, 
            02252199, 02251686, 02251430, 02251173, 02250661, 02250404, 02250147, 02249635, 
            02249378, 02249121, 02314145, 02313888, 02313632, 02313119, 02312862, 02312606, 
            02312093, 02311836, 02311580, 02311067, 02310810, 02376090, 02375833, 02375321, 
            02375064, 02374807, 02374295, 02374038, 02373781, 02373269, 02373012, 02372756, 
            02372498, 02306704, 02240910, 02175372, 02109578, 02043784, 01978247, 01912453, 
            01846659, 01846401, 01780863, 01715069, 01649275, 01583481, 01517944, 01452150, 
            01386356, 01320818, 01255024, 01189230, 01188972, 01123434, 01057640, 00991847, 
            00926309, 00860515, 00794721, 00728927, 00663389, 00597595, 00531801, 00531800, 
        };
    }
}