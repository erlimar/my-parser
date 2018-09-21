﻿using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;

namespace MyParser2.Grammar.CommonElements
{
    public class NumberGrammarElement : MyGrammarElement
    {
        private static char[] _validChars = "0123456789".ToCharArray();

        public override MyToken[] Eval(ObjectStream<Char> input, MyScannerDiscardDelegate<char> discarder)
        {
            Ensure(input, discarder);

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            var initialPos = input.GetPosition();
            var foundChars = new List<Char>();

            while (!input.EndOfStream())
            {
                var pos = input.GetPosition();
                char c = input.Next();

                if (!_validChars.Contains(c))
                {
                    input.SetPosition(pos);
                    break;
                }

                foundChars.Add(c);
            }

            if (!foundChars.Any())
            {
                input.SetPosition(initialPos);

                return null;
            }

            var token = new MyToken(
                MakeTokenClass(),
                initialPos,
                input.GetPosition(),
                new string(foundChars.ToArray())
            );

            return new MyToken[] { token };
        }
    }
}
