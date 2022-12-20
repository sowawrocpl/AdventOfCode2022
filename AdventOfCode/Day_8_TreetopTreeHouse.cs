using System.ComponentModel;

namespace AdventOfCode;

public class Day_8_TreetopTreeHouse
{
    public void Execute()
    {
        string filePath = InputPathProvider.GetInputPath("day_8.txt");

        Dictionary<int, List<Tree>> xDictionary = new Dictionary<int, List<Tree>>(); // key = x
        Dictionary<int, List<Tree>> yDictionary = new Dictionary<int, List<Tree>>(); // key = y
        for (int i = 0; i < 99; i++)
        {
            xDictionary[i] = new List<Tree>();
            yDictionary[i] = new List<Tree>();
        }

        List<Tree> allTrees = new List<Tree>();


        using (var reader = new StreamReader(filePath))
        {
            int y = 0;
            while (reader.Peek() >= 0)
            {
                var digits = reader.ReadLine().ToCharArray().Select(c => (int)c - 48).ToList();
                for (int x = 0; x < digits.Count; x++)
                {
                    var tree = new Tree(x, y, digits[x]);
                    xDictionary[x].Add(tree);
                    yDictionary[y].Add(tree);
                    allTrees.Add(tree);
                }

                y++;
            }
        }

        int maxScenicScore = allTrees.Max(GetScenicScore);
        Console.WriteLine(maxScenicScore);

        bool IsVisible(Tree tree)
        {
            var xTrees = xDictionary[tree.X];
            var yTrees = yDictionary[tree.Y];

            if (tree.X == 0 || tree.X == xDictionary.Keys.Last() || tree.Y == 0 || tree.Y == yDictionary.Keys.Last())
            {
                return true;
            }

            var maxBottom = xTrees.Where(t => t.Y < tree.Y).Max(x => x.Height);
            var maxTop = xTrees.Where(t => t.Y > tree.Y).Max(x => x.Height);
            var maxLeft = yTrees.Where(t => t.X < tree.X).Max(x => x.Height);
            var maxRight = yTrees.Where(t => t.X > tree.X).Max(x => x.Height);

            return new[] { maxBottom, maxTop, maxLeft, maxRight }.Any(h => h < tree.Height);
        }

        int GetScenicScore(Tree theThree)
        {
            var xTrees = xDictionary[theThree.X];
            var yTrees = yDictionary[theThree.Y];

            if (theThree.X == 0 || theThree.X == xDictionary.Keys.Last() || theThree.Y == 0 || theThree.Y == yDictionary.Keys.Last())
            {
                return 0;
            }

            var bottom = xTrees.Where(t => t.Y < theThree.Y).OrderByDescending(t => t.Y);
            var top = xTrees.Where(t => t.Y > theThree.Y).OrderBy(t => t.Y);
            var left = yTrees.Where(t => t.X < theThree.X).OrderByDescending(t => t.X);
            var right = yTrees.Where(t => t.X > theThree.X).OrderBy(t => t.X);

            var multipliers = new[] { bottom, top, left, right }
                .Select(trees => GetDistanceScenicForOneDirection(theThree, trees.ToList()));
            int result = 1;
            foreach (var multiplier in multipliers)
            {
                result *= multiplier;
            }

            return result;
        }

        int GetDistanceScenicForOneDirection(Tree theTree, List<Tree> trees)
        {
            int counter = 0;

            for (int i = 0; i < trees.Count; i++)
            {
                counter++;
                if (trees[i].Height >= theTree.Height)
                {
                    break;
                }
            }

            return counter;
        }
    }



    private class Tree
    {
        public Tree(int x, int y, int height)
        {
            X = x;
            Y = y;
            Height = height;
        }

        public int X { get; }

        public int Y { get; }

        public int Height { get; }
    }
}
