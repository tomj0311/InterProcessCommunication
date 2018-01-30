using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvpChatServer.View;

namespace MvpChatServer
{
    public class ReceiveViaSendMessageAPI : NativeWindow
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, ref COPYDATASTRUCT lParam);

        private const int WM_COPYDATA = 0x4A;

        [StructLayout(LayoutKind.Sequential)]
        struct COPYDATASTRUCT
        {
            public int dwData;
            public int cbData;
            public int lpData;
        }

        private string message = string.Empty;
        private readonly ReceiveForm _receiveForm;

        public ReceiveViaSendMessageAPI(ReceiveForm receiveForm)
        {
            receiveForm.HandleCreated += new EventHandler(this.OnHandleCreated);
            receiveForm.HandleDestroyed += new EventHandler(this.OnHandleDestroyed);
            _receiveForm = receiveForm;
        }

        internal void OnHandleCreated(object sender, EventArgs e)
        {
            AssignHandle(((ReceiveForm)sender).Handle);
        }
        internal void OnHandleDestroyed(object sender, EventArgs e)
        {
            ReleaseHandle();
        }

        public string ReceiveMessage()
        {
            throw new NotImplementedException();
        }

        // Receive messages send through SendMessageAPI
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_COPYDATA:

                    COPYDATASTRUCT CD = (COPYDATASTRUCT)m.GetLParam(typeof(COPYDATASTRUCT));
                    byte[] B = new byte[CD.cbData];
                    IntPtr lpData = new IntPtr(CD.lpData);
                    Marshal.Copy(lpData, B, 0, CD.cbData);
                    string strData = Encoding.Default.GetString(B);

                    _receiveForm.messageList.Add(strData);

                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
