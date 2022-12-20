using System.Linq;
using System.Text.RegularExpressions;

namespace Stockfolio.Shared.Abstractions;

public static class StringExtensions
{
    public static string RemoveWhitespace(this string value)
       => string.IsNullOrWhiteSpace(value) ? value : Regex.Replace(value, @"\s+", string.Empty);

    public static int SpecialCharactersCount(this string value)
        => value.Where(x => !char.IsLetterOrDigit(x)).Count();

    public static int NumbersCount(this string value)
        => value.Where(x => char.IsDigit(x)).Count();

    public static int UppercaseCount(this string value)
        => value.Where(x => char.IsUpper(x)).Count();

    public static int LowercaseCount(this string value)
        => value.Where(x => char.IsLower(x)).Count();

    public static bool AnySpecialCharacters(this string value)
        => value.Any(x => !char.IsLetterOrDigit(x));

    public static bool AnyNumbers(this string value)
        => value.Any(x => char.IsDigit(x));

    public static bool AnyUppercase(this string value)
        => value.Any(x => char.IsUpper(x));

    public static bool AnyLowercase(this string value)
        => value.Any(x => char.IsLower(x));

    public static string ToSnakeCase(this string value)
        => string.Concat((value ?? string.Empty).Select((x, i) => i > 0 && char.IsUpper(x) && !char.IsUpper(value[i - 1]) ? $"_{x}" : x.ToString())).ToLower();

    public static bool IsNullOrEmpty(this string value)
        => string.IsNullOrEmpty(value);

    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);
}