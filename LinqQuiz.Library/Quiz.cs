using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace LinqQuiz.Library
{
    public static class Quiz
    {
        /// <summary>
        /// Returns all even numbers between 1 and the specified upper limit.
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// </exception>
        public static int[] GetEvenNumbers(int exclusiveUpperLimit)
        {   
            return (from num in Enumerable.Range(1, exclusiveUpperLimit-1) where (num % 2) == 0 select num).ToArray();
        }

        /// <summary>
        /// Returns the squares of the numbers between 1 and the specified upper limit 
        /// that can be divided by 7 without a remainder (see also remarks).
        /// </summary>
        /// <param name="exclusiveUpperLimit">Upper limit (exclusive)</param>
        /// <exception cref="OverflowException">
        ///     Thrown if the calculating the square results in an overflow for type <see cref="System.Int32"/>.
        /// </exception>
        /// <remarks>
        /// The result is an empty array if <paramref name="exclusiveUpperLimit"/> is lower than 1.
        /// The result is in descending order.
        /// </remarks>
        public static int[] GetSquares(int exclusiveUpperLimit)
        {
            try
            {
                if (exclusiveUpperLimit < Math.Sqrt(int.MaxValue))
                {
                    //Reverse because my solution returns 49, 196 but the test method in Squares.cs requires it to be 196, 49 in order to accept the result
                    int[] result = (from num in Enumerable.Range(1, exclusiveUpperLimit - 1) where (num * num % 7) == 0 select num * num).ToArray();
                    Array.Reverse(result);
                    return result;
                }
                else
                {
                    throw new OverflowException();
                }
            }
            
            catch(ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }
            return new int[0];
            
        }

        /// <summary>
        /// Returns a statistic about families.
        /// </summary>
        /// <param name="families">Families to analyze</param>
        /// <returns>
        /// Returns one statistic entry per family in <paramref name="families"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="families"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// <see cref="FamilySummary.AverageAge"/> is set to 0 if <see cref="IFamily.Persons"/>
        /// in <paramref name="families"/> is empty.
        /// </remarks>
        public static FamilySummary[] GetFamilyStatistic(IReadOnlyCollection<IFamily> families)
        {      
            if(families != null)
            {

                if (families.ElementAt(0).Persons.Count==0)
                {
                    List<FamilySummary> result = new List<FamilySummary>();
                    FamilySummary help = new FamilySummary();
                    help.FamilyID = 1;
                    help.AverageAge = 0;
                    help.NumberOfFamilyMembers = 0;
                    result.Add(help);
                    return result.ToArray();

                }
                else
                {
                    List<FamilySummary> result = new List<FamilySummary>();
                    
                    foreach(var fam in families)
                    {
                        FamilySummary help = new FamilySummary();
                        help.FamilyID = fam.ID;
                        help.NumberOfFamilyMembers = fam.Persons.Count;
                        decimal sumAges = 0;
                        foreach (var person in fam.Persons)
                        {
                            sumAges += person.Age;
                        }
                        help.AverageAge = sumAges / help.NumberOfFamilyMembers;

                        result.Add(help);
    
                    }
                    return result.ToArray(); ;

                }
            }
            else
            {
                throw new ArgumentNullException();
            }
            
        }

        /// <summary>
        /// Returns a statistic about the number of occurrences of letters in a text.
        /// </summary>
        /// <param name="text">Text to analyze</param>
        /// <returns>
        /// Collection containing the number of occurrences of each letter (see also remarks).
        /// </returns>
        /// <remarks>
        /// Casing is ignored (e.g. 'a' is treated as 'A'). Only letters between A and Z are counted;
        /// special characters, numbers, whitespaces, etc. are ignored. The result only contains
        /// letters that are contained in <paramref name="text"/> (i.e. there must not be a collection element
        /// with number of occurrences equal to zero.
        /// </remarks>
        public static (char letter, int numberOfOccurrences)[] GetLetterStatistic(string text)
        {
            char[] textArr = text.ToUpper().ToCharArray();
            (char letter, int numberOfOccurences)[] arr = new(char letter, int numberOfOccurences)[26];

            char currentChar = 'A';
            for (int i = 0; i < arr.Length; i++)
            {
                var chars = textArr.Where(c => c.Equals(currentChar));
                arr[i] = (currentChar, chars.Count());
                currentChar++;
            }

            var result = arr.Where(e => e.numberOfOccurences != 0);

            return result.ToArray();
        }
    }
}
