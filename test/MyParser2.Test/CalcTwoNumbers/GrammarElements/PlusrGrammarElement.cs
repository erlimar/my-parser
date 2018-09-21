namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class PlusrGrammarElement : Grammar.CommonElements.CharacterGrammarElement
    {
        public PlusrGrammarElement()
            : base('+')
        { }

        public override object MakeTokenClass()
        {
            return CalcTwoNumbersTokenClass.PLUS;
        }
    }
}
