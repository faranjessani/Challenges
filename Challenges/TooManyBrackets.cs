using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Challenges
{
    /// <summary>
    /// [2017-01-2] Challenge #298 [Easy] Too many Parentheses
    /// https://www.reddit.com/r/dailyprogrammer/comments/5llkbj/2017012_challenge_298_easy_too_many_parentheses/
    /// 
    /// Description:
    /// 
    /// Difficulty may be higher than easy,
    ///
    /// (((3))) is an expression with too many parentheses.
    /// 
    /// The rule for "too many parentheses" around part of an expression is that if removing matching parentheses around a section of text still leaves that section enclosed by parentheses, then those parentheses should be removed as extraneous.
    /// 
    /// (3) is the proper stripping of extra parentheses in above example.
    /// 
    /// ((a((bc)(de)))f) does not have any extra parentheses. Removing any matching set of parentheses does not leave a "single" parenthesesed group that was previously enclosed by the parentheses in question.
    /// 
    /// inputs:
    /// ((a((bc)(de)))f)  
    /// (((zbcd)(((e)fg))))
    /// ab((c))
    /// 
    /// outputs:
    /// ((a((bc)(de)))f)  
    /// ((zbcd)((e)fg))
    /// ab(c)
    ///
    /// Solution Notes:
    /// 
    /// The solution below parses the input string into a n-ry tree
    /// Where each open bracket increases the depth of the tree
    /// Creating the tree is an O(n) operation
    /// Once the tree is built, I can traverse it depth first
    /// I use the number and type of children at any level to determine if should be wrapped in a ()
    /// I could've taken this further and sub-typed Node instead of using an enum
    /// That way, Open nodes wouldn't allocate extra memory for Value
    /// I could've also used a stack when building a tree to avoid each node having a reference to their parent
    /// Otherwise, the code came out pretty clean
    /// </summary>
    [TestFixture]
    public class TooManyBrackets
    {
        [Test]
        [TestCase("(((3)))", "(3)")]
        [TestCase("((a((bc)(de)))f)", "((a((bc)(de)))f)")]
        [TestCase("(((zbcd)(((e)fg))))", "((zbcd)((e)fg))")]
        [TestCase("ab((c))", "ab(c)")]
        public void RemoveExtraParentheses(string input, string expected)
        {
            var root = new Node();
            BuildTree(input, root);
            var result = PrintTree(root);

            Assert.That(result, Is.EqualTo(expected));
        }

        private void BuildTree(string input, Node root)
        {
            foreach (var c in input)
                switch (c)
                {
                    case '(':
                        var openNode = Node.OpenNode(root);
                        root.Children.Add(openNode);
                        root = openNode;
                        break;
                    case ')':
                        root = root.Parent;
                        break;
                    default:
                        root.Children.Add(Node.Const(c, root));
                        break;
                }
        }

        private string PrintTree(Node node)
        {
            if (node.Type == NodeType.Const)
                return node.Value.ToString();

            var children = node.Children;
            if (children.Count > 1)
            {
                var result = new StringBuilder();
                if (node.Type == NodeType.Open)
                    result.Append('(');
                foreach (var child in children)
                    result.Append(PrintTree(child));
                if (node.Type == NodeType.Open)
                    result.Append(')');
                return result.ToString();
            }

            var firstChild = node.Children.First();
            return firstChild.Type == NodeType.Const ? '(' + PrintTree(firstChild) + ')' : PrintTree(firstChild);
        }
    }

    internal enum NodeType
    {
        Unknown,
        Const,
        Open,
        Close
    }

    internal class Node
    {
        public Node()
        {
            Children = new List<Node>();
        }

        public IList<Node> Children { get; set; }
        public NodeType Type { get; set; }
        public Node Parent { get; set; }
        public char Value { get; set; }

        public static Node OpenNode(Node parent)
        {
            return new Node {Type = NodeType.Open, Parent = parent};
        }

        public static Node Const(char c, Node parent)
        {
            return new Node {Type = NodeType.Const, Value = c, Parent = parent};
        }
    }
}