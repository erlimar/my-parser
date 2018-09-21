using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;

namespace MyParser2.Grammar.CommonElements
{
    public class CharacterGrammarElement : MyGrammarElement
    {
        private readonly Char _character;

        public CharacterGrammarElement(Char character)
        {
            _character = character;
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
            char character = input.Next();

            if (character != _character)
            {
                input.SetPosition(initialPos);

                return null;
            }

            var token = new MyToken(
                MakeTokenClass(),
                initialPos,
                input.GetPosition(),
                character
            );

            return new MyToken[]
            {
                token
            };
        }
    }
}
