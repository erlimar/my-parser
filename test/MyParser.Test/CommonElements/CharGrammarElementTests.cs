using MyParser.CommonElements;
using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Module", nameof(MyParser))]
    [Trait("Target", nameof(CharGrammarElement))]
    public class CharGrammarElementTests
    {
        [Fact(DisplayName = "Requer [TokenExtractor] ao avaliar")]
        public void Requer_TokenExtractor_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CharGrammarElement('\0').Eval(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact(DisplayName = "Reconhece um caractere inválido")]
        public void Reconhece_Caractere_Invalido()
        {
            var element = new CharGrammarElement('a');
            var extractor = TokenExtractor.FromString("A");
            var token = element.Eval(extractor);

            Assert.Null(token);
        }

        [Theory(DisplayName = "Reconhece um caractere válido")]
        [InlineData('a')]
        [InlineData('B')]
        [InlineData('c')]
        [InlineData('0')]
        [InlineData('\n')]
        [InlineData('\r')]
        [InlineData('\0')]
        public void Reconhece_Caractere_Valido(char validChar)
        {
            var element = new CharGrammarElement(validChar);
            var onCharString = new string(new[] { validChar });
            var extractor = TokenExtractor.FromString(onCharString);
            var token = element.Eval(extractor);

            Assert.NotNull(token);
            Assert.Equal(validChar, token.Content);
        }
    }
}
