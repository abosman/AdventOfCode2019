using System.Collections.Generic;
using System.Text;

namespace Day5
{
    public class Instruction
    {
        public Instruction()
        {
            Parameters = new List<Parameter>();
        }
        public int OperationCode { get; set; }

        public int  OperationRaw { get; set; }

        public List<Parameter> Parameters { get; set; }

        public bool Jumped { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(OperationRaw);
            sb.Append(" ");
            foreach (var parameter in Parameters)
            {
                sb.Append(parameter.AddressOrValue);
                sb.Append(" ");
            }

            return sb.ToString();
        }

    }
}
