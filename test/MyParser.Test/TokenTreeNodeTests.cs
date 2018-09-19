using System;
using Xunit;

namespace MyParser.Test
{
    [Trait("Target", nameof(SyntaxTreeNode))]
    public class TokenTreeNodeTests
    {
        [Fact(DisplayName = "Token é obrigatório")]
        public void Token_EhObrigatorio()
        {
            var ex = Assert.Throws<ArgumentNullException>(
                () => new SyntaxTreeNode(null)
            );

            Assert.Equal("token", ex.ParamName);
        }

        [Fact(DisplayName = "A propriedade Token é exatamente o token passado no construtor")]
        public void Propriedade_Token_EhUm_Wrapper()
        {
            var token = new Token(null);
            var node = new SyntaxTreeNode(token);

            Assert.Equal(token, node.Token);
        }

        [Fact(DisplayName = "Childs inicia como uma lista vazia")]
        public void Childs_IniciaComo_ListaVazia()
        {
            var node = new SyntaxTreeNode(new Token(null));

            Assert.NotNull(node.Childs);
            Assert.Equal(0, node.Childs.Count);
        }

        [Fact(DisplayName = "Parent inicia como nulo")]
        public void Parent_IniciaComo_Nulo()
        {
            var node = new SyntaxTreeNode(new Token(null));

            Assert.Null(node.Parent);
        }
    }
}
