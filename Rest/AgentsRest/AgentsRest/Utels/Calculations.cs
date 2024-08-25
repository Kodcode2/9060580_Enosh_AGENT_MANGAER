namespace AgentsRest.Utels
{
    public class Calculations
    {
        //מחשב את המרחק בין הסוכן למטרה
        public static double DistanceCalculation(int xA, int yA, int xT, int yT) =>
            Math.Sqrt(Math.Pow(xT - xA, 2) + Math.Pow(yT - yA, 2));
        // בודק האם הוא בטווח של הלוח
        public static bool IsInRange1000(int x, int y)
        {
            return (x > 0 && x < 1000) && (y > 0 && y < 1000);
        }

    }
}
