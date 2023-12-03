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
    private Printer printer;
    public VillageManager(TileMap map, Printer printer){
        this.map = map;
        this.printer = printer;
        evolutionOfVillage = new EvolutionOfVillage();
        village = new Village(this.map, this.printer);
        //evolutionOfVillage.SetVillage(village);
        village.AddObservers(this.map);
        village.AddObservers(this.printer);
        village.StartVillage();
    }

    public void NextTurn(int[] export, int[] import) {
        //evolutionOfVillage.DetermineStrategy();
        village.NextTurn(export, import);
    }
}
