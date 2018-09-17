using System;
using Xunit;
using Moq;

namespace MyParser.Test
{
    [Trait("Target", nameof(Parser))]
    public class ParserTests
    {
        [Fact(DisplayName = "Requer [Grammar] ao instanciar")]
        public void Requer_Gramatica_AoInstanciar()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new Parser(null)
            );

            Assert.Equal("grammar", ex.ParamName);
        }

        [Fact(DisplayName = "Parse requer [SymbolExtractor]")]
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

        [Fact(DisplayName = "Parse retorna [SyntaxTree] inválida para código vazio")]
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

        [Fact(DisplayName = "Parse retorna [SyntaxTree] sem TokenRoot para código vazio")]
        public void Parse_Retorna_ArvoreSintaticaInvalidaSemTokenRoot_ParaVazio()
        {
            var grammarElement = new Mock<GrammarElement>().Object;
            var grammar = new Grammar(grammarElement);
            var extractor = TokenExtractor.FromString("");
            var parser = new Parser(grammar);

            var tree = parser.Parse(extractor);

            Assert.NotNull(tree);
            Assert.False(tree.IsValid);
            Assert.Null(tree.RootNode);
        }

        [Fact(DisplayName = "Para código VAZIO, árvore sintática é inválida mesmo se gramática não informar")]
        public void ParaCodigoVazio_ArvoreEhInvalida_MesmoSeGramaticaNaoDisser()
        {
            var grammarElementMock = new Mock<GrammarElement>();
            var grammar = new Grammar(grammarElementMock.Object);
            var extractor = TokenExtractor.FromString("");
            var parser = new Parser(grammar);

            var tree = parser.Parse(extractor);

            Assert.False(tree.IsValid);
            Assert.True(extractor.EndOfCode);
        }

        [Fact(DisplayName = "Árvore sintática é inválida se gramática for válida mas todo o código não for consumido")]
        public void ArvoreSintaticaEhInvalida_SeTodoCodigo_NaoForConsumido()
        {
            var grammarElementMock = new Mock<GrammarElement>();
            var grammar = new Grammar(grammarElementMock.Object);
            var extractor = TokenExtractor.FromString("a");
            var parser = new Parser(grammar);

            // Uma gramática é válida se retornar um [Token] não nulo
            grammarElementMock
                .Setup(el => el.Eval(It.IsAny<TokenExtractor>()))
                .Returns(new Token());

            var tree = parser.Parse(extractor);

            Assert.False(tree.IsValid);
            Assert.False(extractor.EndOfCode);
        }

        [Fact(DisplayName = "Árvore sintática é VÁLIDA se gramática for válida e todo o código for consumido")]
        public void ArvoreSintaticaEhValida_SeTodoCodigo_ForConsumido()
        {
            var grammarElementMock = new Mock<GrammarElement>();
            var grammar = new Grammar(grammarElementMock.Object);
            var extractor = TokenExtractor.FromString("a");
            var parser = new Parser(grammar);

            // Uma gramática é válida se retornar um [Token] não nulo
            grammarElementMock
                .Setup(el => el.Eval(It.IsAny<TokenExtractor>()))
                .Returns(new Token())
                .Callback<TokenExtractor>((tk) =>
                {
                    // Consumindo o código
                    var c = tk.NextChar();
                });

            var tree = parser.Parse(extractor);

            Assert.True(tree.IsValid);
            Assert.True(extractor.EndOfCode);
        }
    }
}
