using System;
using System.Collections.Generic;
using TerritoriaV1;

/// <summary>
/// Représente une stratégie de construction primaire, construit des bâtiments de production de matières premières  
/// </summary>
public class PrimaryStrat : BuildingStrategy
{
    private TileType[,] tiles;
    public PrimaryStrat(Placeable[,] placeables, TileType[,] tiles)
    {
        SetTiles(tiles);
    }

    /// <summary>
    /// Créé et place les bâtiments dans le village
    /// </summary>
    /// <param name="import">Les imports de ce tour</param>
    /// <param name="export">Les exports de ce tour</param>
    /// <param name="factory">La factory de Placeable</param>
    /// <param name="targetTile">Un tableau de TileType cible, 1 pour chaque bâtiment</param>
    /// <param name="placeables">Les Placeable du village</param>
    /// <param name="resources">Les ressources actuelles</param>
    /// <param name="oldResources">Les ressources avant production</param>
    /// <returns>La nouvelle grille de Placeable</returns>
    override public Placeable[,] BuildNewPlaceable(int[] import,
        int[] export, PlaceableFactory factory, 
        TileType[] targetTile, Placeable[,] placeables, int[] resources, int[] oldResources)
    {
        int[] resourcesNeed = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] resourcesProduction = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int nbSawmill = 0;
        foreach (Placeable placeable in placeables)
        {
            if (placeable != null)
            {
                int[] needs = placeable.getResourceNeeds();
                int[] prod = placeable.getResourceProduction();
                for (int i = 0 ; i < resourcesNeed.Length; i++)
                {
                    resourcesNeed[i] += needs[i];
                    resourcesProduction[i] += prod[i];
                }

                if (placeable.getPlaceableType() == PlaceableType.SAWMILL) nbSawmill++;
            }
        }
        List<Placeable> newPlaceables = new List<Placeable>();
            if(1.25 * resourcesNeed[ResourceType.HOP.GetHashCode()] > resourcesProduction[ResourceType.HOP.GetHashCode()])
            {
                if(resources[(int)ResourceType.WOOD] > 5)
                {
                newPlaceables.Add(factory.CreateField());
                resources[(int)ResourceType.WOOD] -=5; 
                }
            }

            if(1.25 * resourcesNeed[ResourceType.ICE.GetHashCode()] > resourcesProduction[ResourceType.ICE.GetHashCode()])
            {
                if(resources[(int)ResourceType.WOOD] > 5)
                {   
                    newPlaceables.Add(factory.CreateIceUsine());
                    resources[(int)ResourceType.WOOD] -=5; 
                }
            }
        if(nbSawmill<2) 
        {
            newPlaceables.Add(factory.CreateSawmill());
        }   
        
        foreach (Placeable placeable in newPlaceables)
        {
           placeables =  PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]); 
        }
        return placeables;
    }
    /// <summary>
    /// getter des taux de changes pour l'import/export par ressource
    /// </summary>
    /// <returns>Les taux de change</returns>
    override public int[,] GetExchangesRates()
    {
        int[,] exchangesRates = new[,]
        {
            { 3, 2, 3, 8 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }

    /// <summary>
    /// Place un Placeable dans le tableau 2D des Placeable en fonction du TileType cibe
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// /// <param name="placeable">Le Placeable en question</param>
    /// <param name="targetTile">TileType cible du Placeable</param>
    /// <returns>La nouvelle grille de bâtiment</returns>
    override public Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile)
        {
            bool notPlaced = true;
            for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if (  HasTwoNeighbours(i, j, placeable.getPlaceableType(), placeables) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {  
                        placeables[i, j] = placeable;
                        notPlaced = false;
                    }
                }
            }
               if (notPlaced)
                {
                   for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if (  HasAdjacentPlaceableOfType(i, j, placeable.getPlaceableType(), placeables) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {  
                        placeables[i, j] = placeable;
                        notPlaced = false;
                    }
                }
            } 
                }
                if(notPlaced)
                {
                    PlaceRandomly(targetTile, placeable, placeables);
                }

        return placeables;
        }
}
