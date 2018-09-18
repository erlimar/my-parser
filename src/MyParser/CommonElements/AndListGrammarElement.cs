using System;
using System.Collections.Generic;

namespace MyParser.CommonElements
{
    public class AndListGrammarElement
        : GrammarElement
    {
        private readonly GrammarElement[] _list;

        public AndListGrammarElement(GrammarElement[] list)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));

            if (_list.Length < 1)
            {
                throw new ArgumentException($"Argument {nameof(list)} can not be empty", nameof(list));
            }
        }

        public override Token Eval(TokenExtractor extractor)
        {
            EnsureExtractor(extractor, nameof(extractor));

            var cursor = extractor.SaveCursor();
            var content = new List<Token>();

            try
            {
                foreach (var grammarElement in _list)
                {
                    var token = grammarElement.Eval(extractor);

                    if (token == null)
                    {
                        extractor.RollbackCursor(cursor);
                        return null;
                    }

                    content.Add(token);
                }
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }

            return new Token(content.ToArray())
            {
                ContentPosBegin = cursor.Position,
                ContentLength = extractor.SaveCursor().Position - cursor.Position
            };
        }
    }
}
