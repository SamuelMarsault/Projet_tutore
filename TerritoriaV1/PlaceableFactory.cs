using System;
namespace TerritoriaV1;
//Faire que pour chaque tour il faux retirer des matériaux en fonction de se que l'on veux construire
public class PlaceableFactory
{
    public Placeable CreateHouse()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        Placeable house = new Placeable(PlaceableType.HOUSE,input, output,0);
        return house;
    }

    public Placeable CreateSawmill()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 1;
        output[(int)ResourceType.WOOD] = 1;
        Placeable sawmill = new Placeable(PlaceableType.SAWMILL,input, output,5);
        return sawmill;
    }

    public Placeable CreateTrainStation()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        Placeable trainStation = new Placeable(PlaceableType.TRAIN_STATION,input, output,0);
        return trainStation;   
    }

    public Placeable CreateBar()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.BEER] = 1;
        output[(int)ResourceType.MONEY] = 5;
        Placeable bar = new Placeable(PlaceableType.BAR,input, output,0);
        return bar;
    }

    public Placeable CreateField()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 1;
        output[(int)ResourceType.HOP] = 1;
        Placeable field = new Placeable(PlaceableType.FIELD,input, output,5);
        return field;
    }

    public Placeable CreateIceUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.ICE] = 1;
        Placeable ice_usine = new Placeable(PlaceableType.ICE_USINE,input, output,5);
        return ice_usine;
    }

    public Placeable CreateBeerUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.ICE] = 1;
        input[(int)ResourceType.HOP] = 1;
        output[(int)ResourceType.BEER] = 2;
        Placeable beer_usine = new Placeable(PlaceableType.BEER_USINE,input, output,20);
        return beer_usine;
    }
}
