using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvpChatServer.View;
using MvpChatServer.Presenter;

namespace MvpChatServer
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var mainForm = new ReceiveForm();
            var presenter = new ReceivePresenter(mainForm);

            Application.Run(mainForm);
        }
    }
}
