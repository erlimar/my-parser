using MyParser2.Grammar;
using System;

namespace MyParser2.Lexer
{
    /// <summary>
    /// Um exemplo de analisador léxico
    /// </summary>
    public class MyLexer
    {
        private readonly MyGrammar _grammar;

        public MyLexer(MyGrammar grammar)
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
        public ObjectStream<MyToken> Run(ObjectStream<Char> input)
        {
            throw new NotImplementedException();
        }
    }
}
