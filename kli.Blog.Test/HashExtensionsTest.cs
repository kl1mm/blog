using kli.Blog.Shared.Extensions;
using Xunit;

namespace kli.Blog.Test
{
    public class HashExtensionsTest
    {
        [Fact]
        public void TestSha256()
        {
            Assert.Equal("bwQW2AA96WeirxOpP0eq7e0oheS2kRaQ2Ew4S4Cm5W8=", "someString".Sha256());
        }
    }
}
