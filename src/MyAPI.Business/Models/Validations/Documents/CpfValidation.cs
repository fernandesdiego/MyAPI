using System;
using System.Linq;
using System.Text;

namespace MyAPI.Business.Models.Validations.Documents
{
    public class CpfValidation
    {
        public const int CpfSize = 11;

        public static bool Validate(string cpf)
        {
            var cpfNumbers = new string(cpf.Where(Char.IsDigit).ToArray());

            if (!ValidSize(cpfNumbers)) return false;
            return !HasRepeatedNumbers(cpfNumbers) && HasValidNumbers(cpfNumbers);
        }

        private static bool ValidSize(string valor)
        {
            return valor.Length == CpfSize;
        }

        private static bool HasRepeatedNumbers(string valor)
        {
            string[] invalidNumbers =
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };
            return invalidNumbers.Contains(valor);
        }

        private static bool HasValidNumbers(string valor)
        {
            var number = valor.Substring(0, CpfSize - 2);
            var numberVerifier = new NumberVerifier(number)
                .WithMultipliers(2, 11)
                .Swap("0", 10, 11);
            var firstDigit = numberVerifier.CalculateNumber();
            numberVerifier.AddNumber(firstDigit);
            var secondDigit = numberVerifier.CalculateNumber();

            return string.Concat(firstDigit, secondDigit) == valor.Substring(CpfSize - 2, 2);
        }
    }
}
