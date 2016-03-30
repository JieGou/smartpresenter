using System;
using System.IO;
using System.Windows.Markup;
using System.Xml;

namespace SmartPresenter.Common
{
    /// <summary>
    /// A XamlReader which adds Namespaces while loading.
    /// </summary>
    public static class SmartXamlReader
    {
        /// <summary>
        /// The default namespace
        /// </summary>
        private const string Default_Namespace = " xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"";

        /// <summary>
        /// Loads the specified string.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <returns></returns>
        public static Object Load(string str)
        {
            int index = str.IndexOf(">");
            str = str.Insert(index, Default_Namespace);
            StringReader stringReader = new StringReader(str);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            return XamlReader.Load(xmlReader);
        }
    }
}
