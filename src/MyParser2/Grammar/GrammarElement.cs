using MyParser2.Lexer;
using System;

namespace MyParser2.Grammar
{
    public abstract class MyGrammarElement
    {
        public abstract MyToken[] Eval(ObjectStream<Char> input, MyScannerDiscardDelegate<Char> discarder);

        protected void Ensure(ObjectStream<char> input, MyScannerDiscardDelegate<char> discarder)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }

            if (discarder == null)
            {
                throw new ArgumentNullException(nameof(discarder));
            }
        }
    }
}