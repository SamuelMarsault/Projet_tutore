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
                if(totalResources[(int)ResourceType.WOOD] > 10)// on a assez pour construire, on peut plutot tester ca dans Create
                {
                factory.CreateField();
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

            if(neededResources[(int)ResourceType.WOOD] > totalResources[(int)ResourceType.WOOD])    // on devrait la créer a chaque tour meme si on as assez de bois 
            {
                factory.CreateSawmill();
            }
        
        return null;
    }
}
