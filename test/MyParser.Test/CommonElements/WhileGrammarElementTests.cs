using Moq;
using MyParser.CommonElements;
using System;
using System.Linq;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(WhileGrammarElement))]
    public class WhileGrammarElementTests
    {
        [Fact(DisplayName = "Requer GrammarElement ao instanciar")]
        public void Requer_Element_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new WhileGrammarElement(null)
            );

            Assert.Equal("element", ex.ParamName);
        }

        [Fact(DisplayName = "Requer [TokenExtractor] ao avaliar")]
        public void Requer_TokenExtractor_AoAvaliar()
        {
            var element = new Mock<GrammarElement>().Object;

            var ex = Assert.Throws<ArgumentNullException>(
                () => new WhileGrammarElement(element).Eval(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact(DisplayName = "Reconhece todos os tokens válidos")]
        public void Reconhece_TodosTokens_Validos()
        {
            var blanks = new OrListGrammarElement(new[] {
                new CharGrammarElement(' '),
                new CharGrammarElement('\t'),
            });

            var grammar = new WhileGrammarElement(blanks);
            var extractor = TokenExtractor.FromString("  \t  \t  \t  final!");

            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.IsType<Token[]>(token.Content);
            Assert.Equal(11, (token.Content as Token[]).Length);
            Assert.Equal("  \t  \t  \t  ", (token.Content as Token[]).Aggregate("", (current, tk) =>
            {
                return current + tk.Content.ToString();
            }));
        }
    }
}
