using MyParser2.Compiler;
using MyParser2.Test.CalcTwoNumbers;
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
\t\t                         + 
                            734   ";

            var compiler = new MyCompiler<CalcTwoNumbersObject>(
                grammar,
                CalcTwoNumbersObject.Emitter
            );

            var calc = compiler.Compile(inputCode);
            var result = calc.CalcNumbers();

            Assert.Equal(1171, result);
        }
    }
}
