using MyParser2.Lexer;
using System;

namespace MyParser2.Grammar
{
    public class MyGrammar
    {
        public MyDiscardDelegate<Char> OnLexerDiscard { get; protected set; }
        public MyDiscardDelegate<MyToken> OnParserDiscard { get; protected set; }
        public MyGrammarElement RootElement { get; protected set; }
    }
}
