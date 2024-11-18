using System.Collections;

class Program {
    static void Main(string[] args) {
        RouteTree routeTree = new RouteTree();
        routeTree.AddRoute('a', 'b', 40, 20d);
        routeTree.AddRoute('a', 'c', 20, 20d);
        routeTree.AddRoute('a', 'd', 20, 20d);
        routeTree.AddRoute('c', 'b', 20, 20d);
        routeTree.AddRoute('b', 'e', 20, 20d);
        routeTree.AddRoute('d', 'e', 20, 20d);

        routeTree.PrintTree();
        Console.WriteLine( routeTree.HoleStreckeGewicht('a', 'b'));
    }
}