using System;
using System.Collections.Generic;

namespace FlattenArray
{
    class Program
    {
        // (1) Write some code that will flatten an array of arbitrarily nested arrays of integers into 
        //     a flat array of integers. e.g. [[1,2,[3]],4] -> [1,2,3,4].
        //
        // The FlattenArray executable is in bin\Release directory. It runs on Microsoft Windows OS 
        // with .Net Framework version >= 4.5.2. It opens a console window, print the output flatten array 
        // and wait the operator to press the enter key to quit

        static void Main(string[] args)
        {
            // for simplicity, the input is called runtime
            object[] nestedArray = new object[] { 1, 2, new object[] { 3, new object[] { 4 } } };

            // this will contains the result
            List<int> flatList = new List<int>();

            if (NestedArrayToFlatList(nestedArray, flatList))
            {
                // flatList contains the list of integers. In c# we ca convert this list into an array
                // using ToArray method of List<int> object type
                // => int[] flatArray = flatList.ToArray();

                // print output to console
                Console.WriteLine(string.Format("Output array: [ {0} ]", string.Join(", ", flatList)));
            }

            Console.WriteLine("\nPress [ENTER] to terminate");
            Console.ReadKey();
        }

        /// <summary>
        /// NestedArrayToFlatArray convert nested integers arrays into flatten list of integers
        /// </summary>
        /// <param name="nestedArray">The nested array of integers</param>
        /// <param name="flatList">The output list of integers (can be converted to array)</param>
        /// <returns>true if the conversion is successful, false otherwise</returns>
        static private bool NestedArrayToFlatList(object[] nestedArray, List<int> flatList)
        {
            bool returnValue = true;

            int arrayLength = nestedArray.Length;
            // we use a for cicle to evaluate each array element
            for (int i = 0; i < arrayLength; i++)
            {
                // if the element is an array we evaluate using recursion
                if (nestedArray[i].GetType().IsArray)
                {
                    if (!NestedArrayToFlatList((object[])nestedArray[i], flatList))
                    {
                        return false;
                    }
                }
                else
                {
                    // otherwise we add it to the output list
                    object element = nestedArray[i];
                    int intElement;
                    if (element is int) // check if element is an integer
                    {
                        intElement = (int)element;
                    }
                    else
                    {
                        // else try to converto to integer
                        try
                        {
                            intElement = Convert.ToInt32(element);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error in element evaluation: {0} - probably is not an integer", element);
                            return false;
                        }
                    }

                    flatList.Add(intElement);
                }
            }

            return returnValue;
        }
    }
}
