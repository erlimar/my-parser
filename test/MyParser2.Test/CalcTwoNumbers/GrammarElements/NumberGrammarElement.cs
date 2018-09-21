namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class NumberGrammarElement : Grammar.CommonElements.NumberGrammarElement
    {
        public override object MakeTokenClass()
        {
            return CalcTwoNumbersTokenClass.NUM;
        }
    }
}
