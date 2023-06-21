using CsvHelper.Configuration.Attributes;

public sealed class Pokemon
{
    [Name("#")]
    public int Number { get; set; }

    [Name("Name")]
    public string Name { get; set; }

    [Name("Type 1")]
    public string Type1 { get; set; }

    [Name("Type 2")]
    public string Type2 { get; set; }

    [Name("Total")]
    public int Total { get; set; }

    [Name("HP")]
    public int HP { get; set; }

    [Name("Attack")]
    public int Attack { get; set; }

    [Name("Defense")]
    public int Defense { get; set; }

    [Name("Sp. Atk")]
    public int SpecialAttack { get; set; }

    [Name("Sp. Def")]
    public int SpecialDefense { get; set; }

    [Name("Speed")]
    public int Speed { get; set; }

    [Name("Generation")]
    public int Generation { get; set; }

    [Name("Legendary")]
    public bool Legendary { get; set; }
}