using System;
using System.Collections.Generic;

namespace MyParser
{
    public class TokenTreeNode
    {
        public Token Token { get; set; }
        public TokenTreeNode Parent { get; set; }
        public IList<TokenTreeNode> Childs { get; set; }

        public TokenTreeNode(Token token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));

            Childs = new List<TokenTreeNode>();
        }
    }
}