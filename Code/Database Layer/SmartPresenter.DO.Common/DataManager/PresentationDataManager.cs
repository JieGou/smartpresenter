using SmartPresenter.Common.Logger;
using SmartPresenter.Data.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Data.Common
{
    public class PresentationDataManager
    {
        /// <summary>
        /// Loads the presentation.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException">Specified presentation is not found</exception>
        public static PresentationDTO LoadPresentation(string path)
        {
            Logger.LogEntry();

            if (File.Exists(path) == false)
            {
                throw new FileNotFoundException("Specified presentation is not found", path);
            }
            Serializer<PresentationDTO> serializer = new Serializer<PresentationDTO>();
            PresentationDTO presentationDTO = (PresentationDTO)serializer.Load(path);

            Logger.LogExit();
            return presentationDTO;
        }
    }
}
