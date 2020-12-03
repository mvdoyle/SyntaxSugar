using System;
using System.Collections.Generic;

namespace SyntaxSugar
{
    public static class LinqExtensionMethods
    {

      /*
       * Extension methods are a way to append functionality onto classes that you may
       * not even have the source code for.  (Otherwise you could add it to the class yourself)
       * 
       * In this instance here we're adding functionality to string arrays that might be useful to us
       * but is not something that belongs on a framework method. 
       * 
       * All extension methods must be static.  This one returns an IEnumerable<string>
       * and takes another IEnumerable<string> as an argument.  This is a pretty typical
       * pattern for linq functions.
       */
        public static IEnumerable<string> StripAllDashes(this IEnumerable<string> phrases)
        {
            foreach (var phrase in phrases)
                yield return StripDash(phrase);
            // the yield keyword has a special relationship with IEnumerable that allows us to evaluate
            // and return each instance, but only as needed from the calling class.
        }


        private static string StripDash(string phrase)
        {
            return phrase.Replace("-", "");
        }
    }
}
