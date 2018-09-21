using System;

namespace MyParser2.Lexer
{
    public class MyToken
    {
        public MyToken(object @class, long initialPos, long finalPos, object content)
        {
            if (@class == null)
            {
                throw new ArgumentNullException(nameof(@class));
            }

            InitialPos = initialPos;
            FinalPos = finalPos;
            Content = content;
            Class = @class;
        }

        public long InitialPos { get; private set; }
        public long FinalPos { get; private set; }
        public object Content { get; private set; }
        public object Class { get; private set; }
    }
}
