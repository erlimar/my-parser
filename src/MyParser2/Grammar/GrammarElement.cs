using MyParser2.Lexer;
using MyParser2.Parser;
using System;

namespace MyParser2.Grammar
{
    public abstract class MyGrammarElement
    {
        public abstract MyToken[] Eval(ObjectStream<Char> input, MyDiscardDelegate<Char> discarder);
        public abstract SyntaxTreeNode Make(ObjectStream<MyToken> input, MyDiscardDelegate<MyToken> discarder);

        public virtual object GetTokenClass()
        {
            throw new NotImplementedException();
        }

        protected void Ensure<T>(ObjectStream<T> input, MyDiscardDelegate<T> discarder)
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