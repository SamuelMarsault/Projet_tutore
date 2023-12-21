using System;
using Godot;

using System.Collections.Generic;
using Godot.Collections;
using TerritoriaV1;

public class Village
{
    private BuildingStrategy strategy;
    private List<VillageObserver> observers = new ();
    private PlaceableFactory factory = new ();
    private Placeable[,] placeables;
    private TileType[,] tiles;
    private TileType[] targetTiles = new []{TileType.GRASS, TileType.FOREST,TileType.GRASS, TileType.GRASS, 
        TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.WATER, TileType.GRASS};
    private int[] resources;
    private TileMap map;
    private int turn;
    private int[,] exchangesRates;

    //Les exports
    private int[] old_export;

    //Les imports
    private int[] old_import;

    //Les flux monétaires
    private int[] old_money;

    private bool printNeedResources;    

    /// <summary>
    /// Créé les grilles de Placeable et Tiles, les actualise selon le terrain actuel
    /// Définis les tableau de ressources, d'import, d'export et de flux monétaires
    /// Récupère la carte et la stratégie 
    /// </summary>
    /// <param name="map">La carte du jeu</param>
    public Village(TileMap map)
    {
        this.printNeedResources = false;
        this.map = map;
        resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
        for(int i = 0;i<resources.Length;i++){
            resources[i] =20;
        }
        this.old_export = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        this.old_import = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        this.old_money = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        //Par défaut la stratégie est la croissance
        //Définition du terrain :
        tiles = new TileType[15,15];
        placeables = new Placeable[tiles.GetLength(0),tiles.GetLength(1)];
        for (int i = 0; i < placeables.GetLength(0); i++)
        {
            for (int j = 0; j < placeables.GetLength(1); j++)
            {
                placeables[i,j] = null;
                if(map.GetCellSourceId(0, new Vector2I(i,j))==0){
                    tiles[i,j] = TileType.GRASS;
                }
                else{
                     tiles[i,j] = TileType.WATER;
                }
            }
        }
        InitialiseTile();
        this.map = map;
        BuildingStrategyFactory factoryStrat = new BuildingStrategyFactory();
        this.SetBuildingStrategy(factoryStrat.createPrimaryStrategy(this.placeables, this.GetTiles()));
        this.turn = 1;

    }

    /// <summary>
    /// Getter sur le type de Strategy
    /// </summary>
    /// <returns>Si oui ou non le village est en phase de tertiarisation</returns>
    public bool IsStratTertiary()
    {
        return strategy.GetType()==typeof(TertiaryStrat);
    }
    
    /// <summary>
    /// Getter sur ressources
    /// </summary>
    /// <returns>Le tableau de ressources</returns>
    public int[] GetResources()
    {
        return (int[])resources.Clone();
    }

    /// <summary>
    /// Getter publique sur les besoins en ressources
    /// </summary>
    /// <returns>Le tableau de besoin en ressources</returns>
    public int[] GetNeededRessourcesPublic()
    {
        return (int[])this.GetNeededResources().Clone();
    }
    
    /// <summary>
    /// Getter privé sur les besoins en ressources
    /// </summary>
    /// <returns>Le tableau de besoin en ressources</returns>
    private int[] GetNeededResources()
    {
        int[] neededResources = new int[resources.Length];
        for (int i = 0; i < placeables.GetLength(0); i++)
        {
            for (int j = 0; j < placeables.GetLength(1); j++)
            {
                Placeable currentPlaceable = placeables[i,j];
                if (currentPlaceable != null)
                {
                    int[] needs = currentPlaceable.getResourceNeeds();
                    for (int c = 0; c < resources.Length;c++){
                        neededResources[c] += needs[c];
                    }
                }
            }
        }
        return neededResources;
    }

    /// <summary>
    /// Parcourt la map pour actualiser le sol dans le village 
    /// </summary>
    private void InitialiseTile(){
        // Récupérer les dimensions du TileMap
        int largeur = this.map.GetUsedRect().Size.X;
        int hauteur = this.map.GetUsedRect().Size.Y;

        this.tiles = new TileType[largeur,hauteur];
        // Parcourir chaque cellule du TileMap
        for (int x = 0; x < largeur; x++)
        {
            for (int y = 0; y < hauteur; y++)
            {
                // Récupérer l'ID du tile à la position (x, y)
                int tileID = this.map.GetCellSourceId(0,new Vector2I(x,y));
                // Vérifier si le tile existe à cette position
                if (tileID == 0)
                {
                    this.tiles[x,y] = TileType.GRASS;
                }
                else if(tileID ==1){
                    this.tiles[x,y] = TileType.WATER;
                }
                else if(tileID == 2)
                {
                    this.tiles[x,y] = TileType.FOREST;
                }
            }
        }
        NotifyTilesChange();
    }

