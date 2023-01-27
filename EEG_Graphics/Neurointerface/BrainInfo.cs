using System;
using System.CodeDom;

namespace NeuroTGAM
{
    public class BrainInfo : ICloneable
    {
        public uint second = 0;
        public uint meditation = 0;
        public uint attention = 0;
        public uint alphaLow = 0;
        public uint alphaHigh = 0;
        public uint betaLow = 0;
        public uint betaHigh = 0;
        public uint gammaLow = 0;
        public uint gammaHigh = 0;
        public uint theta = 0;
        public uint delta = 0;

        public object Clone() => MemberwiseClone();
    }
}
