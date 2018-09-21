using MyParser2.Grammar;
using System;
using System.Linq;

namespace MyParser2.Lexer
{
    /// <summary>
    /// Um exemplo de analisador léxico
    /// </summary>
    public class MyScanner
    {
        private readonly MyGrammar _grammar;

        public MyScanner(MyGrammar grammar)
        {
            _grammar = grammar
                ?? throw new ArgumentNullException(nameof(grammar));
        }

        /// <summary>
        /// Executa a análise léxica.
        /// </summary>
        /// <param name="input">Sequência de caracteres de entrada</param>
        /// <returns>Sequência de tokens</returns>
        /// <exception cref="LexicalAnalysisException">Se houver algum erro durante a análise</exception>
        /// <exception cref="TokenNotFoundException">Se nenhum token for encontrado</exception>
        /// <exception cref="InputNotConsumedCompletelyException">Se a entrada não for completamente consumida</exception>
        public ObjectStream<MyToken> Run(ObjectStream<Char> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            MyScannerDiscardDelegate<Char> discarder = (c) => false;

            if (_grammar.OnLexerDiscard != null)
            {
                discarder = _grammar.OnLexerDiscard;
            }

            try
            {
                MyToken[] acquiredTokens = _grammar.RootElement.Eval(input, discarder);

                if (acquiredTokens == null || acquiredTokens.Count() < 1)
                {
                    // TODO: Informar [currentPos] ???
                    throw new TokenNotFoundException();
                }

                // Ignorando qualquer código descartável que tenha restado
                input.Discard(discarder);

                if (!input.EndOfStream())
                {
                    // TODO: Informar [currentPos] ???
                    throw new InputNotConsumedCompletelyException();
                }

                var output = new TokenStream();

                Array.ForEach(acquiredTokens, (token) => output.Push(token));

                return output;
            }
            catch (LexicalAnalysisException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new LexicalAnalysisException(ex);
            }
        }
    }
}
