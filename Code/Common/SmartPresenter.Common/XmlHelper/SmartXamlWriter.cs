using System;
using System.Linq;
using System.Windows.Markup;
using System.Xml.Linq;
namespace SmartPresenter.Common
{
    /// <summary>
    /// A XamlWriter which removes namespaces while saving.
    /// </summary>
    public static class SmartXamlWriter
    {
        /// <summary>
        /// Saves the specified object.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static string Save(Object obj)
        {
            string markup = XamlWriter.Save(obj);
            XElement root = RemoveAllNamespaces(XDocument.Parse(markup).Root);
            return root.ToString();
        }

        /// <summary>
        /// Removes all namespaces.
        /// </summary>
        /// <param name="xmlDocument">The XML document.</param>
        /// <returns></returns>
        private static XElement RemoveAllNamespaces(XElement xmlDocument)
        {
            if (!xmlDocument.HasElements)
            {
                XElement xElement = new XElement(xmlDocument.Name.LocalName);
                xElement.Value = xmlDocument.Value;

                foreach (XAttribute attribute in xmlDocument.Attributes())
                {
                    if (attribute.IsNamespaceDeclaration == false)
                    {
                        xElement.Add(attribute);
                    }
                }

                return xElement;
            }
            XElement newXElement = new XElement(xmlDocument.Name.LocalName, xmlDocument.Elements().Select(el => RemoveAllNamespaces(el)));
            foreach (XAttribute attribute in xmlDocument.Attributes())
            {
                if (attribute.IsNamespaceDeclaration == false)
                {
                    newXElement.Add(attribute);
                }
            }

            return newXElement;
        }
    }
}
