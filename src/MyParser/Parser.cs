using System;
using MyParser.Test;

namespace MyParser
{
    public class Parser
    {
        private readonly Grammar _grammar;

        public Parser(Grammar grammar)
        {
            _grammar = grammar ?? throw new ArgumentNullException(nameof(grammar));
        }

        public SyntaxTree Parse(TokenExtractor extractor)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException(nameof(extractor));
            }

            return new SyntaxTree();
        }
    }
}
