using Godot;
using System.Collections.Generic;
using TerritoriaV1;

public class VillageManager
{
    // Dictionnaire correspondant aux différents bâtiments du village classés par type
    private Dictionary<PlaceableType, List<Placeable>> placeables;
    private Village village;
    private EvolutionOfVillage evolutionOfVillage;
    public VillageManager(TileMap map, Printer printer,Trader trader, EvolutionOfVillage evolutionOfVillage)
    {
        village = new Village(map);
        this.evolutionOfVillage = evolutionOfVillage;
        evolutionOfVillage.SetVillage(village);
        
        village.AddObservers(map);
        village.AddObservers(printer);
        village.AddObservers(trader);

        village.StartVillage();
    }

    public void NextTurn(int[] export, int[] import, int[] money)
    {
        /*for(int i = 0; i < export.Length; i++)
        {
            GD.Print("VM-export["+i+"] :" +export[i]);
        }

        for(int i = 0; i < import.Length; i++)
        {
            GD.Print("VM-import["+i+"] :" +import[i]);
        }*/


        evolutionOfVillage.DetermineStrategy();
        village.NextTurn(export, import, money);
    }

    public void applyNextTurn(bool confirm)
    {
        village.continueNextTurn(confirm);
    }

    public Village GetVillage()
    {
        return this.village;
    }
}
