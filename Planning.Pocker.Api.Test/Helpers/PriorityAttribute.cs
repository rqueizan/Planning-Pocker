using System;

namespace Planning.Pocker.Api.Test
{
    public enum Orderer { Code, MethodName, DisplayName, Random }

    public sealed class PriorityAttribute : Attribute
    {
        public int Priority { get; private set; }
        public Orderer Orderer { get; private set; }

        public PriorityAttribute(int priority, Orderer orderer = Orderer.Code)
        {
            Priority = priority;
            Orderer = orderer;
        }
    }
}
