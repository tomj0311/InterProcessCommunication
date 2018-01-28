using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MvpChatServer
{
    public class ReceiveViaMemmoryMappedFile 
    {
        public string ReceiveMessage()
        {
            const int MMF_SIZE = 1024;

            string message = null;

            var mutex = new Mutex(false, "MmfMutex");

            try
            {
                using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("MMF_MVP_CHAT"))
                {
                    using (MemoryMappedViewStream mmvStream = mmf.CreateViewStream(0, MMF_SIZE))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();

                        byte[] buffer = new byte[MMF_SIZE];

                        if (mmvStream.CanRead)
                        {
                            mutex.WaitOne();

                            mmvStream.Read(buffer, 0, MMF_SIZE);
                            var mmfdata = (string)formatter.Deserialize(new MemoryStream(buffer));
                            if (mmfdata == "###")
                            {
                                message = null;
                            }
                            else
                            {
                                message = mmfdata;
                            }

                            mmvStream.Seek(0, SeekOrigin.Begin);
                            formatter.Serialize(mmvStream, "###");

                            Thread.Sleep(1000);

                            mutex.ReleaseMutex();
                        }
                    }
                }
            }
            catch (FileNotFoundException fnfex)
            {
                // Handle MMF not found exception
            }
            catch (Exception ex)
            {
                // handle other exceptions
            }
            return message;
        }
    }
}
