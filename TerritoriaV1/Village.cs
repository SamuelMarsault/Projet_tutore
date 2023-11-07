using Godot;

using System.Collections.Generic;
using Godot.Collections;
using TerritoriaV1;

public class Village
{
    private BuildingStrategy strategy;
    private List<VillageObserver> observers = new List<VillageObserver>();
    private PlaceableFactory factory = new PlaceableFactory();
    private List<Placeable> placeables = new List<Placeable>();
    private TileType[][] tiles;

    private Godot.Collections.Dictionary<ResourceType, int> resources;

    public Village()
    {
        //Par défaut la stratégie est la croissance
        strategy = new BuildingGrowthStrategy();
        //On initialise le dictionnaire de ressources
        resources = new Godot.Collections.Dictionary<ResourceType, int>();
        //Définition du terrain :
        tiles = new TileType[][]
        {
            new[] { TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS },
            new[] { TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.GRASS },
            new[] { TileType.WATER, TileType.WATER, TileType.WATER, TileType.GRASS, TileType.GRASS },
            new[] { TileType.GRASS, TileType.GRASS, TileType.WATER, TileType.WATER, TileType.GRASS },
            new[] { TileType.GRASS, TileType.GRASS, TileType.GRASS, TileType.WATER, TileType.GRASS}
        };
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

    public void setBuilding(BuildingStrategy strategy)
    {
        this.strategy = strategy;
    }
    public void addObservers(VillageObserver observer)
    {
        observers.Add(observer);
    }

    public Vector2 seekCompatibleTiles(Vector2 position, BuildingType type)
    {
        return Vector2.Down;
    }

}
