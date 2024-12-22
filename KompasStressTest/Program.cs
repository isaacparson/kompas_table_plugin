using System.Diagnostics;
using ApiLogic;
using ParametersLogic;
using System.Management;
using System.CodeDom.Compiler;
using System.IO;

static void main()
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

    var builder = new Builder(parameters, Cad.Kompas);
    var stopWatch = new Stopwatch();
    var count = 0;
    const double gigabyteInByte = 0.000000000931322574615478515625;
    ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
    ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(objectQuery);
    ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
    var managementObject = managementObjectCollection.GetEnumerator().Current;

    while (true)
    {
        stopWatch.Start();
        builder.Build();
        stopWatch.Stop();
        var totalMemory = double.Parse(managementObject["TotalVisibleMemorySize"].ToString());
        var freeMemory = double.Parse(managementObject["FreePhysicalMemory"].ToString());
        var usedMemory = (totalMemory - freeMemory) * gigabyteInByte;
        Console.WriteLine($"{++count}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
        stopWatch.Reset();
    }
    //Console.Write($"End {new ComputerInfo().TotalPhysicalMemory}");
}