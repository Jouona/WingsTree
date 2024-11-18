namespace WingsGmbH;

public interface IConvertToRouteTree {
    RouteTree? ConvertToRouteTree();
}

public class Array2DConverter : IConvertToRouteTree {
    char[][] array;
    int[][] weightMask;
    double[][] costMask;

    public Array2DConverter(char[][] array, int[][] weightMask, double[][] costMask) {
        this.array = array;
        this.weightMask = weightMask;
        this.costMask = costMask;
    }

    public RouteTree? ConvertToRouteTree() {
        if (!Validate()){
            Console.WriteLine("Invalid masks");
            return null;}

        RouteTree routeTree = new RouteTree();
        for (int row = 0; row < array.Length; row++) {
            for (int col = 0; col < array[row].Length; col++) {
                if (col + 1 >= array[row].Length) {
                    continue;
                }

                routeTree.AddRouteOneWay(array[row][col], array[row][col + 1], weightMask[row][col],
                    costMask[row][col]);
            }
        }

        return routeTree;
    }

    bool Validate() {
        if (array.Length != weightMask.Length ||
            array.Length != costMask.Length) return false;

        for (int row = 0; row < array.Length; row++) {
            if (array[row].Length -1 != weightMask[row].Length || 
                array[row].Length -1 != costMask[row].Length) return false;
        }
        return true;
    }
}