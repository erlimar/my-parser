using System;
using System.Linq;
using Xunit;
using Moq;
using MyParser.Test.Utils;
using MyParser.CommonElements;

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
                .Returns(new Token(null));

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
                .Returns(new Token(null))
                .Callback<TokenExtractor>((tk) =>
                {
                    // Consumindo o código
                    var c = tk.NextChar();
                });

            var tree = parser.Parse(extractor);

            Assert.True(tree.IsValid);
            Assert.True(extractor.EndOfCode);
        }

        [Fact(DisplayName = "Desconsidera caracteres conforme IgnoreDelegate")]
        public void DesconsideraCaracteres_Conforme_IgnoreDelegate()
        {
            /*
             * A IDEIA BASICA SOBRE IGNORAR:
             * -----------------------------
             * > O token é que determina se vai ou não ignorar
             *   algo antes iniciar o consumo de seus caracteres.
             * 
             * > Pra isso, além de receber o TokenExtractor, ele
             *   precisa receber o [ignorer]
             *   
             * > Normalmente o ignorar é feito no início do processo,
             *   logo antes de consumir o primeiro [char]
             *   
             *   # Imagine um "ignorar espaços vazios [espaço, tab, nova linha, etc],
             *   # Agora imagine os tokens de:
             *      - Comentário de única linha -> // comentário aqui
             *      - Comentário de várias linhas -> (* várias linhas *)
             *      - Strings -> "uma   string   qualquer"
             *   # Esses devem ignorar os caracteres iniciais, mas uma vez
             *     que estão dentro de seu escopo, devem considerar os "espaços vazios"
             *     
             * > Imaginamos que o elemento [CharGrammarElement] não faz mais
             *   sentido aqui. Talvez um [WordGrammarElement] faça mais sentido.
             *   Se será uma palavra de um único caractere ele é que determina
             */

            bool ignore(char c) => new[] { ' ', '*', '-' }.Contains(c);

            var ABRoot = new AndListGrammarElement(new[] {
                new CharGrammarElement('A'),
                new CharGrammarElement('B')
            });

            var grammar = new Grammar(ABRoot, ignore);
            var parser = new Parser(grammar);

            var code = TokenExtractor.FromString(
                "- - * A - - --- ******  B **    -----"
            );

            var tree = parser.Parse(code);

            Assert.NotNull(tree);
            Assert.NotNull(tree.RootNode);
            Assert.NotNull(tree.RootNode.Token);
            Assert.NotNull(tree.RootNode.Token.Content);
            Assert.True(tree.IsValid);
            Assert.IsType<Token[]>(tree.RootNode.Token.Content);
            Assert.Equal(2, (tree.RootNode.Token.Content as Token[]).Length);
            Assert.Equal("A", ((tree.RootNode.Token.Content as Token[])[0] as Token).Content.ToString());
            Assert.Equal("B", ((tree.RootNode.Token.Content as Token[])[1] as Token).Content.ToString());
        }

        [Fact(DisplayName = "Avalia corretamente uma gramática simples")]
        public void AvaliaCorretamente_UmaGramaticaSimples()
        {
            var grammar = new SampleGrammar1();
            var parser = new Parser(grammar);

            var extractor1 = TokenExtractor.FromString(SampleGrammar1.SampleCode1);
            var tree1 = parser.Parse(extractor1);

            SampleGrammar1.AssertSampleCode1(tree1);
        }
    }
}
