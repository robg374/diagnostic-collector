﻿namespace KFlow
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    // Implements Utility Library
    using Implements;


    public static class Global
    {
        /// <summary>
        /// Initialize Global -- ensure configs are loaded
        /// </summary>
        static Global()
        {
                GetConfigs();
                SetKFlowConfig();
        }

        /// <summary>
        /// Deserialize Config.ini, load kflow and kiroku objects into backing fields
        /// </summary>
        public static void GetConfigs()
        {
            using (Deserializer deserilaizer = new Deserializer())
            {
                var _path = Directory.GetCurrentDirectory() + @"\Config.ini";
                var _path2 = @"E:\02_CLOUD\GitHub\PlatformDiagnosticCollector\Kiroku\kiroku-library\ExampleConsole\Config.ini";

                deserilaizer.Execute(_path);

                _kflowTagList = deserilaizer.GetTag("kflow");

                _kirokuTagList = deserilaizer.GetTag("kiroku");
            }
        }

        /// <summary>
        /// Load raw kflow config through switch tree, sorting and loading into the backing fields
        /// </summary>
        private static void SetKFlowConfig()
        {
            foreach (var kvp in KFlowTagList)
            {
                switch (kvp.Key.ToString())
                {
                    case "instanceloop":
                        _instanceloop = kvp.Value;
                        break;
                    case "blockloop":
                        _blockloop = kvp.Value;
                        break;
                    case "trace":
                        _trace = kvp.Value;
                        break;
                    case "traceloop":
                        _traceloop = kvp.Value;
                        break;
                    case "tracechar":
                        _tracechar = kvp.Value;
                        break;
                    case "info":
                        _info = kvp.Value;
                        break;
                    case "infoloop":
                        _infoloop = kvp.Value;
                        break;
                    case "infochar":
                        _infochar = kvp.Value;
                        break;
                    case "warning":
                        _warning = kvp.Value;
                        break;
                    case "warningloop":
                        _warningloop = kvp.Value;
                        break;
                    case "warningchar":
                        _warningchar = kvp.Value;
                        break;
                    case "error":
                        _error = kvp.Value;
                        break;
                    case "errorloop":
                        _errorloop = kvp.Value;
                        break;
                    case "errorchar":
                        _errorchar = kvp.Value;
                        break;

                    default:
                        {
                            System.Console.WriteLine("Not Hit: {0}", kvp.Value);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Public readonly properties, backing fields applied with proper conversion
        /// </summary>

        // Configs
        private static List<KeyValuePair<string, string>> KFlowTagList { get { return _kflowTagList; } }

        public static List<KeyValuePair<string, string>> KirokuTagList { get { return _kirokuTagList; } }

        // Main
        public static int InstanceLoop { get { return ConvertValueToInt(_instanceloop); } }
        public static int BlockLoop { get { return ConvertValueToInt(_blockloop); } }

        // Trace
        public static bool TraceOn { get { return ConvertValueToBool(_trace); } }
        public static int TraceLoopCount { get { return ConvertValueToInt(_traceloop); } }
        public static int TraceCharCount { get { return ConvertValueToInt(_tracechar); } }

        // Info
        public static bool InfoOn { get { return ConvertValueToBool(_info); } }
        public static int InfoLoopCount { get { return ConvertValueToInt(_infoloop); } }
        public static int InfoCharCount { get { return ConvertValueToInt(_infochar); } }

        // Warning
        public static bool WarningOn { get { return ConvertValueToBool(_warning); } }
        public static int WarningLoopCount { get { return ConvertValueToInt(_warningloop); } }
        public static int WarningCharCount { get { return ConvertValueToInt(_warningchar); } }

        // Error
        public static bool ErrorOn { get { return ConvertValueToBool(_error); } }
        public static int ErrorLoopCount { get { return ConvertValueToInt(_errorloop); } }
        public static int ErrorCharCount { get { return ConvertValueToInt(_errorchar); } }

        /// <summary>
        /// Private backing fields
        /// </summary>
        
        // Configs
        private static List<KeyValuePair<string, string>> _kflowTagList;
        private static List<KeyValuePair<string, string>> _kirokuTagList;

        // Main
        private static string _instanceloop;
        private static string _blockloop;

        // Trace
        private static string _trace;
        private static string _traceloop;
        private static string _tracechar;

        // Info
        private static string _info;
        private static string _infoloop;
        private static string _infochar;

        // Warning
        private static string _warning;
        private static string _warningloop;
        private static string _warningchar;

        // Error
        private static string _error;
        private static string _errorloop;
        private static string _errorchar;

        /// <summary>
        /// Utility to convert string to int
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        private static int ConvertValueToInt(string inputValue)
        {
            int outputValue;

            try
            {
                outputValue = Int32.Parse(inputValue);
            }
            catch
            {
                outputValue = 0;
            }

            return outputValue;
        }

        /// <summary>
        /// Utility to convert string to bool
        /// </summary>
        /// <param name="inputValue"></param>
        /// <returns></returns>
        private static bool ConvertValueToBool(string inputValue)
        {
            bool outputValue;

            if (inputValue == "1")
            {
                outputValue = true;
            }
            else
            {
                outputValue = false;
            }

            return outputValue;
        }
    }
}
