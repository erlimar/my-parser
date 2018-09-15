using System;
using Xunit;

namespace MyParser.Test
{
    public class TokenExtractorTests
    {
        [Fact]
        public void Requer_String_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new TokenExtractor(null)
            );

            Assert.Equal("code", ex.ParamName);
        }
    }
}
