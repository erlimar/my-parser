namespace MyParser2.Parser
{
    public class MyAbstractSyntaxTree
    {
        public MyAbstractSyntaxTree(SyntaxTreeNode rootNode)
        {
            RootNode = rootNode;
        }

        public SyntaxTreeNode RootNode { get; private set; }
    }
}
