using System;

namespace MyParser
{
    public class SyntaxTree
    {
        public bool IsValid { get; private set; }
        public TokenTreeNode RootNode { get; private set; }

        public void Validate(TokenTreeNode node)
        {
            RootNode = node;
            IsValid = RootNode != null;
        }
    }
}