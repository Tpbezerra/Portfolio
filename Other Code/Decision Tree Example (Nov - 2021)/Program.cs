using System;

namespace DecisionTree
{
    class Program
    {
        static void Main()
        {
            DecisionTree petTree = new DecisionTree();
            Generate(petTree);

            petTree.Run(20);
        }
        
        //Moved to own method, just to clean up Main()
        static void Generate(DecisionTree newTree)
        {
            newTree.SetRoot(1, (x) => { return x > 50; }, () => { Console.WriteLine("Fur or no fur?"); });
            newTree.AddTrueNode(1, 2, (x) => { return x > 75; }, () => { Console.WriteLine("Should the pet be smart?"); });
            newTree.AddFalseNode(1, 3, (x) => { return x > 25; }, () => { Console.WriteLine("Should the pet be smart?"); });
            newTree.AddTrueNode(2, 4, (x) => { return true; }, () => { Console.WriteLine("Pet should be a Dog"); });
            newTree.AddFalseNode(2, 5, (x) => { return true; }, () => { Console.WriteLine("Pet should be a Cat"); });
            newTree.AddTrueNode(3, 6, (x) => { return true; }, () => { Console.WriteLine("Pet should be a Parrot"); });
            newTree.AddFalseNode(3, 7, (x) => { return true; }, () => { Console.WriteLine("Pet should be a Guppy"); });
            
            Console.WriteLine();
        }
    }

    class DecisionTree
    {
        class BinTree
        {
            public int ID;

            public Action Eval;
            public Func<int, bool> question;

            public BinTree trueBranch;
            public BinTree falseBranch;

            public BinTree(int newID, Func<int, bool> newQuestion, Action newEval)
            {
                this.ID = newID;
                question = newQuestion;
                Eval = newEval;
            }

            public void RunActions(int comparer)
            {
                Eval?.Invoke();
                if (question(comparer))
                {
                    if (trueBranch != null)
                        trueBranch.RunActions(comparer);
                }
                else
                {
                    if (falseBranch != null)
                        falseBranch.RunActions(comparer);
                }
            }
        }
        
        BinTree root;
        public void SetRoot(int newID, Func<int, bool> newQuestion, Action newEval)
        {
            root = new BinTree(newID, newQuestion, newEval);
        }
        
        public void AddTrueNode(int existingNodeID, int newNodeID, Func<int, bool> newQuestion, Action newQuestAns)
        {
            if (root == null)
            {
                Console.WriteLine("ERROR: No DT!");
                return;
            }

            if (ParseTreeAndAddTrueNode(root, existingNodeID, newNodeID, newQuestion, newQuestAns))
                Console.WriteLine($"Added node {newNodeID} onto \"True\" branch of node {existingNodeID}");
            else
                Console.WriteLine($"Node {existingNodeID} not found!");
        }

        bool ParseTreeAndAddTrueNode(BinTree currentNode, int existingNodeID, int newNodeID, Func<int, bool> newQuestion, Action newQuestAns)
        {
            if (currentNode.ID == existingNodeID)
            {
                if (currentNode.trueBranch == null)
                {
                    currentNode.trueBranch = new BinTree(newNodeID, newQuestion, newQuestAns);
                }
                else
                {
                    Console.WriteLine($"WARNING: Replacing (id = {currentNode.trueBranch.ID}) linked to True-branch of {existingNodeID}");
                    currentNode.trueBranch = new BinTree(newNodeID, newQuestion, newQuestAns);
                }

                return true;
            }
            else
            {
                if (currentNode.trueBranch != null)
                {
                    if (ParseTreeAndAddTrueNode(currentNode.trueBranch, existingNodeID, newNodeID, newQuestion, newQuestAns))
                    {
                        return true;
                    }
                    else
                    {
                        if (currentNode.falseBranch != null)
                            return ParseTreeAndAddTrueNode(currentNode.falseBranch, existingNodeID, newNodeID, newQuestion, newQuestAns);
                        else
                            return false;
                    }
                }

                return false;
            }
        }

        public void AddFalseNode(int existingNodeID, int newNodeID, Func<int, bool> newQuestion, Action newQuestAns)
        {
            if (root == null)
            {
                Console.WriteLine("ERROR: No DT!");
                return;
            }

            // Search tree
            if (ParseTreeAndAddFalseNode(root, existingNodeID, newNodeID, newQuestion, newQuestAns))
                Console.WriteLine($"Added node {newNodeID} onto \"False\" branch of node {existingNodeID}");
            else
                Console.WriteLine($"Node {existingNodeID} not found");
        }
        
        bool ParseTreeAndAddFalseNode(BinTree currentNode, int existingNodeID, int newNodeID, Func<int, bool> newQuestion, Action newQuestAns)
        {
            if (currentNode.ID == existingNodeID)
            {
                if (currentNode.falseBranch == null)
                {
                    currentNode.falseBranch = new BinTree(newNodeID, newQuestion, newQuestAns);
                }
                else
                {
                    Console.WriteLine($"WARNING: Replacing (id = {currentNode.falseBranch.ID}) linked to True-branch of node {existingNodeID}");
                    currentNode.falseBranch = new BinTree(newNodeID, newQuestion, newQuestAns);
                }

                return true;
            }
            else
            {
                if (currentNode.trueBranch != null)
                {
                    if (ParseTreeAndAddFalseNode(currentNode.trueBranch, existingNodeID, newNodeID, newQuestion, newQuestAns))
                    {
                        return true;
                    }
                    else
                    {
                        if (currentNode.falseBranch != null)
                            return ParseTreeAndAddFalseNode(currentNode.falseBranch, existingNodeID, newNodeID, newQuestion, newQuestAns);
                        else
                            return false;
                    }
                }
                else
                {
                    return false;
                }
            }
        }
        
        public void Run(int comparer)
        {
            if (root == null)
                return;

            root.RunActions(comparer);
        }
    }
}