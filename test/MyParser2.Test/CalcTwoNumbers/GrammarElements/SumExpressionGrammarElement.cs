using MyParser2.Grammar;

namespace MyParser2.Test.CalcTwoNumbers.GrammarElements
{
    public class SumExpressionGrammarElement : Grammar.CommonElements.GroupAndGrammarElement
    {
        public SumExpressionGrammarElement()
            : base(ExpressionElements)
        { }

        private static MyGrammarElement[] ExpressionElements
        {
            get
            {
                return new MyGrammarElement[]
                {
                    new NumberGrammarElement(),
                    new PlusrGrammarElement(),
                    new NumberGrammarElement()
                };
            }
        }

        public override object MakeTokenClass()
        {
            return CalcTwoNumbersTokenClass.SUM_EXPRESSION;
        }
    }
}
