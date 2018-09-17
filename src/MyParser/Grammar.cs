using System;

namespace MyParser
{
    public class Grammar
    {
        public Grammar(GrammarElement rootElement)
        {
            RootElement = rootElement ?? throw new ArgumentNullException(nameof(rootElement));
        }

        public GrammarElement RootElement { get; private set; }
    }
}