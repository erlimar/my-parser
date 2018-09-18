using System;

namespace MyParser.CommonElements
{
    public class OrListGrammarElement
        : GrammarElement
    {
        private readonly GrammarElement[] _list;

        public OrListGrammarElement(GrammarElement[] list)
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

            try
            {
                Token token = null;

                foreach (var grammarElement in _list)
                {
                    token = grammarElement.Eval(extractor);

                    if (token != null)
                    {
                        break;
                    }
                }

                if (token == null)
                {
                    extractor.RollbackCursor(cursor);
                    return null;
                }

                return token;
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }
        }
    }
}
