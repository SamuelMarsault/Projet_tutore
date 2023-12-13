using System;
using Godot;
using Godot.Collections;

namespace TerritoriaV1;
//Faire que pour chaque tour il faux retirer des matériaux en fonction de se que l'on veux construire
public class PlaceableFactory
{
    public Placeable CreateHouse()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        //input[(int)ResourceType.WOOD] = 1;
        output[(int)ResourceType.MONEY] = 2;
        Placeable house = new Placeable(PlaceableType.HOUSE,input, output,150);
        return house;
    }

    public Placeable CreateSawmill()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.WOOD] = 2;
        Placeable sawmill = new Placeable(PlaceableType.SAWMILL,input, output,50);
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
        output[(int)ResourceType.MONEY] = 4;
        Placeable sawmill = new Placeable(PlaceableType.BAR,input, output,150);
        return sawmill;
    }

    public Placeable CreateField()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.MONEY] = 2;
        output[(int)ResourceType.HOP] = 6;
        Placeable field = new Placeable(PlaceableType.FIELD,input, output,50);
        return field;
    }

    public Placeable CreateIceUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        output[(int)ResourceType.ICE] = 10;
        Placeable ice_usine = new Placeable(PlaceableType.ICE_USINE,input, output,50);
        return ice_usine;
    }

    public Placeable CreateBeerUsine()
    {
        int[] input = new int[Enum.GetNames(typeof(ResourceType)).Length];
        int[] output = new int[Enum.GetNames(typeof(ResourceType)).Length];
        input[(int)ResourceType.ICE] = 5;
        input[(int)ResourceType.HOP] = 15;
        output[(int)ResourceType.BEER] = 4;
        Placeable beer_usine = new Placeable(PlaceableType.BEER_USINE,input, output,50);
        return beer_usine;
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
