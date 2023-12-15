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
            }
        }
        BuildingStrategyFactory factoryStrat = new BuildingStrategyFactory();
        this.SetBuildingStrategy(factoryStrat.createPrimaryStrategy(this.tiles));
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
                    for (int c = 0; c < needs.Length; c++)
                    {
                        neededResources[c] += needs[c];
                    }
                }
            }
        }
        return neededResources;
    }

    //Initializes a 2D table containing the type of soil
    private void InitialiseTile(){
        // Parcourir chaque cellule du TileMap
        for (int x = 0; x < tiles.GetLength(0); x++)
        {
            for (int y = 0; y < tiles.GetLength(1); y++)
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
                else{
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
        Console.WriteLine("Statégie "+strategy.GetType());
        placeables = strategy.BuildNewPlaceable(resources, GetNeededResources(), factory, targetTiles, placeables, resourcesBeforeProduct);
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

    public void placerBatimentRand( PlaceableFactory factory, Placeable placeable)
    {
        bool canBePlaced = false;
        Random random = new Random();
        int x=0, y=0;
        PlaceableType placeableType = placeable.getPlaceableType();
        while (!canBePlaced)
        {
            canBePlaced = true;
            y = random.Next(0,tiles.GetLength(0));
            x = random.Next(0,tiles.GetLength(0));
            if (placeableType != PlaceableType.ICE_USINE && tiles[x, y] == TileType.WATER) canBePlaced = false;
            else if (placeableType == PlaceableType.ICE_USINE && tiles[x, y] != TileType.WATER) canBePlaced = false;

            if (placeableType == PlaceableType.SAWMILL && placeables[x, y].getPlaceableType() != PlaceableType.FOREST)
                canBePlaced = false;
        } 
        placeables[x, y] = placeable;
    }
    

   private bool MakeTransaction(int[] export, int[] import)
   {
       for (int i = 0; i < export.Length; i++)
       {
           if (resources[i] + import[i] - export[i] < 0)
           {
               NotifyImpossibleTransaction();
               return false;
           }
       } 
           
       for (int i = 0; i < export.Length; i++)
       {
           resources[i] += import[i];
           resources[i] -= export[i];
       }
       return true;
   }
    public void NextTurn(int[] export, int[] import)
    {
        if (MakeTransaction(export,import)){
            turn++;
            int[] resourcesBeforeProduct = (int[])resources.Clone();
            for (int i = 0; i < resources.Length; i++) 
                resourcesBeforeProduct[i] = resources[i];
            ProductResources();
            ApplyStrategy(resourcesBeforeProduct);
            
        }
    }

    private void NotifyImpossibleTransaction()
    {
        foreach (VillageObserver observer in observers) observer.ReactToImpossibleTransaction();
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
