using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Interfaces
{
    /// <summary>
    /// Creates different User Profiles.
    /// </summary>
    public interface IUserProfileFactory
    {
        /// <summary>
        /// Creates the local user prfoile.
        /// </summary>
        IUserProfile CreateLocalUserPrfoile(Guid userAccountId, string displayName);

        /// <summary>
        /// Creates the cloud user prfoile.
        /// </summary>
        IUserProfile CreateCloudUserPrfoile(Guid userAccountId, string displayName);
    }
}
