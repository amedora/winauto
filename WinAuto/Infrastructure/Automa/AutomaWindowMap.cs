using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;

namespace WinAuto.Infrastructure.Automa
{
    public class AutomaWindowMap<T>
    {
        private readonly AutomationElement _targetWindow;
        private readonly List<UpdateActionHandler<T>> _mappings = new List<UpdateActionHandler<T>>();

        public AutomaWindowMap(AutomationElement targetWindow)
        {
            _targetWindow = targetWindow;
        }

        public void AddMapping(string automationId, Expression<Func<T, string>> expression)
        {
            var handler = new UpdateActionHandler<T>(BuildUpdateAction(automationId), expression.Compile());
            _mappings.Add(handler);
        }

        public void Update(T prop)
        {
            foreach (var item in _mappings)
            {
                var propertyValue = item.Lambda(prop);

                try
                {
                    item.WindowItemUpdateAction(propertyValue ?? "");
                }
                catch (ElementNotEnabledException ex)
                {
                    throw new InvalidOperationException("コントロールが編集可能でないため値をセットできません", ex);
                }
            }
        }

        private Action<string> BuildUpdateAction(string automationId)
        {
            var element = _targetWindow.FindFirst(TreeScope.Subtree | TreeScope.Descendants, new PropertyCondition(AutomationElement.AutomationIdProperty, automationId));
            object valuePattern = null;

            if (!element.TryGetCurrentPattern(ValuePattern.Pattern, out valuePattern))
            {
                return value =>
                {
                    element.SetFocus();
                    SendKeys.SendWait(value);
                };
            }
            else
            {
                return value =>
                {
                    var vp = (ValuePattern)valuePattern;
                    // 同じ値をSetValue()するとInvalidOperationExceptionを
                    // 返すことがあるので、同じ値の場合は何もしない。
                    var currentValue = vp.Current.Value;
                    if (value == currentValue)
                        return;

                    vp.SetValue(value);
                };
            }
        }
    }

    class UpdateActionHandler<T>
    {
        public Action<string> WindowItemUpdateAction { get; private set; }
        public Func<T, string> Lambda { get; private set; }

        public UpdateActionHandler(Action<string> action, Func<T, string> lambda)
        {
            WindowItemUpdateAction = action;
            Lambda = lambda;
        }
    }
}
