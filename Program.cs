using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Extensions.Logging;
using nanoFramework.Logging;
using nanoFramework.Logging.Debug;
using nanoFramework.Logging.Serial;
using Windows.Devices.SerialCommunication;
using nanoFramework.Hardware.Esp32;

namespace DependencyProblemTest
{
    public class Program
    {
        private static DebugLogger _logger;

        public static void Main()
        {
            Debug.WriteLine("Hello from Debug.WriteLine!");
            _logger = new DebugLogger("Example");
            _logger.MinLogLevel = LogLevel.Trace;
            _logger.LogInformation("Hello from nanoFramework!");
            _logger.Log(LogLevel.Information, 0, "this is state???", null, null);
            _logger.LogTrace("Trace: the Debug Logger is initialized");
            _logger.LogInformation($"Logger name is: {_logger.LoggerName}, you can use that to trace which component is used");
            _logger.LogInformation("The next call to the class will log as well");
            _logger.LogInformation("For this component, we're using the Logger Factory pattern. It will use the debugger as well");

            LogDispatcher.LoggerFactory = new DebugLoggerFactory();
            MyTestComponent test = new MyTestComponent();
            do
            {
                test.DoSomeLogging();
            } while (true);

            //Thread.Sleep(Timeout.Infinite);

            // Browse our samples repository: https://github.com/nanoframework/samples
            // Check our documentation online: https://docs.nanoframework.net/
            // Join our lively Discord community: https://discord.gg/gCyBu8T
        }
    }

    internal class MyTestComponent
    {
        private readonly ILogger _logger;

        public MyTestComponent()
        {
            _logger = this.GetCurrentClassLogger();
        }

        public void DoSomeLogging()
        {
            _logger.LogInformation("An informative message");
            _logger.LogError("An error situation");
            _logger.LogWarning(new Exception("Something is not supported"), "With exception context");
        }
    }

}
