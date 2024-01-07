using System;
using System.CodeDom;

namespace NeuroTGAM
{
    public class BrainInfo : ICloneable
    {
        public uint Attention { get; set; } = 0;
        public uint Meditation { get; set; } = 0;
        public uint LowAlpha { get; set; } = 0;
        public uint HighAlpha { get; set; } = 0;
        public uint LowGamma { get; set; } = 0;
        public uint HighGamma { get; set; } = 0;
        public uint LowBeta { get; set; } = 0;
        public uint HighBeta { get; set; } = 0;
        public uint Theta { get; set; } = 0;
        public uint Delta { get; set; } = 0;
        public TimeSpan Second { get; set; } = TimeSpan.Zero;

        public object Clone() => MemberwiseClone();
    }
}
