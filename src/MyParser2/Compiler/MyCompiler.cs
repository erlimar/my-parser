using MyParser2.Grammar;
using MyParser2.Lexer;
using MyParser2.Parser;

namespace MyParser2.Compiler
{
    public class MyCompiler<T>
    {
        /// <summary>
        /// Compila um código qualquer em um objeto final com base em uma gramática
        /// </summary>
        /// <param name="grammar">Definição da gramática</param>
        /// <param name="code">Código de entrada</param>
        /// <param name="compiler">Compilador (emissor) do objeto final</param>
        /// <returns>Objeto compilado</returns>
        public static T Compile(
            MyGrammar grammar,
            string code,
            MyEmitterDelegate<T> compiler)
        {
            var scanner = new MyScanner(grammar);
            var parser = new MyParser(grammar);
            var codeStream = new CodeStringStream(code);

            // Executa a análise léxica
            var tokens = scanner.Run(codeStream);

            // Executa a análise sintática
            var syntaxTree = parser.Run(tokens);

            // Emite (compila) o objeto compilado final
            return compiler(syntaxTree);
        }
    }
}
