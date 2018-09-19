using System;

namespace MyParser
{
    public delegate bool GrammarIgnoreDelegate(char c);

    public class Grammar
    {
        private static GrammarIgnoreDelegate _neverIgnore =
            (c) => false;

        public Grammar(GrammarElement rootElement)
            : this(rootElement, _neverIgnore)
        { }

        public Grammar(GrammarElement rootElement, GrammarIgnoreDelegate ignoreDelegate)
        {
            RootElement = rootElement ?? throw new ArgumentNullException(nameof(rootElement));
            IgnoreDelegate = ignoreDelegate ?? throw new ArgumentNullException(nameof(ignoreDelegate));
        }

        public GrammarElement RootElement { get; private set; }
        public GrammarIgnoreDelegate IgnoreDelegate { get; private set; }
    }
}