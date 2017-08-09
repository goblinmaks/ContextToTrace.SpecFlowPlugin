using ContextToTrace.SpecFlowPlugin;
using TechTalk.SpecFlow.Plugins;
using TechTalk.SpecFlow.Tracing;

[assembly: RuntimePlugin(typeof(Plugin))]
namespace ContextToTrace.SpecFlowPlugin
{
    public class Plugin : IRuntimePlugin
    {
        public void Initialize(RuntimePluginEvents runtimePluginEvents, RuntimePluginParameters runtimePluginParameters)
        {
            {
                runtimePluginEvents.CustomizeTestThreadDependencies += (sender, e) =>
                {
                    e.ObjectContainer.RegisterTypeAs<ContextToTrace, ITestTracer>();
                };
            }
        }
    }
}
