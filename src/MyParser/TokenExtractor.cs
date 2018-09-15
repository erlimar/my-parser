using System;

namespace MyParser
{
    public class TokenExtractor
    {
        private TokenExtractor(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }
        }

        public static TokenExtractor FromString(string code)
        {
            return new TokenExtractor(code);
        }

        public bool Ended()
        {
            return true;
        }
    }
}