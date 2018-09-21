using System;

namespace MyParser2.Compiler
{
    public class CompilerException : Exception
    {
        public CompilerException()
            : this((Exception)null)
        { }

        public CompilerException(string message)
            : this(message, null)
        { }

        public CompilerException(Exception innerException)
            : this("Unexpected error while compiling", innerException)
        { }

        public CompilerException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
