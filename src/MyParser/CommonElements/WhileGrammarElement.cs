using System;
using System.Collections.Generic;

namespace MyParser.CommonElements
{
    public class WhileGrammarElement
        : GrammarElement
    {
        private readonly GrammarElement _element;

        public WhileGrammarElement(GrammarElement element)
        {
            _element = element ?? throw new ArgumentNullException(nameof(element));
        }

        public override Token Eval(TokenExtractor extractor)
        {
            EnsureExtractor(extractor, nameof(extractor));

            var cursor = extractor.SaveCursor();
            var content = new List<Token>();

            try
            {
                while (true)
                {
                    var token = _element.Eval(extractor);

                    if (token == null)
                    {
                        break;
                    }

                    content.Add(token);
                }
            }
            catch (Exception)
            {
                extractor.RollbackCursor(cursor);
                throw;
            }

            // Se não achou nenhum token to elemento informado é inválido
            if (content.Count < 1)
            {
                return null;
            }

            return new Token(content.ToArray())
            {
                ContentPosBegin = cursor.Position,
                ContentLength = extractor.SaveCursor().Position - cursor.Position
            };
        }
    }
}
