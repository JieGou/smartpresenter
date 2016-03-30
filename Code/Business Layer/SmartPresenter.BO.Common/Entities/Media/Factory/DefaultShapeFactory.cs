using SmartPresenter.BO.Common.Enums;
using SmartPresenter.BO.Common.Interfaces;
using SmartPresenter.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPresenter.BO.Common.Entities
{
    /// <summary>
    /// class for creating shape factories
    /// </summary>
    internal class DefaultShapeFactory : AbstractShapeFactory
    {
        #region Method

        /// <summary>
        /// Creates the factory based on the type given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public override IShapeFactory CreateFactory(ElementType elementType)
        {
            switch(elementType)
            {
                case ElementType.Audio:
                    return new AudioFactory();
                    break;
                case ElementType.Circle:
                    return new CircleFactory();
                    break;
                case ElementType.DVD:
                    return new DVDFactory();
                    break;
                case ElementType.Ellipse:
                    return new EllipseFactory();
                    break;
                case ElementType.Image:
                    return new ImageFactory();
                    break;
                case ElementType.LiveVideo:
                    return new LiveVideoFactory();
                    break;                
                case ElementType.Polygon:
                    return new PolygonFactory();
                    break;
                case ElementType.Rectangle:
                    return new RectangleFactory();
                    break;
                case ElementType.Square:
                    return new SquareFactory();
                    break;
                case ElementType.Text:
                    return new TextFactory();
                    break;
                case ElementType.Triangle:
                    return new TriangleFactory();
                    break;
                case ElementType.Video:
                    return new VideoFactory();
                    break;
                case ElementType.Path:
                case ElementType.Line:
                default:
                    return NullShapeFactory.Instance;
                    break;
            }
        }

        #endregion        
    
        /// <summary>
        /// Creates the factory based on the name given.
        /// </summary>
        /// <param name="elementType">Type of the element.</param>
        /// <returns></returns>
        public override IShapeFactory CreateFactory(string elementType)
        {
            switch (elementType.ToLower())
            {
                case "audio":
                    return new AudioFactory();
                    break;
                case "circle":
                    return new CircleFactory();
                    break;
                case "dvd":
                    return new DVDFactory();
                    break;
                case "ellipse":
                    return new EllipseFactory();
                    break;
                case "image":
                    return new ImageFactory();
                    break;
                case "livevideo":
                    return new LiveVideoFactory();
                    break;
                case "polygon":
                    return new PolygonFactory();
                    break;
                case "rectangle":
                    return new RectangleFactory();
                    break;
                case "square":
                    return new SquareFactory();
                    break;
                case "text":
                    return new TextFactory();
                    break;
                case "triangle":
                    return new TriangleFactory();
                    break;
                case "video":
                    return new VideoFactory();
                    break;
                case "path":
                case "line":
                default:
                    return NullShapeFactory.Instance;
                    break;
            }
        }
    }
}
