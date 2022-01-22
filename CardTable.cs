using Godot;
using System;
using System.Linq;

public class CardTable : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.

    private static Random random = new Random();


    TestSurface Surface { get; set; }
    Card Card { get; set; }
    Card Card2 { get; set; }
    Card Card3 { get; set; }



    public override void _Ready()
    {
        Surface = GetNode("TestSurface") as TestSurface;
        Card = GetNode("Card") as Card;
        Card2 = GetNode("Card2") as Card;
        Card3 = GetNode("Card3") as Card;

        //Pick 3 random letters from the alphabet.
        string randomChars;
        do{
            randomChars = RandomString(3);
            GD.Print("Randomizing");
        }while(OnlyOnceCheck(randomChars));
        Card.Value = randomChars[0].ToString();
        Card2.Value = randomChars[1].ToString();
        Card3.Value = randomChars[2].ToString();

        Card._Ready();
        Card2._Ready();
        Card3._Ready();

        //Pick a random character from the 3 to be the target.
        var rng = new RandomNumberGenerator();
        rng.Randomize();
        var randomNumber = rng.RandiRange(0, 2);
        GD.Print(randomNumber);
        Surface.MatchingValue = randomChars[randomNumber].ToString();
        Surface._Ready();
    }




    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var test = Enumerable.Repeat(chars, length);
        var returnValue = new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());

            return returnValue;
    }

    public static bool OnlyOnceCheck(string input)
    {
        return input.GroupBy(x => x).Any(g => g.Count() > 1);
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
