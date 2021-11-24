using FluentAssertions;
using Xunit;

namespace WebApi.UnitTests
{
    public class DemoUnitTest
    {
        [Fact]
        public void FirstTest_WithoutFluent()
        {
            string hello = "hello world";
            Assert.Equal("hello world", hello);
            Assert.StartsWith("hel", hello);
            Assert.EndsWith("orld", hello);
        }

        [Fact]
        public void FirstTest_WithFluent()
        {
            string hello = "hello world";
            hello.Should().Be("hello world");
            hello.Should().StartWith("hel");
            hello.Should().EndWith("orld");
            //....
            //or 
            hello.Should().Be("hello world").And.StartWith("hel").And.EndWith("orld");
            hello.Should().Be("hello world")
                .And.StartWith("hel")
                .And.EndWith("orld");
        }
    }
}