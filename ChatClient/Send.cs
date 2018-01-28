using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public class Send
    {
        private SendStrategy _sendStrategy;
        public void SetSendStrategy(SendStrategy sendStrategy)
        {
            _sendStrategy = sendStrategy;
        }

        public void SendMessage(string message)
        {
            _sendStrategy.SendMessage(message);
        }
    }
}
