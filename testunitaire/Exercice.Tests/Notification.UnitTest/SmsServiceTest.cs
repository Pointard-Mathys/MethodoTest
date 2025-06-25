using System.Text.RegularExpressions;
using FluentAssertions;
using Notification.Contracts;
using Notification.Services;

namespace Notification.UnitTest;

public class SmsServiceTest
{
    [Fact]
    public void IsValidPhoneNumber_ShouldValidateCorrectly()
    {
        var service = new SmsService();

        service.IsValidPhoneNumber("+33612345678").Should().BeTrue();
        service.IsValidPhoneNumber("badphone").Should().BeFalse();
    }

    [Fact]
    public async Task SendSmsAsync_ShouldReturnTrue()
    {
        var service = new SmsService();
        var result = await service.SendSmsAsync("+33612345678", "Hello");

        result.Should().BeTrue();
    }
}

