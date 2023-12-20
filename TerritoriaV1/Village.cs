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

    private int[] old_export;

    private int[] old_import;

    private int[] old_money;

    public Village(TileMap map)
    {
        this.map = map;
        resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
        for(int i = 0;i<resources.Length;i++){
            resources[i] =100;
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
        
        /*Console.WriteLine("##### Avant production : #####");
        for (int i = 0; i < neededResources.Length; i++)
        {
            Console.WriteLine(Enum.GetNames(typeof(ResourceType)).GetValue(i)+" : ");
            Console.WriteLine("Disponible : "+resources[i]);
            Console.WriteLine("Besoin : "+neededResources[i]);
        }*/
        
        
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
            GD.Print("placeables == null");
        }
        //Console.WriteLine("Statégie "+strategy.GetType());
        placeables = strategy.BuildNewPlaceable(resources, GetNeededResources(), factory, targetTiles, placeables, resourcesBeforeProduct);
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
        for(int i = 0; i < placeables.GetLength(0); i++)
        {
            for(int j = 0; j < placeables.GetLength(0); j++)
            {
                if(placeables[i,j] != null)
                {
                    GD.Print(placeables[i,j].getPlaceableType());
                }
            }
        }
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
        /*placeables[6,2] = factory.CreateHouse();
        placeables[6,0] = factory.CreateHouse();*/
        placeables[13,12] = factory.CreateHouse();
        placeables[12,12] = factory.CreateBar();
        //placeables[11,9] = factory.CreateSawmill();
        //placeables[20,16] = factory.CreateTrainStation();
        /*placeables[15,11] = factory.CreateField();
        placeables[16,11] = factory.CreateField();
        placeables[15,10] = factory.CreateField();
        placeables[16,10] = factory.CreateField();*/
        placeables[12, 10] = factory.CreateBeerUsine();
        placeables[14, 14] = factory.CreateTrainStation();

        /*for(int i = 0; i < placeables.GetLength(0); i++)
        {
            for(int j = 0; j < placeables.GetLength(1); j++)
            {
                placeables[i,j] = factory.CreateHouse();
            }
        }*/
        NotifyResourcesChange();
        NotifyPlaceableChange();
        exchangesRates = strategy.GetExchangesRates();
        NotifyExchangesRatesChange();
    }

    private bool MakeTransaction()
    {
        int[] export = new int[Enum.GetNames(typeof(ResourceType)).Length-1];
        int[] import = new int[Enum.GetNames(typeof(ResourceType)).Length-1]; 
                
        for (int i = 0;i<export.Length;i++){
            export[i] = old_export[i];
            import[i] = old_import[i];
        }
        
        int[] oldRessources = GetResources();

        for (int j = 0; j<old_money.Length-1;j++){
            resources[4] += old_money[j];
        }

        for (int i = 0; i < import.Length; i++)
        {
            if ((old_money[i] + oldRessources[4]) > 0){
                resources[i] += import[i];
                resources[i] -= export[i];
            }
        }
        
        ProductResources();
    
        
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

    public void NextTurn(int[] export, int[] import, int[] money)
    { 
        this.old_export = export;
        this.old_import = import;
        this.old_money = money;
        continueNextTurn(MakeTransaction());
    }

    public void continueNextTurn(bool contnue)
    {
        if (contnue)
        {
            applyResourcesTransaction();
            int[] oldResources = (int[])resources.Clone();
            for (int i = 0; i < resources.Length; i++)
                oldResources[i] = resources[i];
            ProductResources();
            ApplyStrategy(oldResources);
            turn++;
        }
    }

    public void applyResourcesTransaction(){
        int[] oldResources = GetResources();
        for (int i = 0; i < old_export.Length; i++)
        {
            GD.Print(old_export.Length);
            if (((resources[i]+ old_import[i]) - old_export[i])>0 && (old_money[i] + oldResources[4])>0){
                resources[i] += old_import[i];
                resources[i] -= old_export[i];
                resources[4] += old_money[i];
            }
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
