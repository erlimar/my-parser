# my-parser
Parser de qualquer coisa

![](lexer-parser-sample.png)
-----------------------------------------------------------------
Exemplo de uso
```csharp
class CalcSample
{
    void Main()
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

        Assert.Equal(1171, result);
    }
}
```

Definição da gramática:
```csharp
public class CalcTwoNumbersGrammar : MyGrammar
{
    public CalcTwoNumbersGrammar()
    {
        RootElement = new GrammarElements.SumExpressionGrammarElement();
        OnLexerDiscard = DoLexerDiscard;
    }

    private bool DoLexerDiscard(Char element)
    {
        var ignorableChars = new char[]
        {
            '\n',
            '\r',
            '\t',
            ' '
        };

        return ignorableChars.Contains(element);
    }
}
```
-----------------------------------------------------------------
* https://pt.wikibooks.org/wiki/Constru%C3%A7%C3%A3o_de_compiladores
* https://johnidm.gitbooks.io/compiladores-para-humanos/
