using Godot.Collections;

namespace TerritoriaV1;

public class PlaceableFactory
{
    public Placeable createHouse()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.MONEY,2);
        Building house = new Building(input, output,200);
        return house;
    }

    public Placeable createSawmill()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        input.Add(ResourceType.WOOD,1);
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.PLANK,2);
        Building sawmill = new Building(input, output,150);
        return sawmill;
    }

    public Placeable createRail()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        Building rail = new Building(input, output,150);
        return rail;
    }

    public Placeable createBar()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        input.Add(ResourceType.BEER,1);
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.MONEY,5);
        Building sawmill = new Building(input, output,300);
        return sawmill;
    }

    public Placeable TrainStation()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        Building trainStation = new Building(input, output,150);
        return trainStation;   
    }

    public Placeable createForest()
    {
        Resource forest = new Resource(ResourceType.WOOD,1000);
        return forest;
    }

    public Placeable createField()
    {
        Dictionary<ResourceType, int> input = new Dictionary<ResourceType, int>();
        Dictionary<ResourceType, int> output = new Dictionary<ResourceType, int>();
        output.Add(ResourceType.HOP,10);
        Building field = new Building(input, output,50);
        return field;
    }
}