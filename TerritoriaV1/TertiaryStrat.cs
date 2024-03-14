using System;
using System.Collections.Generic;
using TerritoriaV1;

/// <summary>
/// Représente une stratégie de construction tertiaire, construit des bâtiments de service et des maisons
/// </summary>
public class TertiaryStrat : BuildingStrategy
{
    public TertiaryStrat(Placeable[,] placeables,TileType[,] tiles)
    {
        SetTiles(tiles);
    }

    /// <summary>
    /// Créé et place les bâtiments dans le village, actualise aussi la production des bars selon le nombre de maisons
    /// Et détruit des maisons s'il n'y a pas assez de bière
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
        int[] resourcesNeed = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        int[] resourcesProduction = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        int beerProduction = oldResources[ResourceType.BEER.GetHashCode()] + import[ResourceType.BEER.GetHashCode()] - export[ResourceType.BEER.GetHashCode()];
        foreach (Placeable placeable in placeables)
        {
            if (placeable != null)
            {
                int[] needs = placeable.getResourceNeeds();
                int[] prod = placeable.getResourceProduction();
                for (int i = 0 ; i < resourcesNeed.Length; i++)
                {
                    resourcesNeed[i] += needs[i]+export[i];
                    resourcesProduction[i] += prod[i]+import[i];
                }
            }
        }
        
        List<Placeable> newPlaceables = new List<Placeable>();
        int nbHouse = 0, nbBar = 0;
        foreach (Placeable placeable in placeables)
        {
            if (placeable!=null)
            {
                if (placeable.getPlaceableType() == PlaceableType.HOUSE)
                    nbHouse++;
                else if (placeable.getPlaceableType() == PlaceableType.BAR)
                    nbBar++;
            }
        }
        
        //Si manque de bière
        while (beerProduction/5<nbHouse)
        {
            nbHouse--;
            Destroy(PlaceableType.HOUSE, placeables);
        }
        
        if(resourcesProduction[ResourceType.BEER.GetHashCode()]*1.25 > resourcesNeed[ResourceType.BEER.GetHashCode()]) // le joueur a interet a exporter ses bieres si il veut pas qu'on construisent des bars partout
        {
            if(resources[(int)ResourceType.WOOD] > 10 && nbBar*10<=nbHouse)
            {
                newPlaceables.Add(factory.CreateBar());
                resources[(int)ResourceType.WOOD] -=10;
                nbBar++;
            }

            for (int i = 0; i < 3 && nbBar*10>nbHouse && resources[(int)ResourceType.WOOD] > 10; i++)
            {
                newPlaceables.Add(factory.CreateHouse());
                resources[(int)ResourceType.WOOD] -= 10;
                nbHouse++;
            }
        }

        while (nbBar*10<nbHouse)
        {
            Destroy(PlaceableType.HOUSE,placeables);
            nbHouse--;
        }
        foreach (Placeable placeable in newPlaceables)
        {
            PlacePlaceable(placeables,placeable, targetTile[placeable.getPlaceableType().GetHashCode()]);
        }

        Destroy(PlaceableType.BEER_USINE,placeables);

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
            { 3, 1, 1, 4 }, //import
            { 1, 1, 1, 6 } //export
        };
        return exchangesRates;
    }

    /// <summary>
    /// Place un Placeable dans le tableau 2D des Placeable en fonction du TileType cibe
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// <param name="placeable">Le Placeable en question</param>
    /// <param name="targetTile">TileType cible pour le bâtiment</param>
    /// <returns>La nouvelle grille de bâtiment</returns>
    override public Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile)
     {
            bool notPlaced = true;
            for (int i = 0; i < placeables.GetLength(0) && notPlaced; i++)
            {
                for (int j = 0; j < placeables.GetLength(1) && notPlaced; j++)
                {
                    if (HasAdjacentPlaceableOfType(i, j, placeable.getPlaceableType(), placeables) && CanPlaceAtLocation(i, j, targetTile, placeables))
                    {  
                        placeables[i, j] = placeable;
                        notPlaced = false; 
                    }
                }
            }
               if (notPlaced)
                {
                    PlaceRandomly(targetTile, placeable, placeables);
                }

        return placeables;
        }
}
