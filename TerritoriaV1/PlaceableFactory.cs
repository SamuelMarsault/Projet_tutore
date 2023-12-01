using System;
using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public class PlaceableFactory
{
    public Placeable CreateHouse()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.MONEY] = 2;
        Placeable house = new Placeable(PlaceableType.HOUSE,input, output,200);
        return house;
    }

    public Placeable CreateSawmill()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.WOOD] = 2;
        Placeable sawmill = new Placeable(PlaceableType.SAWMILL,input, output,150);
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
        Placeable sawmill = new Placeable(PlaceableType.BAR,input, output,300);
        return sawmill;
    }

    public Placeable CreateField()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.HOP] = 10;
        Placeable field = new Placeable(PlaceableType.FIELD,input, output,50);
        return field;
    }

    public Placeable CreateIceUsine()
    {
        return null;
    }

    public Placeable CreateBeerUsine()
    {
        return null;
    }

    public void Destroy(PlaceableType type)
    {
        /*
            détruit un batiment au hasard du type choisi
            devrait pouvoir renvoyer les coordonnées du batiment détruit
            si on part sur un syteme ou le joueur peut positionner les batiments, devrait etre géré par un noeud
        */
    }
}
