using System;
using System.Collections.Generic;
using System.Text;

namespace MyParser.CommonElements
{
    public class CharListGrammarElement
        : GrammarElement
    {
        private readonly CharGrammarElement[] _list;

        public CharListGrammarElement(CharGrammarElement[] charList)
        {
            _list = charList ?? throw new ArgumentNullException(nameof(charList));

            if (_list.Length < 1)
            {
                throw new ArgumentException($"Argument {nameof(charList)} can not be empty", nameof(charList));
            }
        }

        public override Token Eval(TokenExtractor extractor)
        {
            EnsureExtractor(extractor, nameof(extractor));

            var cursor = extractor.SaveCursor();
            var content = new StringBuilder();

            try
            {
                foreach (var charGrammarElement in _list)
                {
                    var charToken = charGrammarElement.Eval(extractor);

                    if (charToken == null)
                    {
                        extractor.RollbackCursor(cursor);
                        return null;
                    }

                    content.Append((char)charToken.Content);
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
