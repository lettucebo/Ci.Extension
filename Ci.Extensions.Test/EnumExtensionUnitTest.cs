using Ci.Extension;
using Ci.Extensions.Test.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ci.Extensions.Test
{
    [TestClass]
    public class EnumExtensionUnitTest
    {
        [TestMethod]
        public void Get_Enum_From_DisplayName()
        {
            var description = "One";

            var actual = description.GetEnumFromDescription(typeof(TestEnum));

            actual.Should().Be(TestEnum.First);
        }

        [TestMethod]
        public void Get_Enum_From_DisplayName_Should_Null()
        {
            var description = "One1";

            var actual = description.GetEnumFromDescription(typeof(TestEnum));

            actual.Should().Be(null);
        }
    }
}