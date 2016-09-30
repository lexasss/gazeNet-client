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

            GazeNetClient gazeNetClient;

            try
            {
                gazeNetClient = new GazeNetClient();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            if (gazeNetClient.AutoStarter?.Enabled == true)
                gazeNetClient.AutoStarter.run(gazeNetClient);

            Application.Run();

            gazeNetClient.Dispose();
        }
    }
}
