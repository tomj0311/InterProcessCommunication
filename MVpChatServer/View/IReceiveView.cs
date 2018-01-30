using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvpChatServer.View
{
    public interface IReceiveView
    {
        event EventHandler showMessages;
        void DisplayMessage();
    }
}
