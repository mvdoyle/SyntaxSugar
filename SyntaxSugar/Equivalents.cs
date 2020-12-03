using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;

namespace SyntaxSugar
{
    /// <summary>
    /// The compiler doesn't care what file we put things in,
    /// it only cares about namespace.
    /// However to ease the way for future programmers it is
    /// nice to organize classes into files of the same name.
    /// </summary>
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        //ignore region below for now
        #region Needed4validity
        public override bool Equals(object obj)
        {
            var compareTo = ((Person)obj);
            return compareTo.FirstName == FirstName
                && compareTo.LastName == LastName;
        }
        #endregion

    }

    public class Equivalents
    {

        /// <summary>
        /// The following are all the same ways to initialize a person.
        /// </summary>
        public void ConstructorExamples()
        {
            var p1 = new Person();
            p1.FirstName = "Mariah";
            p1.LastName = "Carrie";
            //-------

            var p2 = new Person { FirstName = "Mariah", LastName = "Carrie" };
            //p1 == p2

            //--------

            var p3 = new Person() { FirstName = "Mariah", LastName = "Carrie" };
            //p1 == p2 == p3

            //--------

            //Defining like below would be considered bad practice these days compared to using var
            //However if you need to initialize a variable and set it later it's still relevant
            Person p4 = new Person { FirstName = "Mariah", LastName = "Carrie" };
            //p1 == p2 == p3 == p4

            //---------

            Person p5 = null;
            p5 = new Person { FirstName = "Mariah", LastName = "Carrie" };

            //---------

            //NEW in C#8
            //omitting the type on the right side is legal only because type is specified on the left
            Person p6 = new () { FirstName = "Mariah", LastName = "Carrie" };
            //p1 == p2 == p3 == p4 == p5 == p6

        }
        public void ArrayInitializations()
        {
            ///This is the most explicit and most verbose. We'll pair this down from here.
            string[] people = new string[] { "Bob", "Dereck", "Susan" };

            //Since the initialization is defined the compiler can determine the type of var
            var people1 = new string[] { "Bob", "Dereck", "Susan" };

            //Since the initial values of the array are defined the type can be omitted and inferred from them.
            // double quotes indicate those values are strings so the compiler knows this is a string array
            var people2 = new[] { "Bob", "Dereck", "Susan" };

        }
        /// <summary>
        /// Determining whether an object is null or not is something you'll do over and over all day long
        /// so programmers have invented a plethera of ways to do so.
        /// </summary>
        public void NullComparisons(Person person)
        {
            string firstName;
            if (person == null)
            {
                person = new Person();
            }
            if (person != null && person.FirstName == "Mariah") {
                //When person is populated and FirstName is Mariah
                firstName = person.FirstName;
            }

            //-------
            // The if from above to ensure that person is not null is taken care of by the ? here.
            firstName = person?.FirstName;

            //-------
            // An extension from above.  The null-coalescing ?? allows you to fall through to another value
            // if and only if the preceding value is null
            firstName = person?.FirstName ?? "Unknown";

            // -----
            //The null-coalescing assignment allows you to avoid the additional if syntax and abbreviate to a single line
            //This is equivalent to if (person == null) person = new Person();
            person ??= new Person();
        }


        public void Scoping()
        {
            //you can establish scope anywhere with { }
            {
                //this isn't meaningful and you wouldn't do it but it is legal syntax
            }

            if (true)
            {
                //This is more like it
                var thing = "";
                //thing is available within the scope here.
            }
            //thing is no longer available here because the scope ended on the prior line.

            //-----

            //You can omit the { } which does an implicite single line scope.
            if (true)
                Console.WriteLine("Hello");


            //----

            //Similar to above you can put the statement on the same line as the conditional
            if (true) Console.WriteLine("Hello");


        }

        public string StripDashes(string phrase)
        {
            return phrase.Replace("-", "");
        }

        /// <summary>
        /// A lambda is an advanced concept that you will need to learn quickily to efficiently
        /// manipulate lists.  but in the end it's just an function declared inline
        /// </summary>
        public void Lambdas()
        {
            ///This is equivalent to the StripDashes function above.
            ///Func uses generics to establish the types of the parameters and the return type
            ///is always the last type specified.
            ///
            Func<string, string> stripDashes1 = (string phrase) =>
            {
                return phrase.Replace("-", "");
            };

            ///In this we omit the scope and the "return" is removed because it's understood
            Func<string, string> stripDashes2 = (string phrase) => phrase.Replace("-", "");

            ///Type can be omitted in the function because it's known that it's a string due
            ///to the Func definition
            Func<string, string> stripDashes3 = (phrase) => phrase.Replace("-", "");

            ///Paren can be omitted when there is only one parameter to the Func
            Func<string, string> stripDashes4 = phrase => phrase.Replace("-", "");


            string phoneNumber = "123-123-1234";
            string phoneMinusDash;

            //All of the following have the same outcome
            phoneMinusDash = StripDashes(phoneNumber);
            phoneMinusDash = stripDashes1(phoneNumber);
            phoneMinusDash = stripDashes2(phoneNumber);
            phoneMinusDash = stripDashes3(phoneNumber);
            phoneMinusDash = stripDashes4(phoneNumber);

        }

        public void StaticFilesAndExtensionMethods()
        {
            string phoneNumber = "123-123-1234";
            string phoneMinusDash;
            //While normally we would have to "new" up an instances.  Static classes allow us to avoid that
            //See ExtensionMethods.cs
            phoneMinusDash = ExtensionMethods.StripDashes(phoneNumber);

            //This particular static method utilizes the "this" keyword which allows it to be called as an extension
            //to any string and implicitely use that string as the first argument.
            //Both the below and above statement are equivalent.
            phoneMinusDash = phoneNumber.StripDashes();

        }


        /// <summary>
        /// LINQ stands for Language INtegrated Query. It takes advantage of a combination of language
        /// features to accomplish the syntax.  (Extension methods, Generics, Lambdas
        /// Must include namespace System.Linq by putting "using System.Linq;" at the top of your file 
        /// 
        /// </summary>
        public void LinqFunctions()
        {
            /* Ok if you're still with me we're going to put several concepts together at once, so
             * if the other concepts don't make sense go back and review them now and come back after.
             */

            //There is a large collection of linq functions available to us but before we jump into those
            //let's write our own.

            //GOTO Linq ExtensionMethods then return here after you' finish reviewing

            //ok.... here we go.
            var phoneNumbers = new[] { "111-111-1111", "123-123-1234", "555-123-1234" };
            IEnumerable<string> phoneMinusDashes;

            //First well call it the uncool way.
            phoneMinusDashes = LinqExtensionMethods.StripAllDashes(phoneNumbers);

            //Now for the cool way.
            phoneMinusDashes = phoneNumbers.StripAllDashes();
            //phoneNumbers is implicitly passed in as the first argument thanks to the "this" keyword

            //Weird right...

            //Let's see what's possible with the Linq functions Microsoft wrote for us in System.Linq
            var people = new[] { new Person { FirstName = "Bob" },
                                 new Person { FirstName = "Gary" },
                                 new Person { FirstName = "Bart"}
            };

            //initialize the variable for the result
            IEnumerable<Person> peopleThatStartWithB;

            //Define a function inline that allows us to determine whether or not a
            //person's name starts with B
            Func<Person, bool> startsWithB = p => p.FirstName.StartsWith("B");

            //Pass that function into where.  Where will evaluate whether or not that function evaluates to true
            //and Where will return all items in people where the function evaluates to true.
            peopleThatStartWithB = people.Where(startsWithB);


            //This can be shorthanded even further by omitting the startsWithB Func all together and
            // defining it inline with
            // a lambda
            // p is arbitary and can be any variable name you determine but typically a single character
            // is used for brievity.  In this case p is an individual person
            // Each item in the list will be evaluated with the lambda function
            // For each item that evaluates to true those items will be returned.
            // The items that do not evaluate to true will not be recorded.
            peopleThatStartWithB = people.Where(p => p.FirstName.StartsWith("B"));


            // Ok Let's get fancier.
            // The pattern of returning the same value you take as a parameter allows us to "chain" methods.
            var firstNamesThatStartWithB = people.Where(p => p.FirstName.StartsWith("B")).Select(p => p.FirstName);

            // ok real fancy...   equivalent to above.
            var firstNamesThatStartWithB2 = from p in people where p.FirstName.StartsWith("B") select p.FirstName;

            // fancier still.. equivalent to above.  In this we incorporate the let keyword which is
            // equivalent to a variable that is set during the evaluation of the query
            var firstNamesThatStartWithB3 = from p in people
                                            let firstName = p.FirstName
                                            where firstName.StartsWith("B")
                                            select firstName;

            //The SQL like code above has fallen out of favor by programmers and typically the
            //lambda syntax is used, but nonetheless you will encounter it

            // whew that's alot to take in.  Just like anything the more you see it the easier it gets
            // but in the meantime you can use this as a reference to try and decipher other code and
            // hopefully the explainations here are helpful.
        }
    }

    
}
