using System;
using Xunit;

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
            var rootElement = new GrammarElementFake();
            var grammar = new Grammar(rootElement);
            var parser = new Parser(grammar);

            var ex = Assert.Throws<ArgumentNullException>(
                () => parser.Parse(null)
            );

            Assert.Equal("extractor", ex.ParamName);
        }

        [Fact]
        public void Parse_Retorna_ArvoreSintaticaInvalida_ParaVazio()
        {
            var rootElement = new GrammarElementFake();
            var grammar = new Grammar(rootElement);
            var parser = new Parser(grammar);
            var extractor = new TokenExtractor(string.Empty);
            var tree = parser.Parse(extractor);

            Assert.NotNull(tree);
            Assert.False(tree.IsValid);
        }

        [Fact]
        public void Parse_Retorna_ArvoreSintaticaInvalidaSemTokenRoot_ParaVazio()
        {
            var rootElement = new GrammarElementFake();
            var grammar = new Grammar(rootElement);
            var parser = new Parser(grammar);
            var extractor = new TokenExtractor(string.Empty);
            var tree = parser.Parse(extractor);

            Assert.NotNull(tree);
            Assert.False(tree.IsValid);
            Assert.Null(tree.TokenRoot);
        }
    }

    internal class GrammarElementFake
        : GrammarElement
    { }
}
