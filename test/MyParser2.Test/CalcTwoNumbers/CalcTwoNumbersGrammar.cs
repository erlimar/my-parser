using MyParser2.Grammar;
using System;
using System.Linq;

namespace MyParser2.Test.CalcTwoNumbers
{
    public class CalcTwoNumbersGrammar : MyGrammar
    {
        public CalcTwoNumbersGrammar()
        {
            RootElement = new GrammarElements.NumberGrammarElement();
            OnLexerDiscard = DoLexerDiscard;
        }

        private bool DoLexerDiscard(Char element)
        {
            var ignorableChars = new char[]
            {
                '\n',
                '\r',
                '\t',
                ' '
            };

            return ignorableChars.Contains(element);
        }

    }
}
