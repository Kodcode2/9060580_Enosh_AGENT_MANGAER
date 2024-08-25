namespace ClientAgretTarget.Utels
{
    public class Calculations
    {
        public static double DistanceCalculation(int xA, int yA, int xT, int yT) =>
            Math.Sqrt(Math.Pow(xT - xA, 2) + Math.Pow(yT - yA, 2));
    }
}
