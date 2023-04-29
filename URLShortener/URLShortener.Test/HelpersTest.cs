
namespace URLShortener.Test
{
    /// <summary>
    /// Test class for Helpers class
    /// </summary>
    [TestClass]
    public class HelpersTest
    {
        /// <summary>
        /// Should create guid with actual size.
        /// </summary>
        [TestMethod]
        public void Should_Create_Guid_With_Actual_Size_Test_Method()
        {
            var expected1 = Helpers.CreateGuid(5);
            var expected2 = Helpers.CreateGuid(5);

            // created guid is not null.
            Assert.IsNotNull(expected1);
            Assert.IsNotNull(expected2);

            // Length is equal the size.
            Assert.IsTrue(expected1.Length == 5);
            Assert.IsTrue(expected2.Length == 5);

            // should two expected are different
            Assert.IsTrue(expected1 != expected2);

        }

        /// <summary>
        /// Should generate string with actual size.
        /// </summary>
        [TestMethod]
        public void Should_Generate_String_With_Actual_Size_Test_Method()
        {
            var expected1 = Helpers.CreateUniqeStrig(5);
            var expected2 = Helpers.CreateUniqeStrig(5);

            // created string is not null.
            Assert.IsNotNull(expected1);
            Assert.IsNotNull(expected2);

            // Length is equal the size.
            Assert.IsTrue(expected1.Length == 5);
            Assert.IsTrue(expected2.Length == 5);

            // should two expected are different
            Assert.IsTrue(expected1 != expected2);
        }
    }
}
