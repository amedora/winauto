using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace WinAuto.Infrastructure.Automa
{
    public class AutomaTextBox
    {
        private readonly AutomationElement _element;

        public AutomaTextBox(AutomationElement targetWindow, string automationId)
        {
            _element = targetWindow.FindFirst(TreeScope.Subtree,
                new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
        }

        public void Focus()
        {
            _element.SetFocus();
        }
    }
}
