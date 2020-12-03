using System;
namespace SyntaxSugar
{

    /// <summary>
    /// Extension methods MUST be defined in a static class
    /// </summary>
    public static class ExtensionMethods
    {

        /// <summary>
        /// Static classes must have static methods.  The "this" attribute is the other attribute
        /// worth explaining.  It's what allows you to call this function as if it were an extension
        /// of string.  See other file.
        /// </summary>
        /// <param name="phrase"></param>
        /// <returns></returns>
        public static string StripDashes(this string phrase)
        {
            return phrase.Replace("-", "");
        }
    }
}
