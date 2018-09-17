namespace MyParser
{
    public class Token
    {
        public Token(object content)
        {
            Content = content;
        }

        public object Content { get; private set; }
    }
}