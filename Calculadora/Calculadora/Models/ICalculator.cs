using System;
using System.Collections.Generic;
using System.Text;

namespace Calculadora.Models
{
    public interface ICalculator
    {
        string AddSymbol { get; }
        string SubstractSymbol { get; }
        string MultiplySymbol { get; }
        string DivideSymbol { get; }
        string PercentSymbol { get; }

        decimal Number1 { get; set; }
        decimal Number2 { get; set; }

        decimal Calculate(string operation);
    }
}
