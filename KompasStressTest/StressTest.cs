﻿using System.Diagnostics;
using ApiLogic;
using ParametersLogic;
using System.Management;
using System.CodeDom.Compiler;
using System.IO;

namespace KompasStressTest
{
    internal static class StressTest
    {
        static void Main()
        {
            double gbytesInKbytes = 0.00000095367;

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

            var builder = new Builder(parameters, Cad.Kompas);
            var stopWatch = new Stopwatch();
            var count = 0;

            string path = @"..\\..\\..\\..\\docs\\kompas_log.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Create(path).Close();

            StreamWriter streamWriter = new StreamWriter(path);

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
                var totalMemory = double.Parse(managementObject["TotalVisibleMemorySize"].ToString()) * gbytesInKbytes;
                var freeMemory = double.Parse(managementObject["FreePhysicalMemory"].ToString()) * gbytesInKbytes;
                var usedMemory = (totalMemory - freeMemory);
                streamWriter.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
                stopWatch.Reset();
                System.Threading.Thread.Sleep(50);
            }
            {
                ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
                ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
                var enumerator = managementObjectCollection.GetEnumerator();
                enumerator.MoveNext();
                var managementObject = enumerator.Current;
                streamWriter.Write($"End {double.Parse(managementObject["TotalVisibleMemorySize"].ToString()) * gbytesInKbytes}");
                streamWriter.Flush();
            }
        }
    }
}

