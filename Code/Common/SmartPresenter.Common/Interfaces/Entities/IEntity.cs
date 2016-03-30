using System;

namespace SmartPresenter.Common.Interfaces
{
    // An application entity with a unique identifier.
    public interface IEntity : ICloneable
    {
        #region Properties

        // Unique identifier of an entity.
        Guid Id { get; }

        #endregion
    }
}
