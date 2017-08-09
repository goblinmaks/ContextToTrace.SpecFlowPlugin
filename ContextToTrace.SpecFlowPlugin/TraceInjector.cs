using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Tracing;

namespace ContextToTrace.SpecFlowPlugin
{
    [Binding]
    class TraceInjector : Steps
    {
        private ITraceListener traceListener;

        public TraceInjector(ITraceListener traceListener)
        {
            this.traceListener = traceListener;
        }


        [AfterStep(Order = 50000)]
        public void AfterStep()
        {
            var filtered = Configuration.ContextToTrace.FilterKey.Trim().Length > 0 ? StepContext.Keys.Where(a => a == Configuration.ContextToTrace.FilterKey) : StepContext.Keys;

            foreach (var scValue in filtered.ToList())
            {
                var outputString = Configuration.ContextToTrace.TraceKey ? $"{scValue}:{StepContext[scValue]}" : $"{StepContext[scValue]}";              
                traceListener.WriteToolOutput(outputString);
            }
        }


        [BeforeStep(Order = 50000)]
        public void BeforeStep()
        {
            var filtered = Configuration.ContextToTrace.FilterKey.Trim().Length > 0 ? StepContext.Keys.Where(a => a == Configuration.ContextToTrace.FilterKey) : StepContext.Keys;
            foreach (var scValue in filtered.ToList())
            {
                var outputString = Configuration.ContextToTrace.TraceKey ? $"{scValue}:{StepContext[scValue]}" : $"{StepContext[scValue]}";
                traceListener.WriteToolOutput(outputString);
                StepContext.Remove(scValue);
            }
        }




    }
}
