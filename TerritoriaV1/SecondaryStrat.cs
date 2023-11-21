using Godot;
using System;

public partial class SecondaryStrat : BuildingStrategy
{
    int availableWood;

    public override void executeStrategie()
    {
        // construire 1 usine à bière par tour  

        for(int i = 0; i < availableWood%10; i++)    // 10 bois par maison ? a voir
        {
            // construire maison
        }

        // détruire une scierie, un champ et une usine a glacon par tour 
 
        // si bière vendu < nbMaison
        // -> detruit une maison 
    }
}
