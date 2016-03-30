using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.Common.Media;

namespace SmartPresenter.BO.Media.Test
{
    [TestClass]
    public class VideoPropertiesTest
    {
        [TestMethod]
        public void InitializeTest()
        {
            VideoProperties videoProperties = new VideoProperties(@"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
        }
    }
}
