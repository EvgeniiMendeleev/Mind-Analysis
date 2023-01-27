using NeuroTGAM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EEG_Graphics
{
    public abstract class NeuroModuleBase
    {
        protected Thread _readingThread;
        protected readonly Mutex _mutex;                                                //Для синхронизации потоков.
        public abstract bool AreDataReading { get; }
        public NeuroModuleBase()
        {
            _readingThread = new Thread(ReadingThread);
            _mutex = new Mutex();
            _readingThread.IsBackground = true;
        }

        public abstract void Connect();
        //public abstract bool AreDataReading();
        
        public abstract BrainInfo GetCurrentBrainData();
        public abstract void CloseConnection();
        protected abstract void ReadingThread();
    }
}
