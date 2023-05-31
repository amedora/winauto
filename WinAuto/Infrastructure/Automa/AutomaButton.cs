using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace WinAuto.Infrastructure.Automa
{
    public class AutomaButton
    {
        private readonly AutomationElement _element;
        private readonly InvokePattern _buttonInvoke;

        public AutomaButton(AutomationElement targetWindow, string automationId)
        {
            _element = targetWindow.FindFirst(TreeScope.Subtree,
                new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));

            _buttonInvoke = _element
                .GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
        }

        public void Focus()
        {
            _element.SetFocus();
        }

        public void Click()
        {
            _buttonInvoke.Invoke();
        }
    }
}
