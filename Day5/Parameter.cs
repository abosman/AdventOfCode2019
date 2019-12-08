namespace Day5
{
    public class Parameter
    {
        public ParameterMode Mode { get; set; }

        public int AddressOrValue { get; set; }

        public enum ParameterMode
        {
            Position=0,
            Immediate=1
        }

        public override string ToString()
        {
            return $"{AddressOrValue} {(Mode == ParameterMode.Position ? "(p)" : "(i)")},";
        }
    }
}
