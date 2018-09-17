using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(Grammar))]
    public class GrammarTests
    {
        [Fact(DisplayName = "Requer [GrammarElement] ao instanciar")]
        public void Requer_ElementoGramatical_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Grammar(null)
            );

            Assert.Equal("rootElement", ex.ParamName);
        }
    }
}
