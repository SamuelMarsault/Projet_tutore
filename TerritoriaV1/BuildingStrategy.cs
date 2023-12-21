using System;
using TerritoriaV1;

/// <summary>
/// Représente une stratégie de construction d'un village, permet la construction mais aussi la destruction
/// </summary>
public abstract class BuildingStrategy {
    private TileType[,] tiles;

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
    public abstract Placeable[,] BuildNewPlaceable(int[] import,
        int[] export, PlaceableFactory factory, 
        TileType[] targetTile,Placeable[,] placeables, int[] resources, int[] oldResources);


    /// <summary>
    /// getter des taux de changes pour l'import/export par ressource
    /// </summary>
    public abstract int[,] GetExchangesRates();

    /// <summary>
    /// Place un Placeable dans le tableau 2D des Placeable en fonction du TileType cibe
    /// </summary>
    /// <param name="placeables">Les Placeable du village</param>
    /// /// <param name="placeable">Le Placeable en question</param>
    /// <param name="targetTile">TileType cible du Placeable</param>
    public abstract Placeable[,] PlacePlaceable(Placeable[,] placeables,Placeable placeable, TileType targetTile);

    /// <summary>
    /// Vérifie si la case indiqué peut accueillir le Placeable
    /// </summary>
    /// <param name="x">Position en x du Placeable</param>
    /// <param name="y">Position en y du Placeable</param>
    /// <param name="targetTileType">TileType cible du Placeable</param>
    /// <param name="placeables">Les Placeable du village</param>
    protected bool CanPlaceAtLocation(int x, int y, TileType targetTileType, Placeable[,] placeables)
    {
    if (x < placeables.GetLength(0) && y < placeables.GetLength(1))
    {
        if ((tiles[x, y] != targetTileType) || (placeables[x, y] != null))
        {
            
            return false;
        }
        else
        {
            return true; 
        }
    }
    return false;

    }


    /// <summary>
    /// Vérifie si la case indiqué possède des bâtiments du même type que le type donné
    /// </summary>
    /// <param name="x">Position en x du Placeable</param>
    /// <param name="y">Position en y du Placeable</param>
    /// <param name="type">Type du Placeable en question</param>
    /// <param name="placeables">Les Placeable du village</param>
    protected bool HasAdjacentPlaceableOfType(int x, int y, PlaceableType type, Placeable[,] placeables)
    {

        if(((x<placeables.GetLength(0)-1)   &&    (placeables[x+1,y] != null)    && (placeables[x+1,y].getPlaceableType() == type)) ||
        ((y<placeables.GetLength(0)-1)  &&  (placeables[x,y+1] != null )    &&  ( placeables[x,y+1].getPlaceableType() == type)) ||
        ((x>0)  &&  (placeables[x-1,y] != null )    &&  ( placeables[x-1,y].getPlaceableType() == type)) ||
        ((y>0)  &&  (placeables[x,y-1] != null )    &&  ( placeables[x,y-1].getPlaceableType() == type)))
        {
            return true;
        }

    return false;
   }

    /// <summary>
    /// Regarde si la case a au moins 2 voisins du PlaceableType type
    /// </summary>
    /// <param name="x">Position en x du Placeable</param>
    /// <param name="y">Position en y du Placeable</param>
    /// <param name="type">Type du Placeable en question</param>
    /// <param name="placeables">Les Placeable du village</param>
    public bool HasTwoNeighbours(int x, int y, PlaceableType type, Placeable[,] placeables)
    {
        int rowCount = placeables.GetLength(0);
        int colCount = placeables.GetLength(1);

        int neighbourCount = 0;

        // Vérification du voisin de droite
        if (x < rowCount - 1 && placeables[x + 1, y] != null && placeables[x + 1, y].getPlaceableType() == type)
            neighbourCount++;

        // Vérification du voisin du dessous
        if (y < colCount - 1 && placeables[x, y + 1] != null && placeables[x, y + 1].getPlaceableType() == type)
            neighbourCount++;

        // Vérification du voisin de gauche
        if (x > 0 && placeables[x - 1, y] != null && placeables[x - 1, y].getPlaceableType() == type)
            neighbourCount++;

        // Vérification du voisin du dessus
        if (y > 0 && placeables[x, y - 1] != null && placeables[x, y - 1].getPlaceableType() == type)
            neighbourCount++;

        // Vérification du voisin en diagonal
        if (x < rowCount - 1 && y < colCount - 1 && placeables[x + 1, y + 1] != null && placeables[x + 1, y + 1].getPlaceableType() == type)
            neighbourCount++;

        if (x > 0 && y < colCount - 1 && placeables[x - 1, y + 1] != null && placeables[x - 1, y + 1].getPlaceableType() == type)
            neighbourCount++;

        if (x < rowCount - 1 && y > 0 && placeables[x + 1, y - 1] != null && placeables[x + 1, y - 1].getPlaceableType() == type)
            neighbourCount++;

        if (x > 0 && y > 0 && placeables[x - 1, y - 1] != null && placeables[x - 1, y - 1].getPlaceableType() == type)
            neighbourCount++;

        return neighbourCount >= 2;
    }


    /// <summary>
    /// Place un Placeable à un endroit aléatoire sur la carte sur une TileType cible
    /// </summary>
    /// <param name="targetTileType">Le TileType cible du placeable</param>
    /// <param name="placeable">Position en y du Placeable</param>
    /// <param name="placeables">Les Placeable du village</param>
    protected void PlaceRandomly(TileType targetTileType, Placeable placeable, Placeable[,] placeables) {
        var rand = new Random();
        int x = rand.Next(15);
        int y = rand.Next(15);
        if(CanPlaceAtLocation(x, y, targetTileType, placeables))
        {
            placeables[x, y] = placeable;
        }
        else
        {
            PlaceRandomly(targetTileType,placeable,placeables);
        }
    }
    /// <summary>
    /// setter de tiles
    /// </summary>
    /// <param name="tiles">Tableau 2D de TileType, représente le sol</param>
    public void SetTiles(TileType[,] tiles) {this.tiles = tiles;}

    public void Destroy(PlaceableType type, Placeable[,] placeables)
    {
        Boolean Destroyed = false; 
        for(int i = 0; i < placeables.GetLength(0) && !Destroyed; i++)
        {
            for(int j = 0; j < placeables.GetLength(1) && !Destroyed; j++)
            {
                if(placeables[i,j] != null && placeables[i,j].getPlaceableType() == type)
                {
                    placeables[i,j] = null;
                    Destroyed = true;
                }
            }
        }
    }
}
