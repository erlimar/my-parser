using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;
using MyParser2.Parser;

namespace MyParser2.Grammar.CommonElements
{
    public class CharacterGrammarElement : MyGrammarElement
    {
        private readonly Char _character;

        public CharacterGrammarElement(Char character)
        {
            _character = character;
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
            char character = input.Next();

            if (character != _character)
            {
                input.SetPosition(initialPos);

                return null;
            }

            var token = new MyToken(
                GetTokenClass(),
                initialPos,
                input.GetPosition(),
                null
            );

            return new MyToken[] { token };
        }

        public override SyntaxTreeNode Make(ObjectStream<MyToken> input, MyDiscardDelegate<MyToken> discarder)
        {
            throw new NotImplementedException();
        }
    }
}
