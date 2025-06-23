using FluentAssertions;
using Learning;

namespace LearningUnitTest;

public class ProductTest
{
    [Fact]
    public void Product_DefaultValues_ShouldBeCorrect()
    {
        // Arrange & Act
        var product = new Product();

        // Assert with FluentAssertions
        product.Id.Should().Be(0);
        product.Name.Should().BeNull();
        product.Price.Should().Be(0);
        product.Category.Should().BeNull();
        product.CreatedAt.Should().Be(default(DateTime));
        product.IsActive.Should().BeFalse();
        product.Tags.Should().NotBeNull().And.BeEmpty();
    }
    
      [Theory]
    [InlineData(50, false)]
    [InlineData(100, false)]
    [InlineData(101, true)]
    [InlineData(200.50, true)]
    public void IsExpensive_DifferentPrices_ShouldReturnCorrectResult(decimal price, bool expected)
    {
        // Arrange
        var product = new Product { Price = price };

        // Act & Assert with FluentAssertions
       
    }

    [Fact]
    public void IsNew_RecentProduct_ShouldReturnTrue()
    {
        // Arrange
        var product = new Product { CreatedAt = DateTime.Now.AddDays(-15) };

        // Act & Assert with FluentAssertions
    }

    [Fact]
    public void IsNew_OldProduct_ShouldReturnFalse()
    {
        // Arrange
        var product = new Product { CreatedAt = DateTime.Now.AddDays(-45) };

        // Act & Assert with FluentAssertions
      
    }

    [Theory]
    [InlineData(10, 90)]
    [InlineData(25, 75)]
    [InlineData(50, 50)]
    public void ApplyDiscount_ValidPercentage_ShouldReducePrice(decimal discount, decimal expectedPrice)
    {

    }

    [Theory]
    [InlineData(-5)]
    [InlineData(101)]
    [InlineData(150)]
    public void ApplyDiscount_InvalidPercentage_ShouldThrowArgumentException(decimal discount)
    {
        // Arrange
        var product = new Product { Price = 100 };

        // Act & Assert with FluentAssertions
        Action act = () => product.ApplyDiscount(discount);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Le pourcentage doit être entre 0 et 100");
    }

    [Fact]
    public void Tags_Operations_ShouldWorkWithFluentAssertions()
    {
        // Arrange
        var product = new Product();
        var expectedTags = new[] { "electronics", "mobile", "smartphone" };

        // Act
        foreach (var tag in expectedTags)
        {
            product.Tags.Add(tag);
        }

        // Assert with FluentAssertions
        product.Tags.Should().HaveCount(3);
        product.Tags.Should().BeEquivalentTo(expectedTags);
        product.Tags.Should().ContainInOrder("electronics", "mobile", "smartphone");
        product.Tags.Should().AllSatisfy(tag => tag.Should().NotBeNullOrEmpty());
    }
}