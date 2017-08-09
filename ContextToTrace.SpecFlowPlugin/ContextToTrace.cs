using TechTalk.SpecFlow.BindingSkeletons;
using TechTalk.SpecFlow.Configuration;
using TechTalk.SpecFlow.Tracing;

namespace ContextToTrace.SpecFlowPlugin
{
    public class ContextToTrace : TestTracer
    {
        private ITraceListener traceListener;

        public ContextToTrace(ITraceListener traceListener, IStepFormatter stepFormatter, IStepDefinitionSkeletonProvider stepDefinitionSkeletonProvider, RuntimeConfiguration runtimeConfiguration) : base(traceListener, stepFormatter, stepDefinitionSkeletonProvider, runtimeConfiguration)
        {
            this.traceListener = traceListener;
            new TraceInjector(traceListener);
        }
    }
}
