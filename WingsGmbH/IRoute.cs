public interface IRoute {
    Location To { get; }
    int Weight { get; }
    double Cost { get; }
}