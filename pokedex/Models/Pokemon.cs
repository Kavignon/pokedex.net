using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

public sealed class Pokemon
{
    [Name("#")]
    [Required]
    public int Number { get; set; }

    [Name("Name")]
    [Required]
    public string Name { get; set; }

    [Name("Type 1")]
    [Required]
    public string Type1 { get; set; }

    [Name("Type 2")]
    public string Type2 { get; set; }

    [Name("Total")]
    [Required]
    public int Total { get; set; }

    [Name("HP")]
    [Required]
    public int HP { get; set; }

    [Name("Attack")]
    [Required]
    public int Attack { get; set; }

    [Name("Defense")]
    [Required]
    public int Defense { get; set; }

    [Name("Sp. Atk")]
    [Required]
    public int SpecialAttack { get; set; }

    [Name("Sp. Def")]
    [Required]
    public int SpecialDefense { get; set; }

    [Name("Speed")]
    [Required]
    public int Speed { get; set; }

    [Name("Generation")]
    [Required]
    public int Generation { get; set; }

    [Name("Legendary")]
    [Required]
    public bool Legendary { get; set; }
}