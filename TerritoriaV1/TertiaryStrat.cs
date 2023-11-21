using Godot;
using System;

public partial class TertiaryStrat : BuildingStrategy
{

    public override void executeStrategie()
    {
        // transformer 1 usine à bière par tour en bibliotheque 

        for(int i = 0; i < availableWood%10; i++)    // 10 bois par maison ? a voir
        {
            // construire maison
        }

        // détruire une scierie, un champ et une usine a glacon par tour 
 
        // si bière vendu < nbMaison
        // -> detruit une maison 
    }
}
