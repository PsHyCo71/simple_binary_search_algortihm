# Simple Binary Search Algorithm

![.NET](https://img.shields.io/badge/.NET-10.0-blue) [![License](https://img.shields.io/badge/license-GPLv3-blue)](https://www.gnu.org/licenses/gpl-3.0.html?utm_source=chatgpt.com)

A simple binary search algorithm to find a given integer within the range of a signed 64-bit integer.

## How to use

- Enter an integer within the range of a signed 64-bit integer (1 to 9,223,372,036,854,775,807).  
- The program will use a binary search algorithm to find the entered number.  
- Once the number is found, it will display the number of tries and the time taken to find it.

## The Algorithm

```csharp
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
```

## Example Output

```pws1
Enter a number from 1 to 9,223,372,036,854,775,807:
8589934592
Try 1: 4,611,686,018,427,387,904
Try 2: 2,305,843,009,213,693,952
...
Try 30: 8,589,934,592
The number is: 8,589,934,592. Found in 30 tries, time taken: 5.872ms
```

## License

This project is licensed under the [GNU GPL v3.0](https://www.gnu.org/licenses/gpl-3.0.html)
