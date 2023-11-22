using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public class PlaceableFactory
{
    public Placeable createHouse(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.MONEY,2);
        Building house = new Building(position, PlaceableType.HOUSE,input, output,200);
        return house;
    }

    public Placeable createSawmill(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        input.Add(ResourceType.WOOD,1);
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.PLANK,2);
        Building sawmill = new Building(position, PlaceableType.SAWMILL,input, output,150);
        return sawmill;
    }

    public Placeable createRail(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        Building rail = new Building(position, PlaceableType.RAIL,input, output,0);
        return rail;
    }

    public Placeable createTrainStation(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        Building trainStation = new Building(position, PlaceableType.TRAIN_STATION,input, output,0);
        return trainStation;   
    }

    public Placeable createBar(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        input.Add(ResourceType.BEER,1);
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.MONEY,5);
        Building sawmill = new Building(position, PlaceableType.BAR,input, output,300);
        return sawmill;
    }

    public Placeable createForest(Vector2I position)
    {
        Resource forest = new Resource(position, PlaceableType.FOREST, ResourceType.WOOD,1000);
        return forest;
    }

    public Placeable createField(Vector2I position)
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.HOP,10);
        Building field = new Building(position, PlaceableType.FIELD,input, output,50);
        return field;
    }
}
