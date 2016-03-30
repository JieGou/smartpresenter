using SmartPresenter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.Data.Entities
{
    /// <summary>
    /// class for creating shape factories
    /// </summary>
    internal class DefaultShapeFactor
    {
        #region Method

        /// <summary>
        /// Creates the factory based on the type given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public ShapeDTO CreateShape(ElementType elementType)
        {
            switch(elementType)
            {
                case ElementType.Audio:
                    return new AudioDTO();                    
                case ElementType.Circle:
                    return new CircleDTO();                    
                case ElementType.DVD:
                    return new DVDDTO();                    
                case ElementType.Ellipse:
                    return new EllipseDTO();                    
                case ElementType.Image:
                    return new ImageDTO();                    
                case ElementType.LiveVideo:
                    return new LiveVideoDTO();                                    
                case ElementType.Polygon:
                    return new PolygonDTO();                    
                case ElementType.Rectangle:
                    return new RectangleDTO();                    
                case ElementType.Square:
                    return new SquareDTO();                    
                case ElementType.Text:
                    return new TextDTO();                    
                case ElementType.Triangle:
                    return new TriangleDTO();                    
                case ElementType.Video:
                    return new VideoDTO();                    
                case ElementType.Path:
                case ElementType.Line:
                default:
                    return null;                    
            }            
        }

        #endregion        
    }
}
