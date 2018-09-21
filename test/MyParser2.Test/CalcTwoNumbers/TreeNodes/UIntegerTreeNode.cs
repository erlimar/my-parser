using MyParser2.Parser;

namespace MyParser2.Test.CalcTwoNumbers.TreeNodes
{
    public class UIntegerTreeNode : SyntaxTreeNode
    {
        public UIntegerTreeNode(uint value)
            : base(value)
        { }

        public new uint Value
        {
            get
            {
                return (uint)base.Value;
            }
        }
    }
}
