using Calculadora.Utils;
using System;

namespace Calculadora.Models
{
    public class Calculator : ICalculator
    {
        /// <summary>
        /// Símbolos
        /// </summary>
        public string AddSymbol { get; } = "+";
        public string SubstractSymbol { get; } = "-";
        public string MultiplySymbol { get; } = "*";
        public string DivideSymbol { get; } = "÷";
        public string PercentSymbol { get; } = "%";
        public decimal Number1 { get; set; }
        public decimal Number2 { get; set; }

        /// <summary>
        /// Operaciones básicas
        /// </summary>
        /// <param name="operation"></param>
        /// <returns></returns>
        public decimal Calculate(string operation)
        {
            decimal result = 0;

            switch (operation)
            {
                case "+":
                    result = Number1 + Number2;
                    break;
                case "-":
                    result = Number1 - Number2;
                    break;
                case "*":
                    result = Number1 * Number2;
                    break;
                case "÷":
                    result = Number2 != 0 ? Number1 / Number2 : 0;
                    break;
                case "%":
                    result = Number1 * (Number2 * 0.01m); // error result: 1.0
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
