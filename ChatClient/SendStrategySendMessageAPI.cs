using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class SendStrategySendMessageAPI : SendStrategy
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

        public override void SendMessage(string message)
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
