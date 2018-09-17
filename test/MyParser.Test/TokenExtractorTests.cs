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

        [Fact(DisplayName = "Conte�do VAZIO j� inicia no fim")]
        public void ConteudoVazio_JaInicia_NoFim()
        {
            var ext = TokenExtractor.FromString("");

            Assert.True(ext.EndOfCode);
        }

        [Fact(DisplayName = "Conte�do N�O VAZIO n�o inicia no fim")]
        public void ConteudoNaoVazio_NaoInicia_NoFim()
        {
            var ext = TokenExtractor.FromString(".");

            Assert.False(ext.EndOfCode);
        }

        [Fact(DisplayName = "RollbackCursor requer um cursor nos limites do c�digo")]
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

        [Fact(DisplayName = "RollbackCursor move a posi��o do c�digo quando for um cursor v�lido")]
        public void RollbackCursor_MovePara_QuandoCursorValido()
        {
            var ext = TokenExtractor.FromString("abc");

            // Inicia em 0
            var cursor1 = ext.SaveCursor();

            ext.NextChar();

            // 1 token � frente
            var cursor2 = ext.SaveCursor();

            ext.RollbackCursor(cursor1);

            // Volta ao in�cio
            var cursor3 = ext.SaveCursor();

            Assert.Equal(0, cursor1.Position);
            Assert.Equal(1, cursor2.Position);
            Assert.Equal(0, cursor3.Position);
        }

        [Fact(DisplayName = "SaveCursor retorna a posi��o atual no c�digo")]
        public void SaveCursor_Retorna_APosicaoAtualNoCodigo()
        {
            var ext = TokenExtractor.FromString("123");

            ext.NextChar();
            ext.NextChar();

            // Dois tokens � frente
            var cursor = ext.SaveCursor();

            Assert.Equal(2, cursor.Position);
        }

        [Fact(DisplayName = "NextChar retorna o token na posi��o atual")]
        public void NextChar_RetornaOToken_NaPosicao()
        {
            var ext = TokenExtractor.FromString("123");

            Assert.Equal('1', ext.NextChar());
            Assert.Equal('2', ext.NextChar());
            Assert.Equal('3', ext.NextChar());
        }

        [Fact(DisplayName = "EndOfCode indica quando est� no fim do c�digo")]
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
