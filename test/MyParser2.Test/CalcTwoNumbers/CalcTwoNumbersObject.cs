using MyParser2.Compiler;
using MyParser2.Parser;
using MyParser2.Test.CalcTwoNumbers.TreeNodes;
using System;

namespace MyParser2.Test.CalcTwoNumbers
{
    public class CalcTwoNumbersObject
    {
        public uint N1 { get; }
        public uint N2 { get; }

        public CalcTwoNumbersObject(uint n1, uint n2)
        {
            N1 = n1;
            N2 = n2;
        }

        public uint CalcNumbers()
        {
            return N1 + N2;
        }

        public static CalcTwoNumbersObject Emitter(MyAbstractSyntaxTree tree)
        {
            EnsureSyntaxTree(tree);

            // Após garantir a estrutura de nossa árvore sintática abstrata,
            // o que temos que fazer aqui é simples.

            // [árvore sintática]
            //        exp
            //         |
            //        sum
            //        /|\
            //       / | \
            //      N1 + N2

            var expNode = tree.RootNode;
            var sumNode = expNode.Childs[0];
            var n1Node = sumNode.Childs[0] as UIntegerTreeNode;
            var n2Node = sumNode.Childs[1] as UIntegerTreeNode;

            return new CalcTwoNumbersObject(n1Node.Value, n2Node.Value);
        }

        private static void EnsureSyntaxTree(MyAbstractSyntaxTree tree)
        {
            if (tree == null)
            {
                throw new ArgumentNullException(nameof(tree));
            }

            void throwException() => throw new CompilerException();

            if (tree.RootNode == null)
            {
                throwException();
            }

            if (tree.RootNode.Parent != null)
            {
                throwException();
            }

            if (tree.RootNode.Childs == null)
            {
                throwException();
            }

            if (tree.RootNode.Value != null)
            {
                throwException();
            }

            if (tree.RootNode.Childs.Count != 1)
            {
                throwException();
            }

            // Primeiro elemento é um [SumOperatorTreeNode]
            if (tree.RootNode.Childs[0].GetType() != typeof(SumOperatorTreeNode))
            {
                throwException();
            }

            // Primeiro elemento tem mais dois filhos
            if (tree.RootNode.Childs[0].Childs == null || tree.RootNode.Childs[0].Childs.Count != 2)
            {
                throwException();
            }

            // Os elementos filhos são um [UIntegerTreeNode]
            if (tree.RootNode.Childs[0].Childs[0].GetType() != typeof(UIntegerTreeNode) ||
                tree.RootNode.Childs[0].Childs[1].GetType() != typeof(UIntegerTreeNode))
            {
                throwException();
            }

            // Os elementos filhos não tem mais filhos
            if ((tree.RootNode.Childs[0].Childs[0].Childs != null && tree.RootNode.Childs[0].Childs[0].Childs.Count > 0) ||
                (tree.RootNode.Childs[0].Childs[1].Childs != null && tree.RootNode.Childs[0].Childs[1].Childs.Count > 0))
            {
                throwException();
            }
        }
    }
}
