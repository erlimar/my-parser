using MyParser2.Lexer;
using MyParser2.Parser;
using MyParser2.Test.CalcTwoNumbers.TreeNodes;

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

        public override SyntaxTreeNode Make(ObjectStream<MyToken> input, MyDiscardDelegate<MyToken> discarder)
        {
            Ensure(input, discarder);

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            if (input.EndOfStream())
            {
                return null;
            }

            var initialPos = input.GetPosition();
            var token = input.Next();

            if (token == null || (CalcTwoNumbersTokenClass)token.Class != (CalcTwoNumbersTokenClass)GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            return new SumOperatorTreeNode();
        }
    }
}
