namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class NumberGrammarElement : Grammar.CommonElements.NumberGrammarElement
    {
        public override object GetTokenClass()
        {
            return CalcTwoNumbersTokenClass.NUM;
        }
    }
}
