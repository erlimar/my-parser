using System;

namespace MyParser
{
    public class TokenExtractor
    {
        public TokenExtractor(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
        }
    }
}