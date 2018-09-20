using MyParser2.Grammar;
using MyParser2.Lexer;
using MyParser2.Parser;
using System;

namespace MyParser2.Compiler
{
    public class MyCompiler<T>
    {
        private readonly MyGrammar _grammar;
        private readonly MyEmitterDelegate<T> _emitter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="grammar">Definição da gramática</param>
        /// <param name="emitter">Compilador (emissor) do objeto final</param>
        public MyCompiler(MyGrammar grammar, MyEmitterDelegate<T> emitter)
        {
            _grammar = grammar
                ?? throw new ArgumentNullException(nameof(grammar));
            _emitter = emitter
                ?? throw new ArgumentNullException(nameof(emitter));
        }

        /// <summary>
        /// Compila um código qualquer em um objeto final com base na gramática
        /// </summary>
        /// <param name="code">Código de entrada</param>
        /// <returns>Objeto compilado</returns>
        public T Compile(string code)
        {
            var scanner = new MyScanner(_grammar);
            var parser = new MyParser(_grammar);
            var input = new CodeStringStream(code);

            // Executa a análise léxica
            var tokens = scanner.Run(input);

            // Executa a análise sintática
            var syntaxTree = parser.Run(tokens);

            // Emite (compila) o objeto compilado final
            var output = _emitter(syntaxTree);

            return output;
        }
    }
}
