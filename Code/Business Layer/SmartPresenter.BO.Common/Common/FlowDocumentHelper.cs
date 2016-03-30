using System;
using System.Text;
using System.Windows.Documents;

namespace SmartPresenter.BO.Common
{
    /// <summary>
    /// 
    /// </summary>
    public static class FlowDocumentHelper
    {
        public static string GetPlainText(FlowDocument doc)
        {
            StringBuilder sb = new StringBuilder();
            for (TextPointer position = doc.ContentStart;
                position != null && position.CompareTo(doc.ContentEnd) <= 0;
                position = position.GetNextContextPosition(LogicalDirection.Forward))
            {
                TextPointerContext context = position.GetPointerContext(LogicalDirection.Backward);
                if (context == TextPointerContext.Text)
                {
                    if (position.Parent is Run)
                    {
                        Run run = position.Parent as Run;
                        sb.Append(run == null ? Environment.NewLine : run.Text);
                    }
                }
            }

            return sb.ToString();
        }
    }
}
