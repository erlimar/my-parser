using System;
using System.IO;
using System.Text;

namespace MyParser2.Compiler
{
    public class CodeStringStream : ObjectStream<char>
    {
        private readonly Stream _stream;

        public CodeStringStream(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            _stream = new MemoryStream(Encoding.ASCII.GetBytes(code));
        }

        public override bool EndOfStream()
        {
            if (_stream.Position < _stream.Length)
            {
                return false;
            }

            return true;
        }

        public override void SetPosition(long position)
        {
            _stream.Position = position;
        }

        public override long GetPosition()
        {
            return _stream.Position;
        }

        public override char Next()
        {
            return (char)_stream.ReadByte();
        }
    }
}
