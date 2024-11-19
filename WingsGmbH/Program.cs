using System.Collections;
using WingsGmbH;

class Program {
    static void Main(string[] args) {
        RouteTree testTree = new RouteTree();
        testTree.AddRouteOneWay('a', 'b', 40, 40d);
        testTree.AddRoute('b', 'a', 20, 20d);

        //testTree.PrintTree();
        //Console.WriteLine();

        char[][] routes = {
            ['a', 'b'],
            ['a', 'c', 'b'],
            ['d', 'a', 'e', 'b']
        };
        int[][] weightMask = {
            [10],
            [20, 20],
            [20, 20, 20]
        };
        double[][] costMask = {
            [22d],
            [20d, 15d],
            [20d, 20d, 30d]
        };

        Array2DConverter converter = new(routes, weightMask, costMask);
        var routeTree2 = converter.ConvertToRouteTree();
        var allPaths = routeTree2?.FindPaths('a', 'b', 11);
        
        var lowest = allPaths?.MinBy(path => path.cost);
        Console.WriteLine(value: lowest);
        // routeTree2?.PrintTree();
    }

    #region PrintLists
    public static void PrintListOfLists(List<List<char>> listOfLists) {
        if (listOfLists == null || listOfLists.Count == 0) {
            Console.WriteLine("The list is empty.");
            return;
        }

        foreach (var innerList in listOfLists) {
            if (innerList != null && innerList.Count > 0) {
                Console.WriteLine($"[{string.Join(", ", innerList)}]");
            }
            else {
                Console.WriteLine("[]"); // Handle empty or null inner lists
            }
        }
    }

    public static void PrintAllPaths(List<(List<char> path, double cost)> allPaths) {
        foreach (var (path, cost) in allPaths) {
            Console.Write("Path: ");
            foreach (var c in path) {
                Console.Write(c + " ");
            }

            Console.WriteLine($"- Cost: {cost}");
        }
    }
    
    public static void PrintCharDoubleList(List<(char character, double value)> list)
    {
        foreach (var (character, value) in list)
        {
            Console.WriteLine($"Character: {character}, Value: {value}");
        }
    }
    
    public static void PrintCharListWithDouble(List<(List<char>, double cost)> list)
    {
        foreach (var (characters, value) in list)
        {
            Console.Write("Characters: ");
            foreach (var c in characters)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine($"- Value: {value}");
        }
    }
    #endregion
}