using MyParser2.Grammar;
using MyParser2.Lexer;
using MyParser2.Parser;
using MyParser2.Test.CalcTwoNumbers.TreeNodes;
using System;

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

            // Por segurança: Aqui garantimos que a expressão contenha
            // sempre 3 elementos -> [NUM] [PLUS] [NUM]
            if (ExpressionElements == null || ExpressionElements.Length != 3)
            {
                throw new SyntaxAnalysisException("Invalid SUM expression elements");
            }

            var numberLeftElement = ExpressionElements[0];
            var plusElement = ExpressionElements[1];
            var numberRightElement = ExpressionElements[2];

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            if (input.EndOfStream())
            {
                return null;
            }

            var initialPos = input.GetPosition();

            // O primeiro token é só um marcador (etiqueta)
            var tag = input.Next();

            if (tag == null || (CalcTwoNumbersTokenClass)tag.Class != (CalcTwoNumbersTokenClass)GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            // Primeiro número
            var numberLeftToken = input.Next();

            if (numberLeftToken == null || (CalcTwoNumbersTokenClass)numberLeftToken.Class != (CalcTwoNumbersTokenClass)numberLeftElement.GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            // O segundo token também é só um marcador (etiqueta)
            var plusToken = input.Next();

            if (plusToken == null || (CalcTwoNumbersTokenClass)plusToken.Class != (CalcTwoNumbersTokenClass)plusElement.GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            // Segundo número
            var numberRightToken = input.Next();

            if (numberRightToken == null || (CalcTwoNumbersTokenClass)numberRightToken.Class != (CalcTwoNumbersTokenClass)numberRightElement.GetTokenClass())
            {
                input.SetPosition(initialPos);
                return null;
            }

            // Aqui já temos tudo que precisamos. Ou melhor, quase tudo.
            // Só precisamos garantir que os números sejam inteiros sem sinal válidos
            (uint numberLeft, uint numberRight) = EnsureNumbers(
                (string)numberLeftToken.Content,
                (string)numberRightToken.Content
            );

            var expressionNode = new SyntaxTreeNode();
            var sumOperatorNode = new SumOperatorTreeNode();

            // Number left tree node
            sumOperatorNode.AddChildNode(new UIntegerTreeNode(
                numberLeft
            ));

            // Number right tree node
            sumOperatorNode.AddChildNode(new UIntegerTreeNode(
                numberRight
            ));

            expressionNode.AddChildNode(sumOperatorNode);

            return expressionNode;
        }

        private (uint, uint) EnsureNumbers(string numberLeft, string numberRight)
        {
            return (
                uint.Parse(numberLeft),
                uint.Parse(numberRight)
            );
        }
    }
}
