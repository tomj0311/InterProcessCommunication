using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MvpChatServer.View
{
    public partial class ReceiveForm : Form, IReceiveView
    {
        private ReceiveViaSendMessageAPI _sendMessageAPIListener;
        private ReceiveViaSocket _socketServer;

        private Timer timer1 = new Timer();

        public event EventHandler GetMessages;

        public ReceiveForm()
        {
            InitializeComponent();

            _sendMessageAPIListener = new ReceiveViaSendMessageAPI(this);
            _socketServer = new ReceiveViaSocket(this);

            timer1.Interval = 10000;
            timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.OnTimerElapsed);
        }

        public void DisplayMessage(string message)
        {
            var msg = new ListViewItem(new string[] { message });
            if (msg != null)
            {
                if (MessagesList.InvokeRequired == true)
                    MessagesList.Invoke((MethodInvoker)delegate { MessagesList.Items.Add(msg);});

                else
                    MessagesList.Items.Add(msg);
            }
        }
        
        private void OnTimerElapsed(object sender, EventArgs e)
        {
            GetMessages?.Invoke(sender, e);
        }
    }
}
