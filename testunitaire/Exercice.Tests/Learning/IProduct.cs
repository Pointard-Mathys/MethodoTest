namespace Learning;

public interface IProduct
{
    /// <summary>
    /// Determines if the product is considered expensive based on its internal criteria.
    /// </summary>
    /// <returns>A boolean value indicating whether the product is expensive.</returns>
    public bool IsExpensive();

    /// <summary>
    /// Checks if the product is newly introduced or recognized as recent within its category.
    /// </summary>
    /// <returns>A boolean value indicating whether the product is new.</returns>
    public bool IsNew();

    /// <summary>
    /// Applies a discount to the product based on the specified percentage.
    /// </summary>
    /// <param name="percentage">The discount percentage to be applied to the product.</param>
    public void ApplyDiscount(decimal percentage);
}