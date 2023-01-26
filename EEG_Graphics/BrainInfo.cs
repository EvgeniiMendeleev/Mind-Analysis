using System;
using System.CodeDom;

namespace NeuroTGAM
{
    public class BrainInfo : ICloneable
    {
        public uint second;
        public uint meditation;
        public uint attention;
        public uint alphaLow;
        public uint alphaHigh;
        public uint betaLow;
        public uint betaHigh;
        public uint gammaLow;
        public uint gammaHigh;
        public uint theta;
        public uint delta;

        public object Clone() => MemberwiseClone();
    }
}
