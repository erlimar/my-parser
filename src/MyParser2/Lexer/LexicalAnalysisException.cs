using System;

namespace MyParser2.Lexer
{
    public class LexicalAnalysisException : Exception
    {
        public LexicalAnalysisException()
            : this(null)
        { }

        public LexicalAnalysisException(Exception innerException)
            : this("Unexpected error when scanning input", innerException)
        { }

        public LexicalAnalysisException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
