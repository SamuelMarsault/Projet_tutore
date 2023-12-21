using System;
using Godot;

using System.Collections.Generic;
using System.Linq;
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
    
    //Renvoi les ressources actuelles du village
    public int[] GetResources()
    {
        return (int[])resources.Clone();
    }

    public int[] GetNeededRessourcesPublic()
    {
        return (int[])this.GetNeededResources().Clone();
    }
    
    //Récupère les besoins en ressources de toutes les structures du village
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

    //Initializes a 2D table containing the type of soil
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

    //"Joue le tour" pour les structures et permet de récupérer les ressources 
    private bool ProductResources()
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
        
        /*Console.WriteLine("##### Après production : #####");
        for (int i = 0; i < neededResources.Length; i++)
        {
            Console.WriteLine(Enum.GetNames(typeof(ResourceType)).GetValue(i)+" : ");
            Console.WriteLine("Disponible : "+resources[i]);
            Console.WriteLine("Besoin : "+neededResources[i]);
        }*/
        return true;
    }

    //Calcule le % de remplissage des besoins du village
    public double FulfilementOfNeeds(Dictionary usableResources,
        Godot.Collections.Dictionary<ResourceType, int> neededResources)
    {
        return 0;
    }
    
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

    public void SetBuildingStrategy(BuildingStrategy strategy)
    {
        this.strategy = strategy;
    }

     private void ApplyStrategy(int[] resourcesBeforeProduct)
    {
        if(placeables == null)
        {
            //GD.Print("placeables == null");
        }
        //Console.WriteLine("Statégie "+strategy.GetType());
        placeables = strategy.BuildNewPlaceable(old_import, old_export, factory, targetTiles, placeables, resources, resourcesBeforeProduct);
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
        /*for(int i = 0; i < placeables.GetLength(0); i++)
        {
            for(int j = 0; j < placeables.GetLength(0); j++)
            {
                if(placeables[i,j] != null)
                {
                    GD.Print(placeables[i,j].getPlaceableType());
                }
            }
        }*/
        NotifyResourcesChange();
    }
    
    public void AddObservers(VillageObserver observer)
    {
        observers.Add(observer);
    }

    private void NotifyResourcesChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToResourcesChange((int[])resources.Clone());
        }
    }
    
    private void NotifyPlaceableChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToPlaceableChange(placeables);
        }
    }
    
    private void NotifyTilesChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.ReactToTilesChange(tiles);
        }
    }

    public TileType[,] GetTiles()
    {
        return tiles;
    }

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
                if (i != 4){
                    if ((resources[i] + needRessorcesNow[i]) - (old_export[i]) < 0){
                        insufficientResources[i] = (((resources[i] + needRessorcesNow[i]) - (old_export[i]))*-1);
                        inssufisant = true;
                    }
                    else{
                        insufficientResources[i] = 0;
                    }
                }
                else{
                    for (int j = 0; j < old_money.Length; j++)
                    {
                        if ((resources[i] + needRessorcesNow[i]) + (old_money[j]) < 0)
                        {
                            GD.Print((resources[i] + needRessorcesNow[i]) + (old_money[j]));
                            insufficientResources[i] += (((resources[i] + needRessorcesNow[i]) + (old_money[j])) * -1);
                            inssufisant = true;
                        }
                    }

                    // Déplacez cette condition à l'extérieur de la boucle
                    if (!inssufisant)
                    {
                        insufficientResources[i] = 0;
                    }
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
    public void NextTurn(int[] export, int[] import, int[] money)
    { 
        this.old_export = export;
        this.old_import = import;
        this.old_money = money;
        continueNextTurn(MakeTransaction(true));
    }

    public void continueNextTurn(bool contnue)
    {
        if (contnue)
        {
            int[] oldResources = applyResourcesTransaction();
            MakeTransaction(false);
            ProductResources();
            ApplyStrategy(oldResources);
            turn++;
        }
    }

    public int[] applyResourcesTransaction(){
        int[] finalResources = new int[resources.Length];

        for (int i = 0; i < finalResources.Length-1; i++)
        {
            
            finalResources[i] = resources[i];
            //GD.Print(i+" Après "+finalResources[i]+" "+old_import[i]+" "+old_export[i]);
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

    public void setMessageNeedResources(bool display){
        this.printNeedResources = display;
    }

    private void NotifyImpossibleTransaction(int[] missingRessources)
    {
        foreach (VillageObserver observer in observers) observer.ReactToImpossibleTransaction(missingRessources);
    }

    private void NotifyExchangesRatesChange()
    {
        foreach (VillageObserver observer in observers) observer.ReactToExchangesRatesChange(exchangesRates);
    }

    public Placeable[,] GetPlaceables()
    {
        return placeables;
    }

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
