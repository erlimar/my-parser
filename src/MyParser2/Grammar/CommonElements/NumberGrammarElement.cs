using System;
using System.Linq;
using System.Collections.Generic;
using MyParser2.Lexer;

namespace MyParser2.Grammar.CommonElements
{
    public class NumberGrammarElement : MyGrammarElement
    {
        public override MyToken[] Eval(ObjectStream<Char> input, MyScannerDiscardDelegate<char> discarder)
        {
            Ensure(input, discarder);

            // Ignora código descartável inicialmente
            input.Discard(discarder);

            // Salva a posição inicial no stream de entrada
            // OBS: Aqui salvamos só após ignorar os caracteres
            //      descartáveis, porque o descarte é global para
            //      o processo. Então a posição inicial é justamente
            //      no primeiro caractere não descartável
            var initialPos = input.GetPosition();

            var validChars = "0123456789".ToCharArray();

            var foundChars = new List<Char>();

            while (!input.EndOfStream())
            {
                var pos = input.GetPosition();
                char c = input.Next();

                if (!validChars.Contains(c))
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
                initialPos,
                input.GetPosition(),
                new string(foundChars.ToArray())
            );

            return new MyToken[]
            {
                token
            };
        }
    }
}
