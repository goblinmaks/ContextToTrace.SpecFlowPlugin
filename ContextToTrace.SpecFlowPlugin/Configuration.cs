using System;
using System.Configuration;


namespace ContextToTrace.SpecFlowPlugin
{
    public static class Configuration
    {
        static readonly Lazy<ContextToTraceSection> ContextToTraceSection = new Lazy<ContextToTraceSection>(() => LoadConfigurationFromPluginAssembly() ?? LoadConfigurationFromAssemblyThatUsingThePlugin());

        private static ContextToTraceSection LoadConfigurationFromAssemblyThatUsingThePlugin()
        {
            return ConfigurationManager.GetSection("contextToTrace") as ContextToTraceSection;
        }

        private static ContextToTraceSection LoadConfigurationFromPluginAssembly()
        {
            var exeConfigPath = new Uri(typeof(Configuration).Assembly.CodeBase).LocalPath;
            return ConfigurationManager.OpenExeConfiguration(exeConfigPath).GetSection("contextToTrace") as ContextToTraceSection;
        }

        private static void ThrowAnErrorIfReportalPortalConfigurationSectionIsNull()
        {
            if (ContextToTraceSection.Value == null)
                throw new ConfigurationErrorsException("No ContextToTraceSection in config file!");
        }

        public static ContextToTraceSection ContextToTrace
        {
            get
            {
                ThrowAnErrorIfReportalPortalConfigurationSectionIsNull();
                return ContextToTraceSection.Value;
            }
        }
    }

    public class ContextToTraceSection : ConfigurationSection
    {
        [ConfigurationProperty("traceKey")]
        public bool TraceKey => bool.Parse(this["traceKey"].ToString());

        [ConfigurationProperty("filterKey")]
        public string FilterKey => this["filterKey"].ToString();
    }


}
