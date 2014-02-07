﻿using Orc.GraphExplorer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Orc.GraphExplorer
{
    public class GraphExplorerSection : ConfigurationSection
    {
        public static GraphExplorerSection Current
        {
            get
            {
                Configuration exeConfiguration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                
                if (exeConfiguration.FilePath.EndsWith("mstest.exe.config", StringComparison.OrdinalIgnoreCase))
                {
                    var map = new ExeConfigurationFileMap();
                    map.ExeConfigFilename = Directory.GetFiles(
                        Environment.CurrentDirectory,
                        "*.dll.config",
                        SearchOption.TopDirectoryOnly).FirstOrDefault(); 
                    var exeConfiguration = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                }
                
                return exeConfiguration.GetSection("graphExplorer") as GraphExplorerSection;
            }
        }

        [ConfigurationProperty("csvGraphDataServiceConfig", IsRequired = false)]
        public CsvGraphDataServiceConfig CsvGraphDataServiceConfig
        {
            get { return (CsvGraphDataServiceConfig)base["csvGraphDataServiceConfig"]; }
            set { base["csvGraphDataServiceConfig"] = value; }
        }

        [ConfigurationProperty("setting",IsRequired=false)]
        public GraphExplorerSetting Setting
        {
            get { return (GraphExplorerSetting)base["setting"]; }
            set { base["setting"] = value; }
        }
    
        [ConfigurationProperty("defaultGraphDataService", DefaultValue = GraphDataServiceEnum.Csv)]
        public GraphDataServiceEnum DefaultGraphDataService
        {
            get { return (GraphDataServiceEnum)base["defaultGraphDataService"]; }
            set { base["defaultGraphDataService"] = value; }
        }

        [ConfigurationProperty("graphDataServiceFactory", IsRequired=false)]
        public string GraphDataServiceFactory
        {
            get { return (string)base["graphDataServiceFactory"]; }
            set { base["graphDataServiceFactory"] = value; }
        }
    }

    public class GraphExplorerSetting : ConfigurationElement
    {
        [ConfigurationProperty("enableNavigation",IsRequired = false, DefaultValue = false)]
        public bool EnableNavigation
        {
            get { return (bool)base["enableNavigation"]; }
            set { base["enableNavigation"] = value; }
        }

        [ConfigurationProperty("navigateToNewTab", IsRequired = false, DefaultValue = true)]
        public bool NavigateToNewTab
        {
            get { return (bool)base["navigateToNewTab"]; }
            set { base["navigateToNewTab"] = value; }
        }
    }
}
