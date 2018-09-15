using System;
using Xunit;

namespace MyParser.Test
{
    public class GrammarTests
    {
        [Fact]
        public void Requer_ElementoGramatical_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Grammar(null)
            );

            Assert.Equal("rootElement", ex.ParamName);
        }
    }
}
