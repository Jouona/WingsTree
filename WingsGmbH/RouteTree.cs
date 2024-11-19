// tree-like structure ist DEUTLICH besser und flexibler als dieses ÄUßERST KOMISCHE 2D Array für die Routen
// deswegen benutzen wir ein ConvertToRouteTree interface, das momentan nur von 2DToRouteTreeConverter implementiert wird
// ----
// die Location Klasse wird hier nicht vernünftig verwendet, es werden immer neue Objekte erstellt, die synonym verwendet werden und diese werden nie gecacht, sehr schlecht für die GarbageCollection
// Lösungsansatz hierfür wäre eine Flyweight-Factory oder die Locations zu cachen
// ----
// im Tree immer die Letters zu benutzen sorgt für sehr lange Referenzen zu den Letters. Bsp: nodes[child.Value.To.Letter]. Das ist etwas unschön und macht es schwerer verständlich
// Letters werden auch oft synonym für Locations verwendet und teilweise für RouteNodes, das ist verwirrend und unschön
// ----
// Ich werde das alles hier aber nicht mehr ändern, es sind aber gute Möglichkeiten, das Programm künftig noch zu verbessern
public class RouteTree {
    Dictionary<char, RouteNode> nodes = new();

    public void AddRoute(char from, char to, int weight, double cost) {
        var fromNode = GetOrAddNode(new Location(from));
        var toNode = GetOrAddNode(new Location(to));
        fromNode.AddRoute(toNode, weight, cost);
        toNode.AddRoute(fromNode, weight, cost);
    }

    public void AddRouteOneWay(char from, char to, int weight, double cost) {
        var fromNode = GetOrAddNode(new Location(from));
        var toNode = GetOrAddNode(new Location(to));
        fromNode.AddRoute(toNode, weight, cost);
    }

    public int HoleStreckeGewicht(char from, char to) {
        return nodes[from].FlightTo[to].Weight;
    }

    public double HoleStreckeKosten(char from, char to) {
        return nodes[from].FlightTo[to].Cost;
    }

    public void PrintTree() {
        foreach (var node in nodes) {
            string flightToStr = "";
            foreach (var route in node.Value.FlightTo) {
                flightToStr += route.Value.To.Letter + $"(weight: {route.Value.Weight}, cost: {route.Value.Cost}) ";
            }

            Console.WriteLine($"{node.Value.ThisLocation.Letter} --> {flightToStr}");
        }
    }

    public List<List<char>> FindPaths(char start, char target) {
        var allPaths = new List<List<char>>();
        var currentPath = new List<char>();

        FindPathsRecursive(nodes[start], start, target, currentPath, allPaths);

        return allPaths;
        
        void FindPathsRecursive(RouteNode root, char start, char target, List<char> currentPath,
            List<List<char>> allPaths) {
            if (root == null) return;

            // Add current node to the path
            currentPath.Add(root.ThisLocation.Letter);

            // Check if we have reached the target and started from the start node
            if (root.ThisLocation.Letter == target && currentPath.Contains(start)) {
                allPaths.Add(new List<char>(currentPath));
            }

            // Traverse children
            foreach (var child in root.FlightTo) {
                FindPathsRecursive(nodes[child.Value.To.Letter], start, target, currentPath, allPaths);
            }

            // Backtrack to explore other paths
            currentPath.RemoveAt(currentPath.Count - 1);
        }
    }

    RouteNode GetOrAddNode(Location location) {
        var containsKey = nodes.ContainsKey(location.Letter);

        if (!containsKey) {
            var node = new RouteNode(location);
            nodes.Add(location.Letter, node);
        }

        return nodes[location.Letter];
    }

    class RouteNode {
        public readonly Location ThisLocation;
        public Dictionary<char, Route> FlightTo { get; } = new();

        public RouteNode(Location thisLocation) {
            this.ThisLocation = thisLocation;
        }

        public void AddRoute(RouteNode to, int weight, double cost) {
            bool containsKey = FlightTo.ContainsKey(to.ThisLocation.Letter);
            if (containsKey) {
                Console.WriteLine(
                    $"Route already exists: {ThisLocation.Letter} --> {FlightTo[to.ThisLocation.Letter].To.Letter}");
                return;
            }

            this.FlightTo.Add(to.ThisLocation.Letter, new Route(to.ThisLocation, weight, cost));
        }
    }
}