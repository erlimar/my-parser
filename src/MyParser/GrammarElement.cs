using System;

namespace MyParser
{
    public abstract class GrammarElement
    {
        public abstract Token Eval(TokenExtractor extractor);

        protected void EnsureExtractor(TokenExtractor extractor, string paramName)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}