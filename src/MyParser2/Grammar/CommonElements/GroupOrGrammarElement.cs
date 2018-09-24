using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;
using MyParser2.Parser;

namespace MyParser2.Grammar.CommonElements
{
    public class GroupOrGrammarElement : MyGrammarElement
    {
        private readonly MyGrammarElement[] _elements;

        public GroupOrGrammarElement(MyGrammarElement[] elements)
        {
            _elements = elements
                ?? throw new ArgumentNullException(nameof(elements));
        }

        public override MyToken[] Eval(ObjectStream<Char> input, MyDiscardDelegate<char> discarder)
        {
            Ensure(input, discarder);

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            if (input.EndOfStream())
            {
                return null;
            }

            var initialPos = input.GetPosition();
            var foundTokens = new List<MyToken>();

            foreach (var element in _elements)
            {
                var elementTokens = element.Eval(input, discarder);

                // O primeiro elemento que obter sucesso, esse é o aceito
                if (elementTokens != null)
                {
                    foundTokens.AddRange(elementTokens);
                    break;
                }
            }

            if (!foundTokens.Any())
            {
                input.SetPosition(initialPos);

                return null;
            }

            return foundTokens.ToArray();
        }

        public override SyntaxTreeNode Make(ObjectStream<MyToken> input, MyDiscardDelegate<MyToken> discarder)
        {
            throw new NotImplementedException();
        }
    }
}
