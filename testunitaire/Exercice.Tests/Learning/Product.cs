namespace Learning;

public class Product : IProduct
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Category { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public List<string> Tags { get; set; } = new List<string>();
    
    public bool IsExpensive ()
        => Price > 100;
    
    public bool IsNew () 
        => (DateTime.Now - CreatedAt).TotalDays <= 30;
    
    public void ApplyDiscount(decimal percentage)
    {
        if (percentage < 0 || percentage > 100)
            throw new ArgumentException("Le pourcentage doit être entre 0 et 100");
        
        Price = Price * (1 - percentage / 100);
    }
}