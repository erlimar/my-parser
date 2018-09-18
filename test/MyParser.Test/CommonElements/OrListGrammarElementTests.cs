using MyParser.CommonElements;
using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(OrListGrammarElement))]
    public class OrListGrammarElementTests
    {
        [Fact(DisplayName = "Requer GrammarElement[] ao instanciar")]
        public void Requer_Lista_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new OrListGrammarElement(null)
            );

            Assert.Equal("list", ex.ParamName);
        }

        [Fact(DisplayName = "GrammarElement[] não pode ser vazio")]
        public void Lista_NaoPodeSerVazia()
        {
            var list = new GrammarElement[] { };

            var ex = Assert.Throws<ArgumentException>(
                () => new OrListGrammarElement(list)
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
                () => new OrListGrammarElement(list).Eval(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Theory(DisplayName = "Reconhece uma lista válida ao avaliar")]
        [InlineData("abc")]
        [InlineData("bc1")]
        [InlineData("c01")]
        public void Reconhece_ListaValida_AoAvaliar(string text)
        {
            var list = new[]
            {
                new CharGrammarElement('a'),
                new CharGrammarElement('b'),
                new CharGrammarElement('c')
            };

            var grammar = new OrListGrammarElement(list);
            var extractor = TokenExtractor.FromString(text);
            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.IsType<char>(token.Content);
            Assert.Contains((char)token.Content, new char[] { 'a', 'b', 'c' });
        }

        [Theory(DisplayName = "Reconhece uma lista inválida ao avaliar")]
        [InlineData("abc")]
        [InlineData("bc1")]
        [InlineData("c01")]
        public void Reconhece_ListaInvalida_AoAvaliar(string text)
        {
            var list = new[]
            {
                new CharGrammarElement('1'),
                new CharGrammarElement('2'),
                new CharGrammarElement('3')
            };

            var grammar = new OrListGrammarElement(list);
            var extractor = TokenExtractor.FromString(text);
            var token = grammar.Eval(extractor);

            Assert.Null(token);
        }
    }
}
