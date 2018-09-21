using MyParser2.Lexer;

namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class PlusrGrammarElement : Grammar.CommonElements.CharacterGrammarElement
    {
        public PlusrGrammarElement()
            : base('+')
        { }

        public override object GetTokenClass()
        {
            return CalcTwoNumbersTokenClass.PLUS;
        }
    }
}
