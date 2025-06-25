using System.Text.RegularExpressions;
using FluentAssertions;
using Notification.Contracts;
using Notification.Services;

namespace Notification.UnitTest;

// Classe de test unitaire pour le service SMS
public class SmsServiceTest
{
    // Test de la méthode de validation de numéro de téléphone
    [Fact]
    public void IsValidPhoneNumber_ShouldValidateCorrectly()
    {
        var service = new SmsService();

        // Vérifie qu'un numéro valide (format international français) est reconnu comme valide
        service.IsValidPhoneNumber("+33612345678").Should().BeTrue();

        // Vérifie qu'un numéro invalide est rejeté
        service.IsValidPhoneNumber("badphone").Should().BeFalse();
    }

    // Test d'intégration simulé pour l'envoi d'un SMS
    [Fact]
    public async Task SendSmsAsync_ShouldReturnTrue()
    {
        var service = new SmsService();

        // Envoie un SMS fictif et vérifie que le résultat est positif (true)
        var result = await service.SendSmsAsync("+33612345678", "Hello");

        // Assertion : le SMS doit être considéré comme envoyé avec succès
        result.Should().BeTrue();
    }
}