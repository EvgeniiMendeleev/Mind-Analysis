using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EEG_Graphics
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
    }
}
