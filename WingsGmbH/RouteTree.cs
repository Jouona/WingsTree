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
            if (containsKey){
                Console.WriteLine($"Route already exists: {ThisLocation.Letter} --> {FlightTo[to.ThisLocation.Letter].To.Letter}");
                return;}
            this.FlightTo.Add(to.ThisLocation.Letter, new Route(to.ThisLocation, weight, cost));
        }
    }
}

public struct Route(Location to, int weight, double cost) : IRoute {
    public Location To { get; } = to;
    public int Weight { get; } = weight;
    public double Cost { get; } = cost;
}

public interface IRoute {
    Location To { get; }
    int Weight { get; }
    double Cost { get; }
}