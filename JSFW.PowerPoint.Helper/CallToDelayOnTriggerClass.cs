using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSFW.PowerPoint.Helper
{
    /// <summary>
    /// 호출되면 대기중에 트리거!!
    /// </summary>
    public class CallToDelayOnTriggerClass
    {
        public float DelayTime = 1.2f;

        DateTime CalledTime;

        Action TriggerAction = null;

        /// <summary>
        /// 호출되는 메소드는 별도 Thread 영역임으로 주의!!
        /// invoke 호출이 필요함.
        /// </summary>
        /// <param name="triggerAction"></param>
        public void CallBy(Action triggerAction = null)
        {
            if (triggerAction == null) return;

            CalledTime = DateTime.Now;

            if (TriggerAction == null)
            {
                TriggerAction = new Action(() =>
                {
                    while (DateTime.Now.AddSeconds(-DelayTime) < CalledTime)
                    {
                        Thread.Sleep(100);
                    }
                    triggerAction?.Invoke();
                });


                TriggerAction?.BeginInvoke(ir =>
                {
                    TriggerAction.EndInvoke(ir);
                    TriggerAction = null;
                }, null);
            }
        }
    }

    public static class CtrlEx
    {
        public static void Sync<T>(this T ctrl, Action<T> invokeAction = null) where T : Control
        {
            if (ctrl == null) return;

            if (ctrl.InvokeRequired)
            {
                ctrl.Invoke(new MethodInvoker(() => invokeAction?.Invoke(ctrl)));
            }
            else
            {
                invokeAction?.Invoke(ctrl);
            }
        }
    }
}
