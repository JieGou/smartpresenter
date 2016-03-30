
using SmartPresenter.UI.Common.Interfaces;
using System.Windows;
using System.Windows.Threading;
namespace SmartPresenter.UI.Common
{
    /// <summary>
    /// A user interaction service to simplify user interaction operations.
    /// </summary>
    public class UIInteractionService : IInteractionService
    {
        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="message">The message.</param>
        /// <param name="button">The button.</param>
        /// <param name="icon">The icon.</param>
        /// <returns></returns>
        public MessageBoxResult ShowMessageBox(string caption, string message, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(message, caption, button, icon);
        }

        /// <summary>
        /// Invokes the provided action on UI thread.
        /// </summary>
        /// <param name="action">The action.</param>
        public void InvokeOnUIThread(System.Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action, DispatcherPriority.Normal);
        }
    }
}