    /// <summary>
    /// Demande à chaque Placeable de produire les ressources qu'il peut
    /// </summary>
    private void ProductResources()
    {
        //On récupère le besoin en ressource
        int[] neededResources = GetNeededResources();
              
        //Et pour chaque bâtiment :
        for (int i = 0; i < placeables.GetLength(0); i++)
        {
            for (int j = 0; j < placeables.GetLength(1); j++)
            {
                //On lui demande de produire en fonction des ressources disponibles
                if(placeables[i,j]!=null)
                {
                    placeables[i,j].ProductResources(resources, neededResources);
                }
            }
        }
    }
    
    /// <summary>
    /// Compte et range le nombre de Placeable de chaque type dans un tableau
    /// </summary>
    /// <returns>Tableau contenant le nombre de Placeable, rangé selon l'ordre dans l'enum PlaceableType</returns>
    public int[] getNBPlaceables()
    {
     int[] NBPlaceables = new int[Enum.GetNames(typeof(PlaceableType)).Length];
     for(int i = 0; i < NBPlaceables.Length; i++)
     {
        NBPlaceables[i] = 0;
     }

     for(int i = 0; i < placeables.GetLength(0); i++)
        {
            for(int j = 0; j < placeables.GetLength(1); j++)
            {
                if (placeables[i, j] != null)
                    NBPlaceables[placeables[i, j].getPlaceableType().GetHashCode()]++;   
            }
        }
     
     return NBPlaceables;
    }

    /// <summary>
    /// Setter de strategy
    /// </summary>
    /// <param name="strategy">La nouvelle stratégie du village</param>
    public void SetBuildingStrategy(BuildingStrategy strategy)
    {
        this.strategy = strategy;
    }

    /// <summary>
    /// Applique la stratégie, a besoin de ressources avant production
    /// </summary>
    /// <param name="resourcesBeforeProduct">Les Placeable du village</param>
     private void ApplyStrategy(int[] resourcesBeforeProduct)
    {
        placeables = strategy.BuildNewPlaceable(old_import, old_export, factory, targetTiles, placeables, resources, resourcesBeforeProduct);
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
        NotifyResourcesChange();
    }
    
    /// <summary>
    /// Ajoute un observeur au village
    /// </summary>
    /// <param name="observer">Observeur du village</param>
    public void AddObservers(VillageObserver observer)
    {
        observers.Add(observer);
    }

