using System;
using System.Collections.Generic;
using System.Text;

namespace MyParser.CommonElements
{
    public class CharListGrammarElement
        : AndListGrammarElement
    {
        public CharListGrammarElement(CharGrammarElement[] list)
            : base(list ?? throw new ArgumentNullException(nameof(list)))
        { }

        public override Token Eval(TokenExtractor extractor)
        {
            EnsureExtractor(extractor, nameof(extractor));

            var cursor = extractor.SaveCursor();
            var content = new StringBuilder();

            try
            {
                var token = base.Eval(extractor);

                if (token == null)
                {
                    extractor.RollbackCursor(cursor);
                    return null;
                }

                foreach (var childToken in (token.Content as Token[]))
                {
                    content.Append((char)childToken.Content);
                }
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }

            return new Token(content.ToString())
            {
                ContentPosBegin = cursor.Position,
                ContentLength = extractor.SaveCursor().Position - cursor.Position
            };
        }
    }
}
