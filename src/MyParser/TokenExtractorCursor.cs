namespace MyParser
{
    public struct TokenExtractorCursor
    {
        public long Position;

        public static TokenExtractorCursor Invalid = new TokenExtractorCursor
        {
            Position = -1
        };
    }
}