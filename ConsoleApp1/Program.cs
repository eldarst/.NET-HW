
using System;


public class ATM
{
    private static List<int> nominals;
    private static List<int[]> combinations = new List<int[]>();
    
    public static void Main(string[] args)
    {
        var n = -1;
        int.TryParse(Console.ReadLine() ?? string.Empty, out n);
        if (n < 1)
            return;

        var allNominals = Console.ReadLine()
            ?.Split()
            .Select(number =>
            {
                var n = 0;
                int.TryParse(number, out n);
                return n;
            })
            .Where(k => k <= n && k > 0)
            .Distinct()
            .OrderByDescending(k => k)
            .ToArray();
        
        nominals = new List<int>(allNominals);
        
        if (nominals == null)
            return;

        CountCombinations(0, n, new int[nominals.Count]);

        foreach (var combination in combinations)
        {
            for (var i = 0; i < combination.Length; ++i)
            {
                Console.Write($"{combination[i]} * {nominals[i]}  ");
            }
            Console.WriteLine($"= {n}");
        }
        
        Console.WriteLine(combinations.Count);
    }

    private static void CountCombinations(int currentNominalNumber, int currentRemainder, int[] currentCombination)
    {
        if (currentNominalNumber > nominals.Count - 1)
            return;
        
        var currentNominal = nominals[currentNominalNumber];
        var max = currentRemainder / currentNominal;
        
        if (currentNominalNumber == nominals.Count - 1 || currentRemainder <= 0)
        {
            if (currentRemainder % currentNominal == 0)
            {
                AddCombination(currentCombination, currentNominalNumber, max);
            }
            return;
        }
        
        for (var i = 0; i <= max; ++i)
        {
            currentCombination[currentNominalNumber] = i;
            CountCombinations(currentNominalNumber + 1, currentRemainder - i * currentNominal, currentCombination);
        }
    }

    private static void AddCombination(int[] currentCombination, int currentNominal, int val)
    { 
        var newCombination = new int[currentCombination.Length];
        Array.Copy(currentCombination, newCombination, currentCombination.Length);
        newCombination[currentNominal] = val;
        combinations.Add(newCombination);
    }
}