namespace Learning;

public class NumberList : INumberList
{
    private List<int> _numbers = new List<int>();

    public void Add(int number)
    {
        _numbers.Add(number);
    }

    public bool Remove(int number)
    {
       return _numbers.Remove(number);
    }

    public void Clear()
    {
        _numbers.Clear();
    }

    public int Count()
    {
        return _numbers.Count;
    }

    public int Sum()
    {
        return _numbers.Sum();
    }

    public int Max()
    {
        return _numbers.Max();
    }

    public int Min()
    {
        return _numbers.Min();
    }

    public double Average()
    {
        return _numbers.Average();
    }

    public bool Contains(int number)
    {
        return _numbers.Contains(number);
    }
}