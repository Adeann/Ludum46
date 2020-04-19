using System.Collections;
using System.Collections.Generic;

public class Faction
{
    public string Name;
    private List<Faction> Allies;

    public Faction(string name)
    {
        this.Allies = new List<Faction>();
        this.Name = name;
    }

    public void AddAlly(Faction faction)
    {
        // check that we're not re-adding an ally
        if (!isAlly(faction))
            Allies.Add(faction);
    }

    public void RemoveAlly(Faction faction)
    {
        if (isAlly(faction))
            Allies.Remove(faction);
    }
    public bool isAlly(Faction faction)
    {
        if (this == faction)
        {
            return true;
        }
        // if the list of allies contains the faction
        if (Allies.Contains(faction))
            return true;
        else
            return false;
    }
}

public static class Factions
{
    public static Faction Neutral = new Faction("Neutral");
    public static Faction Parasite = new Faction("Parasite");
    public static Faction Whale = new Faction("Whale");
    public static Faction Player = new Faction("Player");
    public static Faction Pirate = new Faction("Pirate");

    public static void DefaultInitialization()
    {
        Whale.AddAlly(Player);
        Player.AddAlly(Whale);
    }
}