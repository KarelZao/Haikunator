using System;
using System.Text.RegularExpressions;

Console.WriteLine("Vítejte v programu pro tvorbu haiku.");
Console.WriteLine();

const int pocetSlabik1 = 5, pocetSlabik2 = 7, pocetSlabik3 = 5;

// Řádek 1
Console.Write("Zadejte první řádek (5 slabik): ");
var radek1 = PrectiSlovoSDanymPoctemSlabik(pocetSlabik1);

// Řádek 2
Console.Write("Zadejte druhý řádek (7 slabik): ");
var radek2 = PrectiSlovoSDanymPoctemSlabik(pocetSlabik2);

// Řádek 3
Console.Write("Zadejte třetí řádek (5 slabik): ");
var radek3 = PrectiSlovoSDanymPoctemSlabik(pocetSlabik3);

Console.WriteLine("Tvoje haiku:");
Console.WriteLine($"{radek1}");
Console.WriteLine($"{radek2}");
Console.WriteLine($"{radek3}");


//Čte ze stdin dokud nemá vstup správný počet slabik
static string PrectiSlovoSDanymPoctemSlabik(int pozadovanyPocetSlabik)
{
    while (true)
    {
        var input = Console.ReadLine()?.ToLower().Trim();
        if (input is null) continue;
        
        var pocetSlabik = PocetSlabik(input);
        Informuj(pocetSlabik, pozadovanyPocetSlabik);
        
        if (pocetSlabik == pozadovanyPocetSlabik) return input;
    }
}

static int PocetSlabik(string slovo)
{
    string[] prefixes = new string[] { "o", "za", "na", "pře", "ne", "po", "pro", "do" };

    foreach (string prefix in prefixes)
    {
        if (Regex.IsMatch(slovo, $"^{prefix}(?![aeiouáéíóúùyý])", RegexOptions.IgnoreCase))
        {
            slovo = Regex.Replace(slovo, $"^{prefix}", "|", RegexOptions.IgnoreCase);
            break;
        }
        else if (Regex.IsMatch(slovo, $"^{prefix}", RegexOptions.IgnoreCase))
        {
            return -1; // slovo začínající zakázaným prefixem
        }
    }

    slovo = Regex.Replace(slovo, "[aeo]u", "a", RegexOptions.IgnoreCase);
    slovo = Regex.Replace(slovo, "[^aeiouáéíóúùůyýě][rl]$", "X|", RegexOptions.IgnoreCase);
    slovo = Regex.Replace(slovo, "[^aeiouáéíóúùůyýě][rl][^aeiouáéíóúùůyýě]", "X|X", RegexOptions.IgnoreCase);
    slovo = Regex.Replace(slovo, "[aeiouáéíóúùůyýě]", "|", RegexOptions.IgnoreCase);
    slovo = Regex.Replace(slovo, "[^|]", "", RegexOptions.IgnoreCase);

    return slovo.Length;
}

static void Informuj(int pocetSlabik, int pozadovanyPocetSlabik)
{
    if (pocetSlabik == pozadovanyPocetSlabik)
    {
        Console.WriteLine("Slova v řádku mají správný počet slabik");
        return;
    }

    if (pocetSlabik > pozadovanyPocetSlabik)
    {
        var rozdilSlabik = pocetSlabik - pozadovanyPocetSlabik;
        Console.WriteLine($"Slova v řádku mají o {rozdilSlabik} slabik{SlabikaKoncovka(rozdilSlabik)} více.");
        return;
    }

    if (pocetSlabik < pozadovanyPocetSlabik)
    {
        var rozdilSlabik = pozadovanyPocetSlabik - pocetSlabik;
        Console.WriteLine($"Slova v řádku mají o {rozdilSlabik} slabik{SlabikaKoncovka(rozdilSlabik)} méně.");
    }
}

static string SlabikaKoncovka(int cnt)
{
    return cnt switch
    {
        1 => "u",
        < 5 => "y",
        _ => ""
    };
}
