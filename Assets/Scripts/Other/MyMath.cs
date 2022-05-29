public class MyMath
{
    public static int Gcd(int a, int b)
    {
        return a > b ? GcdRecursive(a, b) : GcdRecursive(b, a);
    }

    private static int GcdRecursive(int a, int b)
    {
        return b == 0 ? a : GcdRecursive(b, a % b);
    }

    public static int Clamp(int val, int max, int min)
    {

        if (max < val)
            val = max;
        else if (val < min)
            val = min;
        
        return val;
    }

    public static float Clamp(float val, float max, float min)
    {

        if (max < val)
            val = max;
        else if (val < min)
            val = min;

        return val;
    }
}
