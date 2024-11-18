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
                flightToStr += route.Value.To.Letter + " ";
            }

            Console.WriteLine($"{node.Value.ThisLocation.Letter} --> {flightToStr}");
        }
    }

    RouteNode GetOrAddNode(Location location) {
        var node = nodes.GetValueOrDefault(location.Letter);

        if (node == null) {
            node = new RouteNode(location);
            nodes.Add(location.Letter, node);
        }

        return node;
    }

    class RouteNode {
        public readonly Location ThisLocation;
        public Dictionary<char, Route> FlightTo { get; } = new();

        public RouteNode(Location thisLocation) {
            this.ThisLocation = thisLocation;
        }

        public void AddRoute(RouteNode to, int weight, double cost) {
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