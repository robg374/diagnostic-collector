﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Collections.Specialized;
using System.IO;

namespace Kiroku
{
    /// <summary>
    /// CLASS: Starts and Stops Kiroku instance opertaions.
    /// </summary>
    public class KManager
    {
        #region Online

        /// <summary>
        /// Signals the start of KLog operations for an instance. Reading and setting global configuration values. Writing first log entry.
        /// </summary>
        /// <param name="hostValueCollection"></param>
        public static void Online(NameValueCollection hostValueCollection)
        {
            // Prase App.Config\Kiroku and Set
            if (hostValueCollection != null)
            {
                foreach (var configKey in hostValueCollection.AllKeys)
                {
                    string configValue = hostValueCollection.GetValues(configKey).FirstOrDefault().ToString();

                    switch (configKey.ToString())
                    {
                        case "local":
                            LogConfiguration.Local = configValue;
                            break;

                        case "applicationid":
                            LogConfiguration.ApplicationID = new Guid(configValue);
                            break;

                        case "trackid":
                            LogConfiguration.TrackID = new Guid(configValue);
                            break;

                        case "verbose":
                            LogConfiguration.Verbose = configValue;
                            break;

                        case "trace":
                            LogConfiguration.Trace = configValue;
                            break;

                        case "info":
                            LogConfiguration.Info = configValue;
                            break;

                        case "warning":
                            LogConfiguration.Warning = configValue;
                            break;

                        case "error":
                            LogConfiguration.Error = configValue;
                            break;

                        case "filepath":
                            LogConfiguration.RootFilePath = configValue;
                            break;

                        default:
                            {
                                System.Console.WriteLine("Not Hit: {0}", configKey);
                            }
                            break;
                    }
                }

                // Rest of Setup
                // Set Session Date
                LogConfiguration.Datetime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
                // Set Instance GUID
                LogConfiguration.InstanceID = Guid.NewGuid();
                // Creat File Path
                LogConfiguration.FullFilePath = LogConfiguration.RootFilePath + LogType.WritingToLog + LogConfiguration.InstanceID + ".txt";
                
                // Trigger file creation and first write -- as header
                LogFileWriter.StartInstance(LogType.InstanceStart);
            }
        }

        #endregion

        #region Offline

        /// <summary>
        /// Signals the end of KLog operations for an instance, closing KLog instance log, renaming the KLOG file for sending operation.
        /// </summary>
        public static void Offline()
        {
            LogFileWriter.StopInstance(LogType.InstanceStop);

            // Rename from KLOG_W_$(guid) to KLOG_S_$(guid) -- this will maket the log available for transmission
            File.Move(LogConfiguration.FullFilePath, (LogConfiguration.RootFilePath + LogType.ReadyToSend + LogConfiguration.InstanceID + ".txt"));
        }

        #endregion
    }
}