using System;
using Xunit;
using Moq;

namespace MyParser.Test
{
    public class ParserTests
    {
        [Fact]
        public void Requer_Gramatica_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Parser(null)
            );

            Assert.Equal("grammar", ex.ParamName);
        }

        [Fact]
        public void ParseRequer_SymbolExtractor()
        {
            var grammarElement = new Mock<GrammarElement>().Object;
            var grammar = new Grammar(grammarElement);
            var parser = new Parser(grammar);

            var ex = Assert.Throws<ArgumentNullException>(
                () => parser.Parse(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact]
        public void Parse_Retorna_ArvoreSintaticaInvalida_ParaCodigoVazio()
        {
            var grammarElement = new Mock<GrammarElement>().Object;
            var grammar = new Grammar(grammarElement);
            var extractor = TokenExtractor.FromString("");
            var parser = new Parser(grammar);

            var tree = parser.Parse(extractor);

            Assert.NotNull(tree);
            Assert.False(tree.IsValid);
        }

        [Fact]
        public void Parse_Retorna_ArvoreSintaticaInvalidaSemTokenRoot_ParaVazio()
        {
            var grammarElement = new Mock<GrammarElement>().Object;
            var grammar = new Grammar(grammarElement);
            var extractor = TokenExtractor.FromString("");
            var parser = new Parser(grammar);

            var tree = parser.Parse(extractor);

            Assert.NotNull(tree);
            Assert.False(tree.IsValid);
            Assert.Null(tree.TokenRoot);
        }
    }
}
