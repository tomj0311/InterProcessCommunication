using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    public class SendStrategyMMF : SendStrategy
    {
        public override void SendMessage(string message)
        {
            const int MMF_MAX_SIZE = 1024;
            const int MMF_VIEW_SIZE = 1024;

            var mutex = new Mutex(false, "MmfMutex");

            MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("MMF_MVP_CHAT", MMF_MAX_SIZE);

            MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, MMF_VIEW_SIZE);

            BinaryFormatter formatter = new BinaryFormatter();

            mutex.WaitOne();
            formatter.Serialize(mmvStream, message);
            mmvStream.Seek(0, SeekOrigin.Begin);
            mutex.ReleaseMutex();
        }
    }
}
