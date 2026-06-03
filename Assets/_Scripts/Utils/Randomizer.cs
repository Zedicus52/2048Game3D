using System;

public sealed class Randomizer
{
    public float Value => (float)_random.NextDouble();

    private Random _random;
    private int _seed;

    public Randomizer() : this(DateTime.Now.Millisecond)
    {

    }
    public Randomizer(int seed)
    {
        _seed = seed;
        _random = new Random(seed);
    }

    public void ReinitRandom(int seed)
    {
        _seed = seed;
        _random = new Random(seed);
    }

    public void ReinitRandom()
    {
        ReinitRandom(DateTime.Now.Millisecond);
    }

    public int GetRandom(int min, int max)
    {
        return _random.Next(min, max);
    }

    public float GetRandom(float min, float max)
    {
        return (float)_random.NextDouble() * (max - min) + min; ;
    }
}
