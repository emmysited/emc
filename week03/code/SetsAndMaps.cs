using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Plan:
        // Step 1: Add all words into a HashSet for O(1) lookup.
        // Step 2: Loop through each word.
        // Step 3: Skip words where both letters are the same (e.g. "aa").
        // Step 4: Reverse the word and check if the reversed word exists in the set.
        // Step 5: To avoid duplicates (e.g. reporting "am & ma" and "ma & am"),
        //         only add the pair when the current word comes before its reverse alphabetically.
        // Step 6: Return the results as an array.

        var wordSet = new HashSet<string>(words);
        var results = new List<string>();

        foreach (var word in words)
        {
            // Skip words where both characters are the same (e.g. "aa")
            if (word[0] == word[1])
                continue;

            var reversed = $"{word[1]}{word[0]}";

            // Only add once: when current word is less than reversed to avoid duplicate pairs
            if (wordSet.Contains(reversed) && string.Compare(word, reversed) < 0)
            {
                results.Add($"{reversed} & {word}");
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            // Plan:
            // The degree is in column index 3 (4th column, 0-based).
            // Trim whitespace from the value.
            // If the degree already exists in the dictionary, increment its count.
            // Otherwise, add it with a count of 1.
            if (fields.Length > 3)
            {
                var degree = fields[3].Trim();
                if (degrees.ContainsKey(degree))
                    degrees[degree]++;
                else
                    degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Plan:
        // Step 1: Convert both words to lowercase and remove spaces.
        // Step 2: Build a dictionary of letter counts for word1.
        //         For each letter, increment its count.
        // Step 3: For each letter in word2, decrement its count in the dictionary.
        //         If a letter is not found or count goes below 0, return false.
        // Step 4: After processing word2, check that all counts are 0.
        //         If any count is non-zero, return false.
        // Step 5: Return true if all counts match.

        var w1 = word1.ToLower().Replace(" ", "");
        var w2 = word2.ToLower().Replace(" ", "");

        // Different lengths means they can't be anagrams
        if (w1.Length != w2.Length)
            return false;

        var letterCounts = new Dictionary<char, int>();

        // Count letters in word1
        foreach (var ch in w1)
        {
            if (letterCounts.ContainsKey(ch))
                letterCounts[ch]++;
            else
                letterCounts[ch] = 1;
        }

        // Subtract counts using word2
        foreach (var ch in w2)
        {
            if (!letterCounts.ContainsKey(ch))
                return false;

            letterCounts[ch]--;

            if (letterCounts[ch] < 0)
                return false;
        }

        // All counts should be zero
        foreach (var count in letterCounts.Values)
        {
            if (count != 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // Build the result array from the deserialized data
        // Each entry is formatted as: "place - Mag magnitude"
        return featureCollection?.Features
            .Select(f => $"{f.Properties.Place} - Mag {f.Properties.Mag}")
            .ToArray() ?? [];
    }
}
