
namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// An interface for creating different types of User Accounts.
    /// </summary>
    public interface IUserAccountFactory
    {
        /// <summary>
        /// Creates the new local user account.
        /// </summary>
        /// <param name="eMail">The e mail.</param>
        /// <param name="id">The User Id.</param>
        /// <param name="password">The password.</param>
        IUserAccount CreateLocalUserAccount(string eMail, string displayName, string password);

        /// <summary>
        /// Creates the new cloud user account.
        /// </summary>
        /// <param name="eMail">The e mail.</param>
        /// <param name="id">The User Id.</param>
        /// <param name="password">The password.</param>
        IUserAccount CreateCloudUserAccount(string eMail, string displayName, string password);

    }
}

