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
        var inputCode = "437 + 734";

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

Defini��o da gram�tica:
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

Classes gramaticais:
```csharp
public enum CalcTwoNumbersTokenClass
{
    SUM_EXPRESSION,
    NUM,
    PLUS
}
```

Elementos gramaticais:
```csharp
public class SumExpressionGrammarElement : Grammar.CommonElements.GroupAndGrammarElement
{
    private static MyGrammarElement[] ExpressionElements = new MyGrammarElement[]
    {
        new NumberGrammarElement(),
        new PlusGrammarElement(),
        new NumberGrammarElement()
    };

    public SumExpressionGrammarElement()
        : base(ExpressionElements)
    { }

    public override object GetTokenClass()
    {
        return CalcTwoNumbersTokenClass.SUM_EXPRESSION;
    }

    public override SyntaxTreeNode Make(ObjectStream<MyToken> input, MyDiscardDelegate<MyToken> discarder)
    {
        /* C�digo de constru��o do n� sint�tico */
    }
}

public class NumberGrammarElement : Grammar.CommonElements.NumberGrammarElement
{
    public override object GetTokenClass()
    {
        return CalcTwoNumbersTokenClass.NUM;
    }
}

public class PlusGrammarElement : Grammar.CommonElements.CharacterGrammarElement
{
    public PlusGrammarElement()
        : base('+')
    { }

    public override object GetTokenClass()
    {
        return CalcTwoNumbersTokenClass.PLUS;
    }
}
```
-----------------------------------------------------------------
* https://pt.wikibooks.org/wiki/Constru%C3%A7%C3%A3o_de_compiladores
* https://johnidm.gitbooks.io/compiladores-para-humanos/
