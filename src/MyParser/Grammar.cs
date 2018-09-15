using System;

namespace MyParser
{
    public class Grammar
    {
        private readonly GrammarElement _rootElement;

        public Grammar(GrammarElement rootElement)
        {
            _rootElement = rootElement ?? throw new ArgumentNullException(nameof(rootElement));
        }
    }
}