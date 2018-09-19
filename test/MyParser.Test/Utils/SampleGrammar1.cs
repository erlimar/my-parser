using Xunit;

namespace MyParser.Test.Utils
{
    public class SampleGrammar1 : Grammar
    {
        public SampleGrammar1()
            : base(RootElement, IgnoreDelegate)
        { }

        private new static GrammarElement RootElement
        {
            get
            {
                return null;
            }
        }

        private new static bool IgnoreDelegate(char c)
        {
            return false;
        }

        #region Códigos exemplo

        /// <summary>
        /// Código de exemplo #1
        /// </summary>
        public static string SampleCode1
            => @"[ {1:2}, { 66 : 47}, {100000:     2      } ]";

        #endregion

        #region Asserts

        /// <summary>
        /// Avalia resultado de código de exemplo #1
        /// </summary>
        /// <param name="tree">Árvore de sintaxe gerada pelo analisador</param>
        public static void AssertSampleCode1(SyntaxTree tree)
        {
            Assert.NotNull(tree);
            Assert.NotNull(tree.RootNode);
            Assert.NotNull(tree.RootNode.Token);
            Assert.Null(tree.RootNode.Parent);
            Assert.NotNull(tree.RootNode.Childs);

            // _________-------+<PAIR_LIST>--+
            // |                             |
            // \ <PAIR_NUMBER_LEFT>          |_________
            //  \         |__                          \
            //   \  <PAIR>   \__   <PAIR_NUMBER_RIGHT>  |
            //    \    \-------+\-----+/   <SPACE>      |
            //    /------------(__\ __/)---__/___--------\
            // "[ {1:2      }, {66 :47}, {      100000:2} ]"
            //  ^            ^ ^   ^  ^                   ^
            //  |  <COMMA>---| |   |  |                   |
            //  |              |   |  |                   |
            //  | <PAIR_OPEN>--|   |  |--<PAIR_CLOSE>     |
            //  |                 /                       |
            //  |          <PAIR_SEPARATOR>               |
            //  |                                         |
            //  |----<BRACKET_OPEN>                       |
            //                       <BRACKET_CLOSE>------|

            // Composição da árvore sintática
            // <BRACKET_OPEN> + <PAIR_LIST> + <BRACKET_CLOSE>


            // Composição de PAIR_LIST
            // <PAIR> + <COMMA> + <PAIR> + <COMMA> + <PAIR>

            // Composição de PAIR
            // <PAIR_NUMBER_LEFT> + <PAIR_SEPARATOR> + <PAIR_NUMBER_RIGHT>
        }

        #endregion
    }
}
