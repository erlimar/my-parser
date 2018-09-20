using MyParser2.Grammar;
using MyParser2.Grammar.CommonElements;
using System;

namespace MyParser2.Test.CalcTwoNumbers
{
    public class CalcTwoNumbersGrammar : MyGrammar
    {
        public CalcTwoNumbersGrammar()
        {
            RootElement = new NumberGrammarElement();
            OnLexerDiscard = DoLexerDiscard;
        }

        private bool DoLexerDiscard(Char element)
        {
            return false;
        }

    }
}
