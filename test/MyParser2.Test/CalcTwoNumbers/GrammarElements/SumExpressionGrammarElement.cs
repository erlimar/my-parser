using MyParser2.Grammar;
using MyParser2.Lexer;
using MyParser2.Parser;

namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
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
            Ensure(input, discarder);

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            if (input.EndOfStream())
            {
                return null;
            }

            var initialPos = input.GetPosition();

            // O primeiro token é só um marcador (etiqueta) pra indicar
            // um elemento válido
            var tag = input.Next();

            if (tag == null || (CalcTwoNumbersTokenClass)tag.Class != (CalcTwoNumbersTokenClass)GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            var numberLeftNode = ExpressionElements[0].Make(input, discarder);
            var plusNode = ExpressionElements[1].Make(input, discarder);
            var numberRightNode = ExpressionElements[2].Make(input, discarder);

            if (numberLeftNode == null || plusNode == null || numberRightNode == null)
            {
                input.SetPosition(initialPos);
                return null;
            }

            var exprTreeNode = new SyntaxTreeNode();

            plusNode.AddChildNode(numberLeftNode);
            plusNode.AddChildNode(numberRightNode);

            exprTreeNode.AddChildNode(plusNode);

            return exprTreeNode;
        }
    }
}
