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
        /// Executa a análise sintática.
        /// </summary>
        /// <param name="input">Sequência de tokens de entrada</param>
        /// <returns>Árvore sintática abstrata</returns>
        /// <exception cref="SyntaxAnalysisException">Se houver algum erro durante a análise</exception>
        /// <exception cref="TheNodeWasNotCreatedException">Se algum nó não puder ser construído</exception>
        /// <exception cref="InputNotConsumedCompletelyException">Se a entrada não for completamente consumida</exception>
        public MyAbstractSyntaxTree Run(ObjectStream<MyToken> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            MyDiscardDelegate<MyToken> discarder = (c) => false;

            if (_grammar.OnParserDiscard != null)
            {
                discarder = _grammar.OnParserDiscard;
            }

            try
            {
                SyntaxTreeNode rootNode = _grammar.RootElement.Make(input, discarder);

                if (rootNode == null)
                {
                    throw new TheNodeWasNotCreatedException();
                }

                // Ignorando qualquer código descartável que tenha restado
                input.Discard(discarder);

                if (!input.EndOfStream())
                {
                    throw new InputNotConsumedCompletelyException();
                }

                return new MyAbstractSyntaxTree(rootNode);
            }
            catch (SyntaxAnalysisException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new SyntaxAnalysisException(ex);
            }
        }
    }
}
