using System;

namespace MyParser
{
    public class SyntaxTree
    {
        public bool IsValid { get; private set; }
        public SyntaxTreeNode RootNode { get; private set; }

        public void Validate(SyntaxTreeNode node)
        {
            RootNode = node;
            IsValid = RootNode != null;
        }
    }
}