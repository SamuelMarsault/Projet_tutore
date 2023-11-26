using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class VillageManager
{
    // Dictionnaire correspondant aux différents bâtiments du village classés par type
    private Dictionary<PlaceableType, List<Placeable>> placeables;
    private TileMap map;
    private Village village;
    private EvolutionOfVillage evolutionOfVillage;
    public VillageManager(TileMap map){
        this.map = map;
        evolutionOfVillage = new EvolutionOfVillage();
        village = new Village(map);
        evolutionOfVillage.SetVillage(village);
        village.AddObservers(this.map);
        village.StartVillage();
    }

    public void NextTurn(int[] export, int[] import, int total) {
        evolutionOfVillage.DetermineStrategy();
        village.NextTurn();
        Console.WriteLine("Passage au tour suivant");
        Console.WriteLine("Copain ?");
    }
}
