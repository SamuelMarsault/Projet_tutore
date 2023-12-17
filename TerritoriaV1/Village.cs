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

    public Village(TileMap map)
    {
        this.map = map;
        resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
        for(int i = 0;i<resources.Length;i++){
            resources[i] = 500;
        }

        //Par défaut la stratégie est la croissance
        //Définition du terrain :
        tiles = new TileType[20,20];
        InitialiseTile();
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
        this.map = map;
        BuildingStrategyFactory factoryStrat = new BuildingStrategyFactory();
        this.SetBuildingStrategy(factoryStrat.createPrimaryStrategy(this.placeables, this.GetTiles()));
        GD.Print(this.map == null);
        this.turn = 1;

    }
    
    //Renvoi les ressources actuelles du village
    public int[] GetResources()
    {
        return (int[])resources.Clone();
    }

    public int[] GetNeededRessourcesPublic()
    {
        return (int[])this.GetNeededResources(true).Clone();
    }
    
    //Récupère les besoins en ressources de toutes les structures du village
    private int[] GetNeededResources(bool lequel)
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
                    if (lequel == true){
                        for (int c = 0; c < needs.Length; c++)
                        {
                            neededResources[c] += needs[c];
                        }
                    }
                    else{
                        for (int c = 0; c < needs.Length; c++)
                        {
                            neededResources[c] += needs[c]*currentPlaceable.getProductionCapacity();
                        }
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
        int[] neededResources = GetNeededResources(true);
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
        Console.WriteLine("Après production : ");
        for (int i = 0; i < neededResources.Length; i++)
        {
            Console.WriteLine(Enum.GetNames(typeof(ResourceType)).GetValue(i)+" : ");
            Console.WriteLine("Disponible : "+resources[i]);
            Console.WriteLine("Besoin : "+neededResources[i]);
        }
        NotifyResourcesChange();
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
        //Console.WriteLine("Statégie "+strategy.GetType());
        placeables = strategy.BuildNewPlaceable(resources, GetNeededResources(true), factory, targetTiles, placeables, resourcesBeforeProduct);
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
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
        placeables[6,2] = factory.CreateHouse();
        placeables[6,0] = factory.CreateHouse();
        placeables[8,2] = factory.CreateHouse();
        placeables[6,4] = factory.CreateBar();
        placeables[11,9] = factory.CreateSawmill();
        placeables[14,16] = factory.CreateTrainStation();
        placeables[15,11] = factory.CreateField();
        placeables[16,11] = factory.CreateField();
        placeables[15,10] = factory.CreateField();
        placeables[16,10] = factory.CreateField();
        placeables[16, 12] = factory.CreateBeerUsine();
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
    }

  private bool MakeTransaction(int[] export, int[] import)
    {
        int[] oldRessources = GetResources();

        ProductResources();

        int index = ResourceType.MONEY.GetHashCode();

        int[] insufficientResources = new int[resources.Length];

        bool inssufisant = false;

        int[] needRessorcesNow = GetNeededResources(false);

        for (int i = 0; i < export.Length; i++)
        {
            resources[i] += import[i];
            resources[i] -= export[i];
            
            if (i == 0){
               //GD.Print(resources[i]);
                GD.Print("village-test maketransaction");
            }
            
            if ((resources[i]-needRessorcesNow[i]) < 0)
            {
                if (i != 3){
                    // Ajouter le couple ResourceType et la valeur correspondante à la liste
                    insufficientResources[i] = ((resources[i]-needRessorcesNow[i])*-1);
                    inssufisant = true;
                }
                // Remettre la valeur à 0 si elle est devenue négative
                //resources[i] = 0;
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

    public void NextTurn(int[] export, int[] import)
    { 
        continueNextTurn(MakeTransaction(export,import));
    }

    public void continueNextTurn(bool contnue)
    {
        if (contnue)
        {
            int[] oldResources = (int[])resources.Clone();
            for (int i = 0; i < resources.Length; i++)
                oldResources[i] = resources[i];
            ProductResources();
            NotifyResourcesChange();
            ApplyStrategy(oldResources);
            turn++;
        }
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
}
