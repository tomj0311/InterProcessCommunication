using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvpChatServer.View;
using System.Timers;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MvpChatServer.Presenter
{
    public class ReceivePresenter: NativeWindow
    {
        private readonly IReceiveView _receiveView;

        public ReceivePresenter(IReceiveView receiveView)
        {
            _receiveView = receiveView;
            receiveView.showMessages += GetMessages;
        }

        private void GetMessages(object sender, EventArgs e)
        {
            _receiveView.DisplayMessage();
        }
    }
}
