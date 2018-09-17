using System;
using System.Collections.Generic;
using System.Text;

namespace MyParser.CommonElements
{
    public class CharGrammarElement
        : GrammarElement
    {
        private readonly char _char;

        public CharGrammarElement(char c)
        {
            _char = c;
        }

        public override Token Eval(TokenExtractor extractor)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException(nameof(extractor));
            }

            var cursor = extractor.SaveCursor();

            try
            {
                var c = extractor.NextChar();

                if (c != _char)
                {
                    return null;
                }

                return new Token(c);
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }

            return null;
        }
    }
}
