using System.Collections;
using System.Collections.Generic;

public class Faction
{
    public string Name;
    private List<Faction> Allies;

    public Faction()
    {
        this.Allies = new List<Faction>();
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
        // if the list of allies contains the faction
        if (Allies.Contains(faction))
            return true;
        else
            return false;
    }
}
