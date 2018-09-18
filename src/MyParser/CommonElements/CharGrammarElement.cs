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
            EnsureExtractor(extractor, nameof(extractor));

            // TODO: Remover tratamento de cursor do extrator
            //       Isso será garantido pelo anterior?
            var cursor = extractor.SaveCursor();

            try
            {
                var c = extractor.NextChar();

                if (c == _char)
                {
                    return new Token(c)
                    {
                        ContentPosBegin = cursor.Position,
                        ContentLength = extractor.SaveCursor().Position - cursor.Position
                    };
                }
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }

            extractor.RollbackCursor(cursor);
            return null;
        }
    }
}
