namespace ConsoleChat.Server.Extensions;

public static class StringExtensions
{
    public static string ReverseString(this string s)
    {
        var reversedCharArray = s.Reverse().ToArray();
        return new string(reversedCharArray);
    }
}

