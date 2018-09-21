using MyParser2.Lexer;
using MyParser2.Parser;
using MyParser2.Test.CalcTwoNumbers.TreeNodes;

namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class NumberGrammarElement : Grammar.CommonElements.NumberGrammarElement
    {
        public override object GetTokenClass()
        {
            return CalcTwoNumbersTokenClass.NUM;
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

            if (string.IsNullOrEmpty((string)token.Content))
            {
                throw new SyntaxAnalysisException("Invalid content for NUMBER element");
            }

            uint number = 0;

            if (!uint.TryParse((string)token.Content, out number))
            {
                throw new SyntaxAnalysisException("Invalid number value for NUMBER element");
            }

            return new UIntegerTreeNode(number);
        }
    }
}
