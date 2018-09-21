using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;

namespace MyParser2.Grammar.CommonElements
{
    public class GroupAndGrammarElement : MyGrammarElement
    {
        private readonly MyGrammarElement[] _elements;

        public GroupAndGrammarElement(MyGrammarElement[] elements)
        {
            _elements = elements
                ?? throw new ArgumentNullException(nameof(elements));
        }

        public override MyToken[] Eval(ObjectStream<Char> input, MyScannerDiscardDelegate<char> discarder)
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

                // Qualquer elemento do grupo falhando, todo o grupo falha
                if (elementTokens == null)
                {
                    return null;
                }

                foundTokens.AddRange(elementTokens);
            }

            if (!foundTokens.Any())
            {
                input.SetPosition(initialPos);

                return null;
            }

            var token = new MyToken(
                MakeTokenClass(),
                initialPos,
                input.GetPosition(),
                null
            );

            var firstToken = new MyToken[] { token };

            return firstToken.Concat(foundTokens).ToArray();
        }
    }
}
