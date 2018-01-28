using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClient
{
    class Program
    {
        private const int WM_COPYDATA = 0x4A;

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam);

        static void Main(string[] args)
        {
            bool quit = false;

            Console.WriteLine("Please enter person name : ");
            while (!quit)
            {
                Console.WriteLine("Person Name: ");
                string person = Console.ReadLine();

                Console.WriteLine("Message: ");
                string message = Console.ReadLine();

                Console.WriteLine("1. Send via SendMessage API");
                Console.WriteLine("2. Send via Memory Mapped File");
                Console.WriteLine("3. Send via Sockets");
                int sendStrategy = Convert.ToInt16(Console.ReadLine());

                if (!string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(person))
                {

                    Send send = new Send();

                    switch (sendStrategy)
                    {
                        case 1:
                            send.SetSendStrategy(new SendStrategySendMessageAPI());
                            send.SendMessage(person + ", " + message);
                            break;

                        case 2:
                            send.SetSendStrategy(new SendStrategyMMF());
                            send.SendMessage(person + ", " + message);
                            break;

                        case 3:
                            send.SetSendStrategy(new SendStrategySocket());
                            send.SendMessage(person + ", " + message);
                            break;

                        default:
                            send.SetSendStrategy(new SendStrategySocket());
                            send.SendMessage(person + ", " + message);
                            break;
                    }
                }
                else
                {
                    quit = true;
                }
            }
        }

        static void WriteToMMF(string message)
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

        static void WriteToSendMessageAPI(string message)
        {
            foreach (Process clsProcess in Process.GetProcesses())
            {
                Process[] pname = Process.GetProcessesByName("ReceiveForm");
                var pLength = pname.Length;
                if (pLength == 0)
                {
                    COPYDATASTRUCT cds;
                    cds.dwData = 0;
                    cds.lpData = (int)Marshal.StringToHGlobalAnsi(message);
                    cds.cbData = message.Length;
                    SendMessage(clsProcess.MainWindowHandle, (int)WM_COPYDATA, 0, ref cds);
                }
            }
        }
    }
}
