using Godot;

using System.Collections.Generic;
using Godot.Collections;
using TerritoriaV1;

public class Village
{
    private BuildingStrategy strategy;
    private List<VillageObserver> observers = new ();
    private PlaceableFactory factory = new ();
    private List<Placeable> placeables = new ();
    private TileType[][] tiles;
    private Godot.Collections.Dictionary<ResourceType, int> resources;
    private TileMap map;
    public Village(TileMap ground)
    {
        //Par défaut la stratégie est la croissance
        strategy = new BuildingGrowthStrategy(tiles);
        //On initialise le dictionnaire de ressources
        resources = new Godot.Collections.Dictionary<ResourceType, int>();
        //Définition du terrain :
        this.map = ground;
        initialiseTile();
        initialisePleasable();
    }

    //Renvoi les ressources actuelles du village
    public Godot.Collections.Dictionary<ResourceType, int> getResources()
    {
        return resources.Duplicate(true);
    }
    //Récupère les besoins en ressources de toutes les structures du village
    private Godot.Collections.Dictionary<ResourceType, int> getNeededResources()
    {
        Godot.Collections.Dictionary<ResourceType, int> neededResources =
            new Godot.Collections.Dictionary<ResourceType, int>();
        foreach (Placeable placeable in placeables)
        {
            foreach (var (type,value) in placeable.getResourceNeeds() )
            {
                //Si elle n'a encore jamais été ajoutée on l'ajoute
                if (!neededResources.TryAdd(type,value)) 
                    neededResources[type] += value; //Sinon on incrémente juste la valeur
            }
        }
        return neededResources;
    }

    //Initializes a 2D table containing the type of soil
    private void initialiseTile(){
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
        addObservers(map);
    }
    //Initialize basic buildings
    private void initialisePleasable(){
       placeables.Add(factory.createHouse(new Vector2I(6,-1)));
       placeables.Add(factory.createHouse(new Vector2I(6,-3)));
       placeables.Add(factory.createHouse(new Vector2I(8,-1)));
       placeables.Add(factory.createBar(new Vector2I(6,2)));
       placeables.Add(factory.createSawmill(new Vector2I(11,7)));
       placeables.Add(factory.createTrainStation(new Vector2I(14,14)));
       placeables.Add(factory.createField(new Vector2I(15,9)));
       placeables.Add(factory.createField(new Vector2I(16,9)));
       placeables.Add(factory.createField(new Vector2I(15,8)));
       placeables.Add(factory.createField(new Vector2I(16,8)));
       notifySetPleaceable();
    }

    //"Joue le tour" pour les structures et permet de récupérer les ressources 
    public void productResources()
    {
        //On ajoute chaque ressources aux ressources du village
        foreach (var (type,value) in this.getAvailableResources() )
        {
            //Si elle n'a encore jamais été ajoutée on l'ajoute
            if (!resources.TryAdd(type,value))
                resources[type] += value; //Sinon on incrémente juste la valeur
        }
        //On récupère le besoin en ressource
        Godot.Collections.Dictionary<ResourceType, int> neededResources = getNeededResources();
        //Et pour chaque bâtiment :
        foreach (Placeable placeable in placeables)
        {
            //On lui demande de produire en fonction des ressources disponibles
            placeable.productResources(resources,neededResources);
        }
    }
    
    //Récupère les disponibilités en ressources de toutes les structures du village
    private Godot.Collections.Dictionary<ResourceType, int> getAvailableResources()
    {
        Godot.Collections.Dictionary<ResourceType, int> availableResources =
            new Godot.Collections.Dictionary<ResourceType, int>();
        foreach (Placeable placeable in placeables)
        {
            foreach (var (type,value) in placeable.getAvailableResources() )
            {
                //Si elle n'a encore jamais été ajoutée on l'ajoute
                if (!availableResources.TryAdd(type,value)) 
                    availableResources[type] += value; //Sinon on incrémente juste la valeur
            }
        }
        return availableResources;
    }

    //Calcule le % de remplissage des besoins du village
    public double fulfilementOfNeeds(Dictionary usableResources, 
        Godot.Collections.Dictionary<ResourceType,int> neededResources)
    {
        return 0;
    }
    //Allows you to modify a Tile
    public void setTiles(int x, int y, int layer){
        Vector2I updateTile = new Vector2I(x,y);
        int ID;
        if(map.GetCellSourceId(0,updateTile) == 0){
            this.tiles[x][y] = TileType.GRASS;
            ID = 0;
        }
        else{
            this.tiles[x][y] = TileType.WATER;
            ID = 1;
        }
        notifyTilesType(updateTile, layer, ID);
    }
    //Retrieves a Tile
    public TileType getTile(int x, int y){
        return tiles[x][y];
    }
    
    public void setBuildingStrategy(BuildingStrategy strategy)
    {
        this.strategy = strategy;
    }

    public void applyStrategy()
    {
        
    }
    public void addObservers(VillageObserver observer)
    {
        observers.Add(observer);
    }

    private void notifyResourcesChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.reactToResourcesChange(resources.Duplicate(true));
        }
    }
    private void notifyPlaceableChange()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.reactToPlaceableChange(placeables);
        }
    }
    
    private void notifyTilesType(Vector2I updateTile, int layeur, int ID)
    {
        foreach (VillageObserver observer in observers)
        {
            observer.reactToTilesChangesTiles(updateTile, layeur, ID);
        }
    }

    private void notifySetPleaceable()
    {
        foreach (VillageObserver observer in observers)
        {
            observer.reactToInitialisePlaceable(placeables);
        }
    }

    public TileType[][] getTiles()
    {
        return tiles;
    }
}
