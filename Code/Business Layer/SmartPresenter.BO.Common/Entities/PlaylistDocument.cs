using SmartPresenter.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Entities
{
    [DataContract(Namespace="")]
    public sealed class PlaylistDocument
    {
        #region Properties

        [DataMember]
        public List<PlaylistBase> Playlists { get; set; }

        #endregion

        #region Constructor

        public PlaylistDocument()
        {
            Playlists = new List<PlaylistBase>();
        }

        #endregion
    }
}
