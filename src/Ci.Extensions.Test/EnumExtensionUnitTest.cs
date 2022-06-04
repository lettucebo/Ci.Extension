using Ci.Extension.Core;
using Ci.Extensions.Test.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ci.Extensions.Test
{
    [TestClass]
    public class EnumExtensionUnitTest
    {
        [TestMethod]
        public void Get_Enum_From_Description()
        {
            var description = "One";

            var actual = description.GetEnumFromDescription<TestEnum>();

            actual.Should().Be(TestEnum.First);
        }

        [TestMethod]
        public void Get_Enum_From_Description_Should_Null()
        {
            var description = "One1";

            var actual = description.GetEnumFromDescription<TestEnum>();

            actual.Should().Be(default);
        }

        [TestMethod]
        public void Get_Enum_From_DisplayName()
        {
            var description = "Two";

            var actual = description.GetEnumFromDisplayName<TestEnum>();

            actual.Should().Be(default);
        }

        [TestMethod]
        public void Get_Enum_From_DisplayName_Should_Null()
        {
            var description = "Two2";

            var actual = description.GetEnumFromDisplayName<TestEnum>();

            actual.Should().Be(TestEnum.Second);
        }
    }
}