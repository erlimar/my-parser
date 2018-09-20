namespace MyParser2.Lexer
{
    public class MyToken
    {
        public MyToken(long initialPos, long finalPos, object content)
        {
            InitialPos = initialPos;
            FinalPos = finalPos;
            Content = content;
        }

        public long InitialPos { get; private set; }
        public long FinalPos { get; private set; }
        public object Content { get; private set; }
    }
}
