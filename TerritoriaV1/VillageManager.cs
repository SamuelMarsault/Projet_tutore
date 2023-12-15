using Godot;
using System.Collections.Generic;
using TerritoriaV1;

public class VillageManager
{
    // Dictionnaire correspondant aux différents bâtiments du village classés par type
    private Dictionary<PlaceableType, List<Placeable>> placeables;
    private Village village;
    private EvolutionOfVillage evolutionOfVillage = new EvolutionOfVillage();
    public VillageManager(TileMap map, Printer printer,Trader trader){
        village = new Village(map);
        //evolutionOfVillage.SetVillage(village);
        village.AddObservers(map);
        village.AddObservers(printer);
        village.AddObservers(trader);
        village.StartVillage();
        GD.Print("taux de change actualisé");
    }

    public void NextTurn(int[] export, int[] import) {
        //evolutionOfVillage.DetermineStrategy();
        village.NextTurn(export, import);
    }

    public void applyNextTurn(bool confirm){
        village.continueNextTurn(confirm);
    }
}
