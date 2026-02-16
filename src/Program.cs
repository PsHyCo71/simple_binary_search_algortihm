using System;
using System.Diagnostics;
using System.Globalization;

public class Program
{
    /// <summary>
    /// Entry point of the program.
    /// Prompts the user to enter a number within the range of a signed 64-bit integer,
    /// then calls the BinarySearch method to guess the number.
    /// </summary>
    public static void Main(string[] args)
    {
        // Variable to store the number the user wants the program to guess
        long number_to_guess;

        // Prompt the user to enter a number within the allowed range
        Console.WriteLine("Enter a number from 1 to 9,223,372,036,854,775,807:");

        // Repeat the input prompt until the user enters a valid 64-bit integer within the range
        while (!long.TryParse(Console.ReadLine(), out number_to_guess) || number_to_guess < 1 || number_to_guess > long.MaxValue)
        {
            // Inform the user if the input is invalid
            Console.WriteLine("Error: the input is invalid, please enter a valid number.");
        }

        // Call the BinarySearch method to find the number and print the result
        Console.WriteLine(BinarySearch(number_to_guess));
    }


    /// <summary>
    /// Performs a binary search to guess a number chosen by the user within the range of a 64-bit signed integer.
    /// Prints each guess attempt and measures the time taken to find the number.
    /// </summary>
    /// <remarks>
    /// We calculate the middle point as `min + (max - min) / 2` instead of `(min + max) / 2`.
    /// This avoids overflow: if `min` and `max` are very large, `min + max` could exceed
    /// the maximum value of a 64-bit integer (`long`) and cause incorrect results.
    /// Using `min + (max - min) / 2` ensures the difference fits safely in the type.
    /// </remarks>
    /// <param name="number_to_guess">The number the program should guess (must fit in a signed 64-bit integer).</param>
    /// <returns>A string indicating the found number, number of tries, and time taken in milliseconds.</returns>
    public static string BinarySearch(long number_to_guess)
    {
        // Define the minimum and maximum bounds for a signed 64-bit integer
        long min = 1;
        long max = long.MaxValue;

        // Counter for the number of attempts
        int tries = 0;

        // Create and start a stopwatch to measure the total search time
        Stopwatch sw = Stopwatch.StartNew();

        // Continue searching as long as the interval is valid
        while (min <= max)
        {

            // Increment the attempt counter
            tries++;

            // Calculate the middle point safely to avoid overflow, see <remarks> for details
            long mid = min + (max - min) / 2;

            // Print the current guess attempt
            Console.WriteLine($"Try {tries}: {mid.ToString("N0", CultureInfo.InvariantCulture)}");

            // Check if the current guess is correct
            if (mid == number_to_guess)
            {
                // Stop the stopwatch when the number is found
                sw.Stop();

                // Return the result including number, number of tries, and elapsed time
                return $"The number is: {mid.ToString("N0", CultureInfo.InvariantCulture)}. Found in {tries} tries, time taken: {sw.Elapsed.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)}ms";
            }
            else if (number_to_guess < mid)
            {
                // If the target number is smaller, move the upper bound down
                max = mid - 1;
            }
            else
            {
                // If the target number is larger, move the lower bound up
                min = mid + 1;
            }
        }

        // If the number is not found in the range (should not happen), return a fallback message
        return "Number not found.";
    }
}