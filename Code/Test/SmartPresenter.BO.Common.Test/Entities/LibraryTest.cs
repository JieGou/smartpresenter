using Microsoft.VisualStudio.TestTools.UnitTesting;
using SmartPresenter.BO.Common.Entities;

namespace SmartPresenter.BO.Common.Test.Entities
{
    [TestClass]
    public class LibraryTest
    {
        [TestMethod]
        public void SaveTest()
        {
            // Arrange.
            PresentationLibrary library = new PresentationLibrary("B:\\Library\\library1");
            library.Name = "library1";

            Presentation presentation1 = new Presentation() { Name = "Test Presentation 1", Category = "Home" };
            Presentation presentation2 = new Presentation() { Name = "Test Presentation 2", Category = "Home" };

            library.Items.Add(presentation1);
            library.Items.Add(presentation2);

            // Act & Assert.
            try
            {
                library.Save();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void LoadTest()
        {
            // Arrange & Act & Assert.
            try
            {
                string testLibraryLocation = "B:\\Library\\library1";
                PresentationLibrary library = new PresentationLibrary(testLibraryLocation);
                library.Name = "library1";

                Presentation presentation1 = new Presentation() { Name = "Test Presentation 1", Category = "Home" };
                Presentation presentation2 = new Presentation() { Name = "Test Presentation 2", Category = "Home" };

                library.Items.Add(presentation1);
                library.Items.Add(presentation2);

                library.Save();

                PresentationLibrary loadedLibrary = new PresentationLibrary(testLibraryLocation, library.Name);
            }
            catch
            {
                Assert.Fail();
            }

        }
    }
}
