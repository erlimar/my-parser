using MyParser.CommonElements;
using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(CharListGrammarElement))]
    public class CharListGrammarElementTests
    {
        [Fact(DisplayName = "Requer CharGrammarElement[] ao instanciar")]
        public void Requer_ListaChar_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new CharListGrammarElement(null)
            );

            Assert.Equal("charList", ex.ParamName);
        }

        [Fact(DisplayName = "CharGrammarElement[] Não pode ser vazio")]
        public void ListaChar_NaoPodeSerVazia()
        {
            var charList = new CharGrammarElement[] { };

            var ex = Assert.Throws<ArgumentException>(
                () => new CharListGrammarElement(charList)
            );

            Assert.Equal("charList", ex.ParamName);
            Assert.Equal(
                "Argument charList can not be empty",
                ex.Message.Split(new char[] { '\r', '\n' })[0]
            );
        }

        [Fact(DisplayName = "Requer [TokenExtractor] ao avaliar")]
        public void Requer_TokenExtractor_AoAvaliar()
        {
            var charList = new[]
            {
                new CharGrammarElement('\0')
            };

            var ex = Assert.Throws<ArgumentNullException>(
                () => new CharListGrammarElement(charList).Eval(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact(DisplayName = "Reconhece uma lista válida ao avaliar")]
        public void Reconhece_ListaValida_AoAvaliar()
        {
            var charList = new[]
            {
                new CharGrammarElement('h'),
                new CharGrammarElement('h'),
                new CharGrammarElement('h')
            };

            var grammar = new CharListGrammarElement(charList);
            var extractor = TokenExtractor.FromString("hhh");
            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.Equal("hhh", (string)token.Content);
        }

        [Fact(DisplayName = "Consome só os caracteres necessários do extrator")]
        public void Consome_SoOsCaracteres_Necessarios()
        {
            var charList = new[]
            {
                new CharGrammarElement('h'),
                new CharGrammarElement('h'),
                new CharGrammarElement('h')
            };

            var grammar = new CharListGrammarElement(charList);
            var extractor = TokenExtractor.FromString("hhhhhh");
            var posBegin = extractor.SaveCursor();
            var token = grammar.Eval(extractor);

            Assert.NotNull(token);
            Assert.Equal("hhh", (string)token.Content);
            Assert.Equal(0, posBegin.Position);
            Assert.Equal(3, extractor.SaveCursor().Position);
        }
    }
}
