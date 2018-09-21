using System;
using System.Collections.Generic;
using System.Linq;

namespace MyParser2.Lexer
{
    public class TokenStream : ObjectStream<MyToken>
    {
        private readonly IList<MyToken> _stream = new List<MyToken>();
        private long _currentPos = 0;

        public override bool EndOfStream()
        {
            if (_currentPos < _stream.Count)
            {
                return false;
            }

            return true;
        }

        public override void SetPosition(long position)
        {
            if (position >= _stream.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(position));
            }

            _currentPos = position;
        }

        public override long GetPosition()
        {
            return _currentPos;
        }

        public override MyToken Next()
        {
            if (EndOfStream())
            {
                return null;
            }

            return _stream.ElementAt((int)_currentPos++);
        }

        public override void Push(MyToken element)
        {
            if (element == null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            _stream.Add(element);
        }
    }
}
