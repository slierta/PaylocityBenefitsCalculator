using System;
using Api.Extensions;
using FluentAssertions;
using Xunit;

namespace ApiTests.UnitTests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void UserShouldBeFiftyYearsOld()
        {
            var birthDate = new DateTime(1950, 1, 1);
            var whenDate = new DateTime(2000, 1, 2);
            var result = birthDate.Age(whenDate);
            result.Should().Be(50);
        }

        [Fact]
        public void UserShouldBe49YearsOld()
        {
            var birthDate = new DateTime(1950, 1, 1);
            var whenDate = new DateTime(2000, 1, 1);
            var result = birthDate.Age(whenDate);
            result.Should().Be(49);
        }
    }
}