    /// <summary>
    /// Prévient les observeurs que les ressources ont changés
    /// </summary>
    private void NotifyResourcesChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToResourcesChange((int[])resources.Clone());
        }
    }
    
    /// <summary>
    /// Prévient les observeurs que les placeables ont changés
    /// </summary>
    private void NotifyPlaceableChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToPlaceableChange(placeables);
        }
    }
    
    /// <summary>
    /// Prévient les observeurs que les tiles ont changés
    /// </summary>
    private void NotifyTilesChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToTilesChange(tiles);
        }
    }

    /// <summary>
    /// Getter sur le sol du village
    /// </summary>
    /// <returns>Le tableau 2D de TileType</returns>
    public TileType[,] GetTiles()
    {
        return tiles;
    }

    /// <summary>
    /// Place les premiers bâtiments du village et prévient les observeurs
    /// </summary>
    public void StartVillage()
    {
        placeables[13,12] = factory.CreateHouse();
        placeables[12,12] = factory.CreateBar();
        placeables[14, 8] = factory.CreateBeerUsine();
        placeables[7, 14] = factory.CreateTrainStation();
        NotifyResourcesChange();
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
    }

    /// <summary>
    /// Effectue la transaction
    /// </summary>
    /// <param name="verif">Si oui ou non la fonction a été appelé pour vérifier si la transaction était possible</param>
    /// <returns>Si oui ou non on peut faire la transaction sans manquer de ressources</returns>
    private bool MakeTransaction(bool verif)
    {
        int[] oldRessources = GetResources();

        this.resources = applyResourcesTransaction();
        
        ProductResources();

        if (this.printNeedResources && verif == true){

            int[] insufficientResources = new int[resources.Length];
            
             bool inssufisant = false;

             int[] needRessorcesNow = GetNeededResources();

            for (int i = 0; i < resources.Length; i++)
            {
                if ((resources[i]- needRessorcesNow[i]) < 0){
                    insufficientResources[i] = ((resources[i] - needRessorcesNow[i])*-1);
                    inssufisant = true;
                }

                else{
                    insufficientResources[i] = 0;
                }
            }
            resources = oldRessources;
            if (inssufisant == true){
                NotifyImpossibleTransaction(insufficientResources);
                return false;
            }
            return true;
        }
        else{
            if (verif == true){
                resources = oldRessources;
            }
            return true;
        }
    }
    /// <summary>
    /// Récupère les flux et lance la phase 2 du passage de tour
    /// </summary>
    /// <param name="export">Les exports du tour</param>
    /// <param name="import">Les imports du tour</param>
    /// <param name="money">Les flux monétaires du tour</param>
    public void NextTurn(int[] export, int[] import, int[] money)
    { 
        this.old_export = export;
        this.old_import = import;
        this.old_money = money;
        ContinueNextTurn(MakeTransaction(true));
    }

    /// <summary>
    /// Termine le passage de tour si continue est vrai
    /// </summary>
    /// <param name="continueTurn">Si oui ou non on peut passer au tour suivant</param>
    public void ContinueNextTurn(bool continueTurn)
    {
        if (continueTurn)
        {
            int[] oldResources = applyResourcesTransaction();
            MakeTransaction(false);
            ProductResources();
            ApplyStrategy(oldResources);
            turn++;
        }
    }
    
    /// <summary>
    /// Applique une transaction de ressources
    /// </summary>
    /// <returns>Le nouveau tableau de ressources</returns>
    public int[] applyResourcesTransaction(){
        int[] finalResources = new int[resources.Length];

        for (int i = 0; i < finalResources.Length-1; i++)
        {
            
            finalResources[i] = resources[i];
        }

        finalResources[resources.Length - 1] = resources[resources.Length-1];
        
        //Pour chaque ressources sauf l'argent
        for (int i = 0; i < finalResources.Length-1; i++)
        {
            //On ne fait les transactions jusqu'à ce qu'on ne puisse plus
            //Si on exporte + que ce qu'on possède
            if (finalResources[i] < old_export[i])
            {
                //Alors l'argent qu'on a c'est tout ce qu'on peut vendre
                finalResources[ResourceType.MONEY.GetHashCode()] += (finalResources[i] * exchangesRates[1, i]);
                //Et on a plus d'argent
                finalResources[i] = 0;
            }
            else if(old_export[i]!=0)
            {
                //Aucun problème
                finalResources[ResourceType.MONEY.GetHashCode()] += (old_export[i] * exchangesRates[1, i]);
                //Et on a plus d'argent
                finalResources[i] -= old_export[i];
            }
            if(finalResources[ResourceType.MONEY.GetHashCode()] < old_import[i] * exchangesRates[0, i])//Si on importe + que ce qu'on a
            {
                int usedQuantity = finalResources[ResourceType.MONEY.GetHashCode()] / exchangesRates[0, i];
                finalResources[i] = finalResources[i] + usedQuantity;
                //Oui c'est bizarre mais c'est division entière
                finalResources[ResourceType.MONEY.GetHashCode()] -= usedQuantity * exchangesRates[0, i];
            }
            else if(old_import[i]!=0)
            {
                finalResources[i] = finalResources[i] + old_import[i];
                finalResources[ResourceType.MONEY.GetHashCode()] -= old_import[i] * exchangesRates[0, i];
            }
        }

        return finalResources;
    }

    /// <summary>
    /// Setter sur si oui ou non on affiche les manques en ressources 
    /// </summary>
    /// <param name="display">Les Placeable du village</param>
    public void SetMessageNeedResources(bool display){
        this.printNeedResources = display;
    }

    /// <summary>
    /// Préviens les observeurs d'une transaction impossible
    /// </summary>
    /// <param name="missingResources">Les ressources manquantes</param>
    private void NotifyImpossibleTransaction(int[] missingResources)
    {
        foreach (VillageObserver observer in observers) observer.ReactToImpossibleTransaction(missingResources);
    }

    /// <summary>
    /// Préviens les observeurs du changement des taux de change
    /// </summary>
    private void NotifyExchangesRatesChange()
    {
        foreach (VillageObserver observer in observers) observer.ReactToExchangesRatesChange(exchangesRates);
    }

    /// <summary>
    /// Getter sur la grille de Placeable
    /// </summary>
    /// <returns>La grille de Placeable</returns>
    public Placeable[,] GetPlaceables()
    {
        return placeables;
    }

    /// <summary>
    /// Getter sur un Placeable
    /// </summary>
    /// <param name="x">La position en x</param>
    /// <param name="y">La position en y</param>
    /// <returns>Le Placeable à la position indique</returns>
    public Placeable getPlaceable(int x, int y)
    {
        try{
            return placeables[x, y];
        }
        catch (IndexOutOfRangeException) {
            return null;
        }
    }
}
