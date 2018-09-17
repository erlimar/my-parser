using System;
using System.IO;
using System.Text;

namespace MyParser
{
    public class TokenExtractor : IDisposable
    {
        private Stream _reader;

        private TokenExtractor(Stream codeStream)
        {
            _reader = codeStream ?? codeStream;
        }

        public static TokenExtractor FromString(string code)
        {
            if (code == null)
            {
                throw new ArgumentNullException(nameof(code));
            }

            var textBytes = Encoding.ASCII.GetBytes(code);
            var codeStream = new MemoryStream(textBytes, false);

            return new TokenExtractor(codeStream);
        }

        public bool EndOfCode
        {
            get
            {
                if (_reader.Position < _reader.Length)
                {
                    return false;
                }

                return true;
            }
        }

        public TokenExtractorCursor SaveCursor()
            => new TokenExtractorCursor
            {
                Position = _reader.Position
            };

        public void RollbackCursor(TokenExtractorCursor cursor)
        {
            if (cursor.Position < 0 || cursor.Position > _reader.Length - 1)
            {
                throw new IndexOutOfRangeException();
            }

            _reader.Position = cursor.Position;
        }

        #region IDisposable Support
        private bool _disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    if (_reader != null)
                        _reader.Dispose();
                }

                _reader = null;
                _disposedValue = true;
            }
        }

        public char NextChar()
        {
            return (char)_reader.ReadByte();
        }

        public void Dispose() => Dispose(true);
        #endregion
    }
}