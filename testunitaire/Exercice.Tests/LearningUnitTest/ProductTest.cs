using FluentAssertions;
using Learning;

namespace LearningUnitTest;

public class ProductTest
{
    /// <summary>
    /// Vérifie que les valeurs par défaut d'une instance de Product sont correctement initialisées.
    /// </summary>
    [Fact]
    public void Product_DefaultValues_ShouldBeCorrect()
    {
        var product = new Product();

        product.Id.Should().Be(0);
        product.Name.Should().BeNull();
        product.Price.Should().Be(0);
        product.Category.Should().BeNull();
        product.CreatedAt.Should().Be(default(DateTime));
        product.IsActive.Should().BeFalse();
        product.Tags.Should().NotBeNull().And.BeEmpty();
    }

    /// <summary>
    /// Vérifie que la méthode IsExpensive retourne la bonne valeur en fonction de différents prix.
    /// </summary>
    [Theory]
    [InlineData(50, false)]
    [InlineData(100, false)]
    [InlineData(101, true)]
    [InlineData(200.50, true)]
    public void IsExpensive_DifferentPrices_ShouldReturnCorrectResult(decimal price, bool expected)
    {
        var product = new Product { Price = price };
        product.IsExpensive().Should().Be(expected);
    }

    /// <summary>
    /// Vérifie que IsNew retourne true pour un produit créé récemment (moins de 30 jours).
    /// </summary>
    [Fact]
    public void IsNew_RecentProduct_ShouldReturnTrue()
    {
        var product = new Product { CreatedAt = DateTime.Now.AddDays(-15) };
        product.IsNew().Should().BeTrue();
    }

    /// <summary>
    /// Vérifie que IsNew retourne false pour un produit créé il y a plus de 30 jours.
    /// </summary>
    [Fact]
    public void IsNew_OldProduct_ShouldReturnFalse()
    {
        var product = new Product { CreatedAt = DateTime.Now.AddDays(-45) };
        product.IsNew().Should().BeFalse();
    }

    /// <summary>
    /// Vérifie que ApplyDiscount applique correctement une réduction valide sur le prix.
    /// </summary>
    [Theory]
    [InlineData(10, 90)]
    [InlineData(25, 75)]
    [InlineData(50, 50)]
    public void ApplyDiscount_ValidPercentage_ShouldReducePrice(decimal discount, decimal expectedPrice)
    {
        var product = new Product { Price = 100 };
        product.ApplyDiscount(discount);
        product.Price.Should().Be(expectedPrice);
    }

    /// <summary>
    /// Vérifie que des pourcentages de réduction invalides lèvent une exception ArgumentException.
    /// </summary>
    [Theory]
    [InlineData(-5)]
    [InlineData(101)]
    [InlineData(150)]
    public void ApplyDiscount_InvalidPercentage_ShouldThrowArgumentException(decimal discount)
    {
        var product = new Product { Price = 100 };

        Action act = () => product.ApplyDiscount(discount);

        act.Should().Throw<ArgumentException>()
           .WithMessage("Le pourcentage doit être entre 0 et 100");
    }

    /// <summary>
    /// Teste les opérations sur les tags : ajout, ordre, contenu et validation.
    /// </summary>
    [Fact]
    public void Tags_Operations_ShouldWorkWithFluentAssertions()
    {
        var product = new Product();
        var expectedTags = new[] { "electronics", "mobile", "smartphone" };

        foreach (var tag in expectedTags)
        {
            product.Tags.Add(tag);
        }

        product.Tags.Should().HaveCount(3);
        product.Tags.Should().BeEquivalentTo(expectedTags);
        product.Tags.Should().ContainInOrder("electronics", "mobile", "smartphone");
        product.Tags.Should().AllSatisfy(tag => tag.Should().NotBeNullOrEmpty());
    }

    /// <summary>
    /// Vérifie que IsNew retourne true pour un produit créé aujourd'hui.
    /// </summary>
    [Fact]
    public void IsNew_CreatedToday_ReturnsTrue()
    {
        var product = new Product { CreatedAt = DateTime.Now };
        product.IsNew().Should().BeTrue();
    }

    /// <summary>
    /// Vérifie que IsNew retourne true pour un produit créé il y a exactement 30 jours.
    /// </summary>
    [Fact]
    public void IsNew_CreatedExactly30DaysAgo_ReturnsTrue()
    {
        var product = new Product
        {
            CreatedAt = DateTime.Now.Date.AddDays(-30) // ⚠️ .Date fixe à 00:00:00
        };

        product.IsNew().Should().BeTrue();
    }

    /// <summary>
    /// Vérifie que IsNew retourne false pour un produit créé il y a 31 jours.
    /// </summary>
    [Fact]
    public void IsNew_Created31DaysAgo_ReturnsFalse()
    {
        var product = new Product { CreatedAt = DateTime.Now.AddDays(-31) };
        product.IsNew().Should().BeFalse();
    }

    /// <summary>
    /// Vérifie que IsNew retourne true pour un produit avec une date de création future.
    /// </summary>
    [Fact]
    public void IsNew_CreatedInFuture_ReturnsTrue()
    {
        var product = new Product { CreatedAt = DateTime.Now.AddDays(5) };
        product.IsNew().Should().BeTrue(); // Intentional: future dates are still "new"
    }

    /// <summary>
    /// Applique une réduction à différents prix de départ et vérifie le résultat attendu.
    /// </summary>
    [Theory]
    [InlineData(200, 25, 150)]
    [InlineData(100, 0, 100)]
    [InlineData(100, 100, 0)]
    public void ApplyDiscount_ValidPercentages_UpdatesPrice(decimal initialPrice, decimal discount, decimal expectedPrice)
    {
        var product = new Product { Price = initialPrice };
        product.ApplyDiscount(discount);
        product.Price.Should().Be(expectedPrice);
    }

    /// <summary>
    /// Vérifie qu'une exception est levée pour des valeurs de réduction hors limites.
    /// </summary>
    [Theory]
    [InlineData(-1)]
    [InlineData(-100)]
    [InlineData(101)]
    [InlineData(200)]
    public void ApplyDiscount_InvalidPercentages_ThrowsException(decimal discount)
    {
        var product = new Product { Price = 100 };
        Action act = () => product.ApplyDiscount(discount);
        act.Should().Throw<ArgumentException>();
    }
}
