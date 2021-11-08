using System.IO;

namespace ElectricityBillMSIC.Application
{
    public class UserMenuDecisionHandlerParameter
    {
        public UserMenuDecisionType DecisionType { get; init; }
        public TextReader Reader { get; init; }
        public TextWriter Writer { get; init; }
    }
}
