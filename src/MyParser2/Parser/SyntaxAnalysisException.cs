﻿using System;

namespace MyParser2.Parser
{
    public class SyntaxAnalysisException : Exception
    {
        public SyntaxAnalysisException()
            : this(null)
        { }

        public SyntaxAnalysisException(Exception innerException)
            : this("Unexpected error parsing input", innerException)
        { }

        public SyntaxAnalysisException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
