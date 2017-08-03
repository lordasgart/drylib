using System;
using System.Collections.Generic;
using Xunit;
using static DryLib.Sql.Arguments;

namespace DryLib.Sql.xUnitTests
{
    public class ArgumentsTest
    {
        private const string Column = "Id";

        [Fact]
        public void TestStandardIn()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<int> {1, 2, 3, 4, 5};

            var cmdText = In(Column, list);

            Assert.True(cmdText.Contains("OR"));
            Assert.False(cmdText.Contains("'"));
        }

        [Fact]
        public void TestStandardInWithString()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<string> { "A", "B", "C", "D", "E" };

            var cmdText = In(Column, list);

            Assert.True(cmdText.Contains("OR"));

            Assert.True(cmdText.Contains("'"));
        }

        [Fact]
        public void TestStandardInWithGuid()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<Guid> { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };

            var cmdText = In(Column, list);

            Assert.True(cmdText.Contains("OR"));

            Assert.True(cmdText.Contains("'"));
        }

        [Fact]
        public void TestStandardWhere()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<int> { 1, 2, 3, 4, 5 };

            var cmdText = Where(Column, list);

            Assert.True(cmdText.Contains("WHERE"));
        }

        [Fact]
        public void TestStandardAnd()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<int> { 1, 2, 3, 4, 5 };

            var cmdText = And(Column, list);

            Assert.True(cmdText.Contains("AND"));
        }

        [Fact]
        public void MaxParamCountForSingleInOperationShouldThrowExceptionWhenLessThenOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                Settings.MaxParamCountForSingleInOperation = 0;
            });
        }

        [Fact]
        public void TestSingleListShouldNotContainSecondInSeparatedWithOr()
        {
            Settings.MaxParamCountForSingleInOperation = 2;

            var list = new List<int> { 1, 2 };

            var cmdText = In(Column, list);

            Assert.True(!cmdText.Contains("OR"));
        }

        [Fact]
        public void TestOutputWithOneValueOnly()
        {
            Settings.MaxParamCountForSingleInOperation = 10;

            var list = new List<int> { 1 };

            var cmdText = In(Column, list);

            var doesNotContainOrArgument = !cmdText.Contains("OR");
            Assert.True(doesNotContainOrArgument);

            var doesNotContainCommaSeparator = !cmdText.Contains(",");
            Assert.True(doesNotContainCommaSeparator);
        }

        [Fact]
        public void OutputWithNoValueShouldBeStringEmpty()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => { In(Column, new List<int>()); });
        }


    }
}
