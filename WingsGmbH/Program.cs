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
            ['a', 'd', 'e', 'b']
        };
        int[][] weightMask = {
            [20],
            [20, 20],
            [20, 20, 20]
        };
        double[][] costMask = {
            [20d],
            [20d, 15d],
            [20d, 20d, 30d]
        };

        Array2DConverter converter = new(routes, weightMask, costMask);
        var routeTree2 = converter.ConvertToRouteTree();
        PrintListOfLists(routeTree2.FindPaths('a','b'));

        routeTree2?.PrintTree();
    }
    
    public static void PrintListOfLists(List<List<char>> listOfLists)
    {
        if (listOfLists == null || listOfLists.Count == 0)
        {
            Console.WriteLine("The list is empty.");
            return;
        }

        foreach (var innerList in listOfLists)
        {
            if (innerList != null && innerList.Count > 0)
            {
                Console.WriteLine($"[{string.Join(", ", innerList)}]");
            }
            else
            {
                Console.WriteLine("[]"); // Handle empty or null inner lists
            }
        }
    }

}