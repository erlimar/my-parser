using System;

namespace MyParser
{
    public class Parser
    {
        private readonly Grammar _grammar;

        public Parser(Grammar grammar)
        {
            _grammar = grammar ?? throw new ArgumentNullException(nameof(grammar));
        }

        // TODO: Implementar log e OnError callback
        private void LogException(Exception ex) { }

        public SyntaxTree Parse(TokenExtractor extractor)
        {
            if (extractor == null)
            {
                throw new ArgumentNullException(nameof(extractor));
            }

            TokenExtractorCursor cursor = TokenExtractorCursor.Invalid;
            SyntaxTree tree = new SyntaxTree();

            // TODO: Mudar para [SyntaxTreeNode]
            Token token = null;

            try
            {
                cursor = extractor.SaveCursor();

                // TODO: Deve retornar [SyntaxTreeNode]
                token = _grammar.RootElement.Eval(extractor);

                if (token != null && extractor.EndOfCode)
                {
                    var node = new SyntaxTreeNode(token);

                    tree.Validate(node);
                }
            }
            catch (Exception ex)
            {
                if (!TokenExtractorCursor.Invalid.Equals(cursor))
                {
                    try { extractor.RollbackCursor(cursor); }
                    catch (Exception) {/* QUIET */}
                }

                LogException(ex);
            }

            if (token == null || !tree.IsValid)
            {
                if (!TokenExtractorCursor.Invalid.Equals(cursor))
                {
                    try { extractor.RollbackCursor(cursor); }
                    catch (Exception) {/* QUIET */}
                }
            }

            return tree;
        }
    }
}
