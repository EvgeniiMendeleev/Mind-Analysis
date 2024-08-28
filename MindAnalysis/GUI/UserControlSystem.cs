using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MindAnalysis
{
    class UserControlSystem
    {
        private static UserControlSystem instance;

        private UserControlSystem() { }

        public static UserControlSystem GetSystem()
        {
            if (instance == null) instance = new UserControlSystem();
            return instance;
        }

        public UserControlSystem Disable(Control control)
        {
            control.Enabled = false;
            return instance;
        }

        public UserControlSystem Enable(Control control)
        {
            control.Enabled = true;
            return instance;
        }

        public UserControlSystem DisableParams(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Enabled = false;
            }
            return instance;
        }

        public UserControlSystem EnableParams(params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Enabled = true;
            }
            return instance;
        }
    }
}
