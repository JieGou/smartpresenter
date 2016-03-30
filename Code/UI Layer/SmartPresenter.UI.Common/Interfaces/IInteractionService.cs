using System;
using System.Windows;

namespace SmartPresenter.UI.Common.Interfaces
{
    /// <summary>
    /// A user interaction service to simplify user interaction operations.
    /// </summary>
    public interface IInteractionService
    {
        /// <summary>
        /// Shows the message box.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="button">The button.</param>
        /// <param name="image">The image.</param>
        /// <returns></returns>
        MessageBoxResult ShowMessageBox(string title, string message, MessageBoxButton button, MessageBoxImage image);
        /// <summary>
        /// Invokes the provided action on UI thread.
        /// </summary>
        /// <param name="action">The action.</param>
        void InvokeOnUIThread(Action action);
    }
}
