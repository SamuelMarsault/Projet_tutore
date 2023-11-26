using Godot;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TerritoriaV1;

public partial class PrimaryStrat : BuildingStrategy
{
    public PrimaryStrat()
    {
        
    }

    public List<Placeable> BuildNewPlaceable(int[] totalResources, int[] neededResources, Placeable[][] placeables, PlaceableFactory factory)
    {
            if(neededResources[(int)ResourceType.HOP] > totalResources[(int)ResourceType.HOP])
            {
               neededResources[(int)ResourceType.WOOD] +=10; // on aura besoin de quoi faire un champ de plus 
               // stocker qqpart que on veut un champ de plus
            }

            if(neededResources[(int)ResourceType.ICE] > totalResources[(int)ResourceType.ICE])
            {
               neededResources[(int)ResourceType.WOOD] +=10; 
               
            }

            if(neededResources[(int)ResourceType.WOOD] > totalResources[(int)ResourceType.WOOD])
            {

                // factory.CreateSawmill();    // la strat peut pas savoir ou est placé le batiment, et ne peux donc pas udpate la map
            }

            // ce modele ne permet que 1 de chaque batiment par tour.
            // on parcours ensuite les batiment necessaire, et si on as assez on les construits

            // g aucune idée de ce que je fait, je verrais demain 
    }
}
