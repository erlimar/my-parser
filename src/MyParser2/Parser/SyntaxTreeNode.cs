using System;
using System.Collections.Generic;

namespace MyParser2.Parser
{
    public class SyntaxTreeNode
    {
        public object Value { get; set; }
        public SyntaxTreeNode Parent { get; set; }
        public IList<SyntaxTreeNode> Childs { get; set; }

        public SyntaxTreeNode()
            : this(null)
        { }

        public SyntaxTreeNode(object value)
        {
            Value = value;
            Childs = new List<SyntaxTreeNode>();
        }

        public void AddChildNode(SyntaxTreeNode childNode)
        {
            if (childNode == null)
            {
                throw new ArgumentNullException(nameof(childNode));
            }

            childNode.Parent = this;
            Childs.Add(childNode);
        }
    }
}
