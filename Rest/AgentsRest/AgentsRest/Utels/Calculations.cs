namespace AgentsRest.Utels
{
    public class Calculations
    {
        //מחשב את המרחק בין הסוכן למטרה
        public static double DistanceCalculation(int xA,int yA,int xT,int yT) =>
            Math.Sqrt(Math.Pow(xT - xA, 2) + Math.Pow(yT - yA, 2));

    }
}
