using System;
using Godot;
using Godot.Collections;

namespace TerritoriaV1;
/// <summary>
/// construit des placeables ( batiments)
/// </summary>
public class PlaceableFactory
{
    /// <summary>
    /// crée une maison
    /// </summary>
    /// <returns>une maison</returns>
    public Placeable CreateHouse()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.BEER] = 1;
        output[(int)ResourceType.MONEY] = 6;
        Placeable house = new Placeable(PlaceableType.HOUSE,input, output,5);
        return house;
    }

    /// <summary>
    /// crée une scierie
    /// </summary>
    /// <returns>une scierie</returns>
    public Placeable CreateSawmill()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.WOOD] = 1;
        Placeable sawmill = new Placeable(PlaceableType.SAWMILL,input, output,5);
        return sawmill;
    }

/// <summary>
/// crée une gare
/// </summary>
/// <returns>une gare</returns>
    public Placeable CreateTrainStation()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        Placeable trainStation = new Placeable(PlaceableType.TRAIN_STATION,input, output,0);
        return trainStation;   
    }

/// <summary>
/// crée un bar
/// </summary>
/// <returns>un bar</returns>
    public Placeable CreateBar()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.BEER] = 1;
        output[(int)ResourceType.MONEY] = 5;
        Placeable bar = new Placeable(PlaceableType.BAR,input, output,0);
        return bar;
    }

    /// <summary>
    /// crée un champ
    /// </summary>
    /// <returns>un champ</returns>
    public Placeable CreateField()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.HOP] = 1;
        Placeable field = new Placeable(PlaceableType.FIELD,input, output,5);
        return field;
    }

/// <summary>
/// crée une usine a glacon
/// </summary>
/// <returns></returns>
    public Placeable CreateIceUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.ICE] = 1;
        Placeable ice_usine = new Placeable(PlaceableType.ICE_USINE,input, output,5);
        return ice_usine;
    }
    /// <summary>
    /// crée une usine à bière
    /// </summary>
    /// <returns>une usine à bière</returns>
    public Placeable CreateBeerUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.ICE] = 1;
        input[(int)ResourceType.HOP] = 1;
        output[(int)ResourceType.BEER] = 1;
        Placeable beer_usine = new Placeable(PlaceableType.BEER_USINE,input, output,10);
        return beer_usine;
    }
}
