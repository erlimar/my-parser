using System;

namespace MyParser2.Compiler
{
    public class CodeStringStream : ObjectStream<char>
    {
        private readonly string _code;

        public CodeStringStream(string code)
        {
            _code = code
                ?? throw new ArgumentNullException(nameof(code));
        }
    }
}
