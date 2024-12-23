using System.Diagnostics;
using ApiLogic;
using ParametersLogic;
using System.Management;
using System.CodeDom.Compiler;
using System.IO;

namespace InventorStressTest
{
    internal static class Program
    {
        static void Main()
        {
            int topWidth = 1000;
            int topDepth = 500;
            int topHeight = 28;
            int legsWidth = 20;
            int tableHeight = 500;

            var dict = new Dictionary<ParamType, int>();
            dict.Add(ParamType.TopWidth, topWidth);
            dict.Add(ParamType.TopDepth, topDepth);
            dict.Add(ParamType.TopHeight, topHeight);
            dict.Add(ParamType.LegWidth, legsWidth);
            dict.Add(ParamType.TableHeight, tableHeight);

            var parameters = new Parameters();
            parameters.SetParameters(dict);

            var builder = new Builder(parameters, Cad.AutoCad);
            var stopWatch = new Stopwatch();
            var count = 0;

            while (true)
            {
                ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                var enumerator = managementObjectCollection.GetEnumerator();
                enumerator.MoveNext();
                var managementObject = enumerator.Current;

                stopWatch.Start();
                builder.Build();
                stopWatch.Stop();
                var totalMemory = double.Parse(managementObject["TotalVisibleMemorySize"].ToString()) / 1024 / 1024;
                var freeMemory = double.Parse(managementObject["FreePhysicalMemory"].ToString()) / 1024 / 1024;
                var usedMemory = (totalMemory - freeMemory);
                Console.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                stopWatch.Reset();
            }
            {
                ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                var enumerator = managementObjectCollection.GetEnumerator();
                enumerator.MoveNext();
                var managementObject = enumerator.Current;
                Console.Write($"End {double.Parse(managementObject["TotalVisibleMemorySize"].ToString()) / 1024 / 1024}");
            }
        }
    }
}

