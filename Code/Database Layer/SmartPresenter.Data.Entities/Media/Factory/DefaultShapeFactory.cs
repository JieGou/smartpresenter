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
    internal class DefaultShapeFactory
    {
        #region Method

        /// <summary>
        /// Creates the factory based on the type given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public ShapeDTO CreateElement(string elementType)
        {
            switch(elementType)
            {
                case "Audio":
                    return new AudioDTO();                    
                case "Circle":
                    return new CircleDTO();                    
                case "DVD":
                    return new DVDDTO();                    
                case "Ellipse":
                    return new EllipseDTO();                    
                case "Image":
                    return new ImageDTO();                    
                case "LiveVideo":
                    return new LiveVideoDTO();                                    
                case "Polygon":
                    return new PolygonDTO();                    
                case "Rectangle":
                    return new RectangleDTO();                    
                case "Square":
                    return new SquareDTO();                    
                case "Text":
                    return new TextDTO();                    
                case "Triangle":
                    return new TriangleDTO();                    
                case "Video":
                    return new VideoDTO();                    
                case "Path":
                case "Line":
                default:
                    return null;                    
            }            
        }

        //public ShapeDTO CreateElement(ElementType elementType)
        //{
        //    switch (elementType)
        //    {
        //        case ElementType.Audio:
        //            return new AudioDTO();
        //        case ElementType.Circle:
        //            return new CircleDTO();
        //        case ElementType.DVD:
        //            return new DVDDTO();
        //        case ElementType.Ellipse:
        //            return new EllipseDTO();
        //        case ElementType.Image:
        //            return new ImageDTO();
        //        case ElementType.LiveVideo:
        //            return new LiveVideoDTO();
        //        case ElementType.Polygon:
        //            return new PolygonDTO();
        //        case ElementType.Rectangle:
        //            return new RectangleDTO();
        //        case ElementType.Square:
        //            return new SquareDTO();
        //        case ElementType.Text:
        //            return new TextDTO();
        //        case ElementType.Triangle:
        //            return new TriangleDTO();
        //        case ElementType.Video:
        //            return new VideoDTO();
        //        case ElementType.Path:
        //        case ElementType.Line:
        //        default:
        //            return null;
        //    }
        //}

        #endregion        
    }
}
