using CsvHelper;
using NeuroTGAM;
using System.IO;
using System.Globalization;

namespace MindAnalysis.NeuroTGAM
{
    internal class MindFile
    {
        private CsvWriter csvWriter;

        public MindFile(string filePath)
        {
            FileStream file = new FileStream(filePath, FileMode.Create, FileAccess.ReadWrite);
            StreamWriter reader = new StreamWriter(file);
            csvWriter = new CsvWriter(reader, CultureInfo.CurrentCulture);
            
            csvWriter.WriteHeader<BrainInfo>();
            csvWriter.NextRecord();
        }

        public void AppendRecord(BrainInfo brainInfo)
        {
            csvWriter.WriteRecord(brainInfo);
            csvWriter.NextRecord();
        }

        public void Close()
        {
            csvWriter.Dispose();
        }
    }
}
