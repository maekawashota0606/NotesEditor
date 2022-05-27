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
}
