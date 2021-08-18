using System.Collections.Generic;

public static class DictionaryExtension
{
    /// <summary>
    /// Returns the key with the smallest value in the dictionary.
    /// </summary>
    public static T GetSmallest<T>(this Dictionary<T, float> dict)
    {
        T smallestKey = default(T);
        float smallestValue = float.MaxValue;

        foreach (KeyValuePair<T, float> kvp in dict)
        {
            T k = kvp.Key;
            float v = kvp.Value;

            if (v < smallestValue)
            {
                smallestKey = k;
                smallestValue = v;
            }
        }

        return smallestKey;
    }
}
