using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;

namespace Application.BoundedConext.Tests
{
    [TestClass]
    public class AssemblyTestsInitialize
    {
        private const string _windowsAzureStorageEmulatorPath = @"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe";
        private const string _win7ProcessName = "WAStorageEmulator";
        private const string _win8ProcessName = "WASTOR~1";
        private static readonly ProcessStartInfo startStorageEmulator = new ProcessStartInfo
        {
            FileName = _windowsAzureStorageEmulatorPath,
            Arguments = "start",
        };

        private static readonly ProcessStartInfo stopStorageEmulator = new ProcessStartInfo
        {
            FileName = _windowsAzureStorageEmulatorPath,
            Arguments = "stop",
        };

        [AssemblyInitialize]
        public static void LaunchEmulator(TestContext context)
        {
             StartStorageEmulator();
        }


        private static Process GetProcess()
        {
            return Process.GetProcessesByName(_win7ProcessName).FirstOrDefault() ?? Process.GetProcessesByName(_win8ProcessName).FirstOrDefault();
        }

        private static bool IsProcessStarted()
        {
            return GetProcess() != null;
        }

        private static void StartStorageEmulator()
        {
            if (!IsProcessStarted())
            {
                using (Process process = Process.Start(startStorageEmulator))
                {
                    process.WaitForExit();
                }
            }
        }


        private static void StopStorageEmulator()
        {
            using (Process process = Process.Start(stopStorageEmulator))
            {
                process.WaitForExit();
            }
        }

    }
}
