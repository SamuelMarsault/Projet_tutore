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
    private Placeable[][] placeables;
    private TileType[][] tiles;
    private int [] resources;
    private TileMap map;

    public Village(TileMap map)
    {
        resources = new int[Enum.GetNames(typeof(ResourceType)).Length];
        //Par défaut la stratégie est la croissance
        strategy = new BuildingGrowthStrategy(tiles);
        //Définition du terrain :
        tiles = new TileType[20][];
        for (int i = 0; i < tiles.Length; i++)
        {
            tiles[i] = new TileType[tiles.Length];
        }
        placeables = new Placeable[tiles.Length][];
        for (int i = 0; i < placeables.Length; i++)
        {
            placeables[i] = new Placeable[tiles[i].Length];
            for (int j = 0; j < placeables[i].Length; j++)
            {
                placeables[i][j] = null;
            }
        }
        this.map = map;
    }
    

    //Renvoi les ressources actuelles du village
    public int[] GetResources()
    {
        return (int[])resources.Clone();
    }
    
    //Récupère les besoins en ressources de toutes les structures du village
    private int[] GetNeededResources()
    {
        int[] neededResources = new int[resources.Length];
        for (int i = 0; i < placeables.Length; i++)
        {
            for (int j = 0; j < placeables[i].Length; j++)
            {
                Placeable currentPlaceable = placeables[i][j];
                if (currentPlaceable != null)
                {
                    int[] needs = currentPlaceable.getResourceNeeds();
                    for (int c = 0; c < needs.Length; c++)
                    {
                        neededResources[c] += neededResources[c];
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

        this.tiles = new TileType[largeur][];
        for (int i = 0; i < largeur; i++)
        {
            this.tiles[i] = new TileType[hauteur];
        }
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
                    this.tiles[x][y] = TileType.GRASS;
                }
                else if(tileID ==1){
                    this.tiles[x][y] = TileType.WATER;
                }
            }
        }
        NotifyTilesChange();
    }

    //"Joue le tour" pour les structures et permet de récupérer les ressources 
    private void ProductResources()
    {
        //On récupère le besoin en ressource
        int[] neededResources = GetNeededResources();
        //Et pour chaque bâtiment :
        for (int i = 0; i < placeables.Length; i++)
        {
            for (int j = 0; j < placeables[i].Length; j++)
            {
                //On lui demande de produire en fonction des ressources disponibles
                if(placeables[i][j]!=null) placeables[i][j].ProductResources(resources, neededResources);
            }
        }
        NotifyResourcesChange();
    }

    //Calcule le % de remplissage des besoins du village
    public double FulfilementOfNeeds(Dictionary usableResources,
        Godot.Collections.Dictionary<ResourceType, int> neededResources)
    {
        return 0;
    }
    
    public void SetBuildingStrategy(BuildingStrategy strategy)
    {
        this.strategy = strategy;
    }

    private void ApplyStrategy()
    {
        //strategy.BuildNewPlaceable();
        NotifyPlaceableChange();
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

    public TileType[][] GetTiles()
    {
        return tiles;
    }

    public void StartVillage()
    {
        placeables[6][2] = factory.CreateHouse();
        placeables[6][0] = factory.CreateHouse();
        placeables[8][2] = factory.CreateHouse();
        placeables[6][4] = factory.CreateBar();
        placeables[11][9] = factory.CreateSawmill();
        placeables[14][16] = factory.CreateTrainStation();
        placeables[15][11] = factory.CreateField();
        placeables[16][11] = factory.CreateField();
        placeables[15][10] = factory.CreateField();
        placeables[16][10] = factory.CreateField();
        NotifyPlaceableChange();
    }

    public void NextTurn()
    {
        ProductResources();
        ApplyStrategy();
    }
}
