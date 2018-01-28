using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClient
{
    public abstract class SendStrategy
    {
        public abstract void SendMessage(string message);
    }
}
