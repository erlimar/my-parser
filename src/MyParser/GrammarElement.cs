namespace MyParser
{
    public abstract class GrammarElement
    {
        public abstract Token Eval(TokenExtractor extractor);
    }
}