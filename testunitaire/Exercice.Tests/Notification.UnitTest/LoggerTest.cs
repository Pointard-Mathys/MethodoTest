using FluentAssertions;
using Notification.Services;

namespace Notification.UnitTest;

public class LoggerTest
{
    [Fact]
    public void Logger_ShouldStoreLogs()
    {
        var logger = new Logger();
        logger.LogInfo("OK");
        logger.LogWarning("Be careful");
        logger.LogError("Oops");

        logger.Logs.Should().HaveCount(3);
        logger.Logs.Should().Contain(x => x.Contains("[INFO] OK"));
        logger.Logs.Should().Contain(x => x.Contains("[WARN] Be careful"));
        logger.Logs.Should().Contain(x => x.Contains("[ERROR] Oops"));
    }
}
