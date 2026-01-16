namespace API.Utils;

public class NumberHelper
{
    public static double ConvertToDouble(string strNumber)
    {
        double doubleNumber = default;

        if (double.TryParse(
            strNumber,
            System.Globalization.NumberStyles.Any,
            System.Globalization.NumberFormatInfo.InvariantInfo,
            out doubleNumber)
        )
        {
            return doubleNumber;
        }

        return 0;
    }

    public static bool IsNumeric(string strNumber)
    {
        double doubleNumber = default;

        bool isNumber = double.TryParse(
            strNumber,
            System.Globalization.NumberStyles.Any,
            System.Globalization.NumberFormatInfo.InvariantInfo,
            out doubleNumber
        );
        
        return isNumber;
    }
};