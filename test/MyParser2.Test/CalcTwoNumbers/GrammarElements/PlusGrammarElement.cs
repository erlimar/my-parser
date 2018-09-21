using MyParser2.Lexer;

namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
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
}
