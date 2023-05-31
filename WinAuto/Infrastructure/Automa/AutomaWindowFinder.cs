using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;

namespace WinAuto.Infrastructure.Automa
{
    public class AutomaWindowFinder
    {
        public static AutomaWindow FindWindowByName(string windowName)
        {
            var condition = new PropertyCondition(AutomationElement.NameProperty, windowName);

            return FindByCondition(condition);
        }

        public static AutomaWindow FindWindowByAutomationId(string windowAutomationId)
        {
            var condition = new PropertyCondition(AutomationElement.AutomationIdProperty, windowAutomationId);

            return FindByCondition(condition);
        }

        private static AutomaWindow FindByCondition(Condition condition)
        {
            var globalRoot = AutomationElement.RootElement;
            var criteria = new AndCondition(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                condition);
            var element = globalRoot.FindFirst(TreeScope.Children, criteria);

            if (element != null)
            {
                return new AutomaWindow(element);
            }
            else
            {
                return null;
            }
        }
    }
}
