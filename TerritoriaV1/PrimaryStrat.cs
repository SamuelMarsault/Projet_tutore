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
                if(totalResources[(int)ResourceType.WOOD] > 10)//si on a assez pour construire, on peut plutot tester ca dans Create
                {
                factory.CreateField();  // coordonées
                totalResources[(int)ResourceType.WOOD] -=10; // je met a jour manuellement les valeurs, on peux peut etre l'automatiser 
                }
            }

            if(neededResources[(int)ResourceType.ICE] > totalResources[(int)ResourceType.ICE])
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateIceUsine();
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

               if(neededResources[(int)ResourceType.BEER] < totalResources[(int)ResourceType.BEER]) // je pensais avoir oublier bar et beerusine dans cette strat, donc je les ai rajouté, mais en fait il sont dans la strat suivante, il semblerais donc qu'il y a avait une raison pour ne pas en creer a ce moment 
            {
                if(totalResources[(int)ResourceType.WOOD] > 10)
                {
                    factory.CreateBar();
                    totalResources[(int)ResourceType.WOOD] -=10;
                }
            }

            if(neededResources[(int)ResourceType.ICE] < totalResources[(int)ResourceType.ICE] && neededResources[(int)ResourceType.HOP] < totalResources[(int)ResourceType.HOP])
            {
                factory.CreateBeerUsine();
                 totalResources[(int)ResourceType.WOOD] -=10;
            }

            if(neededResources[(int)ResourceType.WOOD] > totalResources[(int)ResourceType.WOOD])    // on devrait la créer a chaque tour meme si on as assez de bois 
            {
                factory.CreateSawmill();
            }
        
        return null;
    }
}
