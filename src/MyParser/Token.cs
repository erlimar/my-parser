namespace MyParser
{
    public class Token
    {
        public Token(object content)
        {
            Content = content;
        }

        public long ContentPosBegin { get; set; }
        public long ContentLength { get; set; }
        public object Content { get; private set; }
    }
}