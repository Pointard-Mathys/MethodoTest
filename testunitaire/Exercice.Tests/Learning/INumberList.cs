namespace Learning;

public interface INumberList
{
    public void Add(int number);
    public bool Remove(int number);
    public void Clear();
    public int Count();
    public int Sum();
    public int Max();
    public int Min();
    public double Average();
    
    public bool Contains(int number);
    
}