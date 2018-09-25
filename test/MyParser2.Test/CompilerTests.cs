using MyParser2.Compiler;
using MyParser2.Test.CalcTwoNumbers;
using MyParser2.Test.JavaScriptHalf;
using Xunit;

namespace MyParser2.Test
{
    [Trait("Module", nameof(MyParser2))]
    [Trait("Target", nameof(MyCompiler<CalcTwoNumbersObject>))]
    public class CompilerTests
    {
        [Fact(DisplayName = "Resolve cenário básico")]
        public void Resolve_Cenario_Basico()
        {
            var grammar = new CalcTwoNumbersGrammar();
            var inputCode = @"
                            437
                             + 
                            734   ";

            var compiler = new MyCompiler<CalcTwoNumbersObject>(
                grammar,
                CalcTwoNumbersObject.Emitter
            );

            var calc = compiler.Compile(inputCode);
            var result = calc.CalcNumbers();

            Assert.Equal((uint)1171, result);
        }

        [Fact(DisplayName = "Resolve cenário menos básico")]
        public void Resolve_Cenario_MenosBasico()
        {
            var grammar = new JavaScriptHalfGrammar();
            var inputCode = @"
                // VAR_DEFINITION
                var varName;

                // VAR ASSIGNMENT
                varName = 0;

                // VAR_DEFINITION_AND_ASSIGNMENT
                var varName = 0;

                // FUNCTION_DEFINITION
                function funcName() {
                }

                // FUNCTION_CALL
                funcName();
            ";

            var compiler = new MyCompiler<JavaScriptHalfObject>(
                grammar,
                JavaScriptHalfObject.Emitter
            );

            var js = compiler.Compile(inputCode);

            Assert.NotNull(js);
        }
    }
}
