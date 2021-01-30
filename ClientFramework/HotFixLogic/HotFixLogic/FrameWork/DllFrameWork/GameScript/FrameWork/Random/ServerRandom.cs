/// <summary>
/// 服务器随机数
/// </summary>
using System;
public class ServerRandom
{
    public static System.Random randomInstance;

    public static int GetRandomIntValue(int min, int max)
    {
        if (randomInstance == null)
        {
            randomInstance = new System.Random();
        }

        return randomInstance.Next(min, max);
    }

    public static int GetRandomIntValue(double min, double max)
    {
        if (randomInstance == null)
        {
            randomInstance = new System.Random();
        }

        double returnValue = max - min;

        returnValue = returnValue * randomInstance.NextDouble();

        returnValue = Math.Round(returnValue);

        if (returnValue < min)
        {
            returnValue = min;
        }

        return (int)returnValue;
    }

    public static double GetRandomDoubleValue(double min, double max)
    {
        if (randomInstance == null)
        {
            randomInstance = new System.Random();
        }

        double returnValue = max - min;

        return returnValue * randomInstance.NextDouble();
    }
}
