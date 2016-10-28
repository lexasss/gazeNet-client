using System.Windows.Forms;

namespace GazeNetClient.Utils
{
    public class UI
    {
        public delegate bool IsConditionMet(Control aControl);

        public static void setGroupEnabling(Control aContainer, IsConditionMet[] aConditions)
        {
            foreach (Control ctrl in aContainer.Controls)
            {
                bool enabled = true;
                foreach (IsConditionMet condition in aConditions)
                    enabled = enabled && condition(ctrl);

                ctrl.Enabled = enabled;
            }
        }
    }
}
