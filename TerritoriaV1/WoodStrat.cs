using Godot;
using System;

public partial class WoodStrat : StratDecorator
{
    private int woodDispo;  // recuperer ces gars
    private int woodNeeded;

    public override void ExecuteOwnStrat()
    {
        if(woodDispo < woodNeeded) // pas assez de bois pour tout ce qui est nécessaire
        {
            // si l'argent est dispo
        }
    }
}
