using System;

namespace MyParser2.Grammar
{
    public class MyGrammar
    {
        public MyScannerDiscardDelegate<Char> OnLexerDiscard { get; protected set; }
        public MyGrammarElement RootElement { get; protected set; }
    }
}
