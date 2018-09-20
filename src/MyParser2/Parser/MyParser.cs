using MyParser2.Grammar;
using MyParser2.Lexer;
using System;

namespace MyParser2.Parser
{
    public class MyParser
    {
        private readonly MyGrammar _grammar;

        public MyParser(MyGrammar grammar)
        {
            _grammar = grammar
                ?? throw new ArgumentNullException(nameof(grammar));
        }

        /// <summary>
        /// Executa a análise léxica.
        /// </summary>
        /// <param name="input">Sequência de tokens de entrada</param>
        /// <returns>Árvore sintática abstrata</returns>
        /// <exception cref="SyntacticAnalysisException">Se houver algum erro durante a análise</exception>
        public MyAbstractSyntaxTree Run(ObjectStream<MyToken> input)
        {
            throw new NotImplementedException();
        }
    }
}
