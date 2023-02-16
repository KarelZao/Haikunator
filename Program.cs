using System;
using System.Text.RegularExpressions;

Console.WriteLine("Vítejte v programu pro tvorbu haiku.");
Console.WriteLine();

while (true)
{
    string? radek1 = null, radek2 = null, radek3 = null;
    int pocetSlabik1 = 0, pocetSlabik2 = 0, pocetSlabik3 = 0;

    // Řádek 1
    while (pocetSlabik1 != 5)
    {
        Console.Write("Zadejte první řádek (5 slabik): ");
        radek1 = Console.ReadLine()?.Trim().ToLower();
        pocetSlabik1 = PocetSlabik(radek1);
        Informuj(pocetSlabik1);
    }

    // Řádek 2
    while (pocetSlabik2 != 7)
    {
        Console.Write("Zadejte druhý řádek (7 slabik): ");
        radek2 = Console.ReadLine()?.Trim().ToLower();
        pocetSlabik2 = PocetSlabik(radek2);
        Informuj(pocetSlabik2);
    }

    // Řádek 3
    while (pocetSlabik3 != 5)
    {
        Console.Write("Zadejte třetí řádek (5 slabik): ");
        radek3 = Console.ReadLine()?.Trim().ToLower();
        pocetSlabik3 = PocetSlabik(radek3);
        Informuj(pocetSlabik3);
    }

    // Pokud jsou všechny řádky správné, vypíšeme je a ukončíme smyčku
    if (pocetSlabik1 == 5 && pocetSlabik2 == 7 && pocetSlabik3 == 5)
    {
        Console.WriteLine("Tvoje haiku:");
        Console.WriteLine($"{radek1}");
        Console.WriteLine($"{radek2}");
        Console.WriteLine($"{radek3}");
        break;
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
static void Informuj(int pocetSlabik)
{
    switch (pocetSlabik)
    {
        case 1:
            Console.WriteLine("Slova v řádku mají pouze jednu slabiku.");
            break;
        case 2:
            Console.WriteLine("Slova v řádku mají pouze dvě slabiky.");
            break;
        case 3:
            Console.WriteLine("Slova v řádku mají pouze tři slabiky.");
            break;
        case 4:
            Console.WriteLine("Slova v řádku mají pouze čtyři slabiky.");
            break;
        case 5:
            Console.WriteLine("Slova v řádku mají správný počet slabik.");
            break;
        default:
            Console.WriteLine("Slova v řádku mají příliš mnoho ({0}) slabik.", pocetSlabik);
            break;
    }
}
