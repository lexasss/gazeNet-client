using System;
using System.Windows.Forms;

namespace GazeNetClient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            GazeNetClient gazeNetClient = new GazeNetClient();

            if (gazeNetClient.AutoStarter.Enabled)
            {
                gazeNetClient.AutoStarter.run(gazeNetClient);
            }

            Application.Run();

            gazeNetClient.Dispose();
        }
    }
}
