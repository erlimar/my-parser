using System;
using System.Collections.Generic;

namespace MyParser
{
    public class SyntaxTreeNode
    {
        public Token Token { get; set; }
        public SyntaxTreeNode Parent { get; set; }
        public IList<SyntaxTreeNode> Childs { get; set; }

        public SyntaxTreeNode(Token token)
        {
            Token = token ?? throw new ArgumentNullException(nameof(token));

            Childs = new List<SyntaxTreeNode>();
        }
    }
}