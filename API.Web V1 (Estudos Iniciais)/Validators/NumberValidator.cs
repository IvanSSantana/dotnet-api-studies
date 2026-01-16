using API.Utils;
using API.Models;

namespace API.Validators;

public class NumberValidator
{
    public static string IsNumeric(string[] numbers)
    {
        string exceptions = "";

        for (int i = 0; i < numbers.Length; i++)
        {
            var currentNumber = numbers[i];
            if (!NumberHelper.IsNumeric(currentNumber)) exceptions += $"An error ocurred in validation with number: {currentNumber}.";
        }

        return exceptions;
    }
}
