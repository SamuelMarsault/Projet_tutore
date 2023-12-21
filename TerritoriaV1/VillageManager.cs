using TerritoriaV1;

public class VillageManager
{
    private Village village;
    private EvolutionOfVillage evolutionOfVillage;
    /// <summary>
    /// Affecte les observeurs au village et le démarre
    /// </summary>
    /// <param name="map">La carte du village</param>
    /// <param name="printer">Le composant d'affichage des ressources</param>
    /// <param name="trader">L'interface d'échange de ressources</param>
    /// <param name="evolutionOfVillage">Le classe qui gère les stratégies du village</param>
    public VillageManager(TileMap map, Printer printer,Trader trader, EvolutionOfVillage evolutionOfVillage)
    {
        village = new Village(map);
        this.evolutionOfVillage = evolutionOfVillage;
        evolutionOfVillage.SetVillage(village);
        
        village.AddObservers(map);
        village.AddObservers(printer);
        village.AddObservers(trader);

        village.StartVillage(); }

    /// <summary>
    /// Demande le passage de tour au village après lui avoir donné une nouvelle stratégie
    /// </summary>
    /// <param name="export">Les exports de ce tour</param>
    /// <param name="import">Les imports de ce tour</param>
    /// <param name="money">Les flux monétaires de ce tour</param>
    public void NextTurn(int[] export, int[] import, int[] money, int turn)
    {

        evolutionOfVillage.DetermineStrategy(turn);
        village.NextTurn(export, import, money);
        
    }

    /// <summary>
    /// Demande la phase 2 du passage de tour
    /// </summary>
    /// <param name="confirm">Les Placeable du village</param>
    public void applyNextTurn(bool confirm)
    {   
        village.ContinueNextTurn(confirm);
    }

    /// <summary>
    /// Getter sur village
    /// </summary>
    /// <returns>Le village</returns>
    public Village GetVillage()
    {
        return this.village;
    }

    /// <summary>
    /// Vérifie s'il y a encore des gens dans le village
    /// </summary>
    /// <returns>Si oui ou non on a au moins 1 maison</returns>
    public bool IsVillageOk()
    {
        bool ok = false;
        Placeable[,] placeables = village.GetPlaceables();
        for (int i = 0; i < placeables.GetLength(0) && !ok; i++)
        {
            for (int j = 0; j < placeables.GetLength(1) && !ok; j++)
            {
                if (placeables[i,j]!=null && placeables[i,j].getPlaceableType()==PlaceableType.HOUSE)
                {
                    ok = true;
                }
            }
        }

        return ok;
    }

    /// <summary>
    /// Getter sur le nombre de citoyens
    /// </summary>
    /// <returns>Le nombre de citoyens</returns>
    public int GetNumberCitizen()
    {
        int nbCitizen = 0;

        foreach(Placeable placeable in village.GetPlaceables())
        {
            if(placeable != null && placeable.getPlaceableType() == PlaceableType.HOUSE)
            {
                nbCitizen+=10;
            }
        }

        return nbCitizen;
    }

    /// <summary>
    /// Setter sur si oui ou non on affiche le manque de ressources 
    /// </summary>
    /// <param name="display">Les Placeable du village</param>
      public void SetMessage(bool display){
        village.SetMessageNeedResources(display);
    }

    /// <summary>
    /// Getter sur un Placeable du village
    /// </summary>
    /// <param name="x">La position en x</param>
    /// <param name="y">La position en y</param>
    /// <returns>Le placeable indiqué</returns>
    public Placeable GetPlaceable(int x, int y){
        return village.getPlaceable(x,y);
    }
}
