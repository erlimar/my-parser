using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(TokenExtractor))]
    public class TokenExtractorTests
    {
        [Fact(DisplayName = "FromString() requer [String] ao instanciar")]
        public void Requer_String_AoInstanciar()
        {
            var ext = Assert.Throws<ArgumentNullException>(
                () => TokenExtractor.FromString(null)
            );

            Assert.Equal("code", ext.ParamName);
        }

        [Fact(DisplayName = "Conteúdo VAZIO já inicia no fim")]
        public void ConteudoVazio_JaInicia_NoFim()
        {
            var ext = TokenExtractor.FromString("");

            Assert.True(ext.EndOfCode);
        }

        [Fact(DisplayName = "Conteúdo NÃO VAZIO não inicia no fim")]
        public void ConteudoNaoVazio_NaoInicia_NoFim()
        {
            var ext = TokenExtractor.FromString(".");

            Assert.False(ext.EndOfCode);
        }

        [Fact(DisplayName = "RollbackCursor requer um cursor nos limites do código")]
        public void RollbackCursor_RequerCursorNoRange()
        {
            var ext = TokenExtractor.FromString("");
            var cursor = new TokenExtractorCursor
            {
                Position = 1
            };

            Assert.Throws<IndexOutOfRangeException>(
                () => ext.RollbackCursor(cursor)
            );
        }

        [Fact(DisplayName = "RollbackCursor move a posição do código quando for um cursor válido")]
        public void RollbackCursor_MovePara_QuandoCursorValido()
        {
            var ext = TokenExtractor.FromString("abc");

            // Inicia em 0
            var cursor1 = ext.SaveCursor();

            ext.NextChar();

            // 1 token à frente
            var cursor2 = ext.SaveCursor();

            ext.RollbackCursor(cursor1);

            // Volta ao início
            var cursor3 = ext.SaveCursor();

            Assert.Equal(0, cursor1.Position);
            Assert.Equal(1, cursor2.Position);
            Assert.Equal(0, cursor3.Position);
        }

        [Fact(DisplayName = "SaveCursor retorna a posição atual no código")]
        public void SaveCursor_Retorna_APosicaoAtualNoCodigo()
        {
            var ext = TokenExtractor.FromString("123");

            ext.NextChar();
            ext.NextChar();

            // Dois tokens à frente
            var cursor = ext.SaveCursor();

            Assert.Equal(2, cursor.Position);
        }

        [Fact(DisplayName = "NextChar retorna o token na posição atual")]
        public void NextChar_RetornaOToken_NaPosicao()
        {
            var ext = TokenExtractor.FromString("123");

            Assert.Equal('1', ext.NextChar());
            Assert.Equal('2', ext.NextChar());
            Assert.Equal('3', ext.NextChar());
        }

        [Fact(DisplayName = "EndOfCode indica quando está no fim do código")]
        public void EndOfCode_Indica_QuandoNoFim()
        {
            var ext = TokenExtractor.FromString("az");
            var noEOC = ext.EndOfCode;

            ext.NextChar();
            ext.NextChar();

            var yesEOC = ext.EndOfCode;

            Assert.False(noEOC);
            Assert.True(yesEOC);
        }
    }
}
