using MyParser.CommonElements;
using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Module", nameof(MyParser))]
    [Trait("Target", nameof(AndListGrammarElement))]
    public class AndListGrammarElementTests
    {
        [Fact(DisplayName = "Requer GrammarElement[] ao instanciar")]
        public void Requer_Lista_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new AndListGrammarElement(null)
            );

            Assert.Equal("list", ex.ParamName);
        }

        [Fact(DisplayName = "GrammarElement[] não pode ser vazio")]
        public void Lista_NaoPodeSerVazia()
        {
            var list = new GrammarElement[] { };

            var ex = Assert.Throws<ArgumentException>(
                () => new AndListGrammarElement(list)
            );

            Assert.Equal("list", ex.ParamName);
            Assert.Equal(
                "Argument list can not be empty",
                ex.Message.Split(new char[] { '\r', '\n' })[0]
            );
        }

        [Fact(DisplayName = "Requer [TokenExtractor] ao avaliar")]
        public void Requer_TokenExtractor_AoAvaliar()
        {
            var list = new[]
            {
                new CharGrammarElement('\0')
            };

            var ex = Assert.Throws<ArgumentNullException>(
                () => new AndListGrammarElement(list).Eval(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact(DisplayName = "Reconhece uma lista válida ao avaliar")]
        public void Reconhece_ListaValida_AoAvaliar()
        {
            var list = new[]
            {
                new CharGrammarElement('a'),
                new CharGrammarElement('b'),
                new CharGrammarElement('c')
            };

            var grammar = new AndListGrammarElement(list);
            var extractor = TokenExtractor.FromString("abc");
            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.IsType<Token[]>(token.Content);
            Assert.Equal(3, (token.Content as Token[]).Length);
            Assert.Equal("a", (token.Content as Token[])[0].Content.ToString());
            Assert.Equal("b", (token.Content as Token[])[1].Content.ToString());
            Assert.Equal("c", (token.Content as Token[])[2].Content.ToString());
        }

        [Fact(DisplayName = "Consome só os caracteres necessários do extrator")]
        public void Consome_SoOsCaracteres_Necessarios()
        {
            var list = new[]
            {
                new CharGrammarElement('e'),
                new CharGrammarElement('f'),
                new CharGrammarElement('g')
            };

            var grammar = new AndListGrammarElement(list);
            var extractor = TokenExtractor.FromString("efghhh");
            var posBegin = extractor.SaveCursor();
            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.Equal(0, posBegin.Position);
            Assert.Equal(3, extractor.SaveCursor().Position);

            Assert.IsType<Token[]>(token.Content);
            Assert.Equal(3, (token.Content as Token[]).Length);
            Assert.Equal("e", (token.Content as Token[])[0].Content.ToString());
            Assert.Equal("f", (token.Content as Token[])[1].Content.ToString());
            Assert.Equal("g", (token.Content as Token[])[2].Content.ToString());
        }
    }
}
