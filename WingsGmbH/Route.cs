public struct Route(Location to, int weight, double cost) : IRoute {
    public Location To { get; } = to;
    public int Weight { get; } = weight;
    public double Cost { get; } = cost;
}