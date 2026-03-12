// file="Performance.cs" 
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using TechTalk.SpecFlow.Infrastructure;

namespace GalaxyCloud.Helpers
{
    /// <summary>
    /// This class implements methods that works with performance measures
    /// such as time, memory spend and CPU usage by app
    /// </summary>
    public class Performance
    {
        // Performance test const variables
        public readonly List<float> actionTime = new List<float>();
        public readonly List<long> memoryConsumed = new List<long>();
        public readonly List<float> cpuConsumed = new List<float>();

        // Performance test variables
        public Stopwatch stopwatch = new Stopwatch();
        public float averageActionTime;
        public float averageMemory;
        public float averageCPU;

        /// <summary>
        /// Variable used to write on Specflow HTML report
        /// </summary>
        private readonly ISpecFlowOutputHelper outputHelper;

        /// <summary>
        /// Class constructor with Specflow Context Injection
        /// </summary>
        /// <param name="outputHelper">Specflow Context Injection Object</param>
        public Performance(ISpecFlowOutputHelper outputHelper = null)
        {
            this.outputHelper = outputHelper;
        }

        /// <summary>
        /// Get average time spent
        /// </summary>
        /// <returns>Returns average time value in milliseconds (float)</returns>
        public float GetAverageTime()
        {
            return averageActionTime;
        }

        /// <summary>
        /// Get average memory spent
        /// </summary>
        /// <returns>Returns average memory spent in MB (float)</returns>
        public float GetAverageMemory()
        {
            return averageMemory;
        }

        /// <summary>
        /// Get average CPU usage
        /// </summary>
        /// <returns>Returns average CPU usage in MB (float)</returns>
        public float GetAverageCPU()
        {
            return averageCPU;
        }

        /// <summary>
        /// Get memory used from app process
        /// </summary>
        /// <param name="processName">App process name. Same name as displayed at windows task manager</param>
        /// <returns>Returns memory used by app in MB</returns>
        public long GetMemoryUsage(string processName)
        {
            long memoryUsed;
            // Get local machine memory process value
            Process[] processArray = Process.GetProcessesByName(processName);

            // Remote
            // Process[] processArray = Process.GetProcessesByName(processName, "Ip URl");

            // Populate memoryUsed variable with live message memory
            if (processArray != null)
            {
                Process liveProcess = processArray[0];
                // Convert bytes to MB
                memoryUsed = liveProcess.WorkingSet64 / 1000000;
            }
            else
            {
                memoryUsed = -1;
            }

            return memoryUsed;
        }

        /// <summary>
        /// Get CPU used by app
        /// </summary>
        /// <param name="processName">App process name. Same name as displayed at windows task manager</param>
        /// <returns>Returns CPU percentual consumed by app while running a routine</returns>
        public float GetCPUPercentual(string processName)
        {
            PerformanceCounter cpuCounter = new PerformanceCounter
            {
                CategoryName = "Process",
                CounterName = "% Processor Time",
                InstanceName = processName
            };

            // First measure will always be 0
            _ = cpuCounter.NextValue();
            Thread.Sleep(1000);
            // Actual mesured value
            float value = cpuCounter.NextValue();

            return value;
        }

        /// <summary>
        /// Log Values Into SpecflowHTML report
        /// </summary>
        /// <param name="outputHelper">Instance of Output Helper object.</param>
        public void LogPerformanceInfoIntoHTMLReport(ISpecFlowOutputHelper outputHelper)
        {
            // Calculates the average time for sign in action to be performed
            averageActionTime = actionTime.Sum() / actionTime.Count;

            outputHelper.WriteLine("------------------------------------------------");
            outputHelper.WriteLine($"The average Time was: {GetAverageTime()} milliseconds");
            outputHelper.WriteLine("------------------------------------------------");
            for (int i = 0; i < actionTime.Count; i++)
            {
                outputHelper.WriteLine($"Time measured at {i + 1} interaction was:  {actionTime[i]} milliseconds");
            }

            outputHelper.WriteLine("------------------------------------------------");
        }
    }
}
