using Moq;
using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Module", nameof(MyParser))]
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

        [Fact(DisplayName = "Requer [GrammarIgnoreDelegate] ao instanciar com construtor 2")]
        public void Requer_GrammarIgnoreDelegate_AoInstanciar()
        {
            var element = new Mock<GrammarElement>().Object;

            var ex = Assert.Throws<ArgumentNullException>(
                () => new Grammar(element, null)
            );

            Assert.Equal("ignoreDelegate", ex.ParamName);
        }
    }
}
