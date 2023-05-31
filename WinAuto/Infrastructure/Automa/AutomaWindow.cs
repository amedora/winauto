using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace WinAuto.Infrastructure.Automa
{
    public class AutomaWindow
    {

        private readonly AutomationElement _targetWindow;

        public AutomaWindow(AutomationElement targetWindow)
        {
            _targetWindow = targetWindow;
        }

        public AutomaWindowMap<T> Map<T>()
        {
            return new AutomaWindowMap<T>(_targetWindow);
        }

        public AutomaButton Button(string automationId)
        {
            return new AutomaButton(_targetWindow, automationId);
        }

        public AutomaTextBox TextBox(string automationId)
        {
            return new AutomaTextBox(_targetWindow, automationId);
        }
    }
}
