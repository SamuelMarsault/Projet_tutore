using Godot;
using System.Collections.Generic;
using TerritoriaV1;

public class VillageManager
{
    private Village village;
    private EvolutionOfVillage evolutionOfVillage;
    public bool change = true;

    int[] oldressources;
    public VillageManager(TileMap map, Printer printer,Trader trader, EvolutionOfVillage evolutionOfVillage)
    {
        village = new Village(map);
        this.evolutionOfVillage = evolutionOfVillage;
        evolutionOfVillage.SetVillage(village);
        
        village.AddObservers(map);
        village.AddObservers(printer);
        village.AddObservers(trader);

        village.StartVillage();
        oldressources = village.GetResources();
    }

    public void NextTurn(int[] export, int[] import, int[] money)
    {
        GD.Print("------------------------------------------- next turn");

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

        int[] newResources =  village.GetResources();

        change = false;
        for(int i = 0; i < newResources.Length; i++)
        {
            GD.Print("current "+oldressources[i]); GD.Print("new "+newResources[i]); 
            if(oldressources[i] != newResources[i])
            {
                change = true;
                GD.Print("changement");
            }
        }
        oldressources = village.GetResources();
        
    }

    public void applyNextTurn(bool confirm)
    {    change = false;
        village.continueNextTurn(confirm);
        int[] newResources =  village.GetResources();

        for(int i = 0; i < newResources.Length; i++)
        {
            GD.Print("current "+oldressources[i]); GD.Print("new "+newResources[i]); 
            if(oldressources[i] != newResources[i])
            {
                change = true;
                GD.Print("changement");
            }
        }
        oldressources = village.GetResources();

    }

    public Village GetVillage()
    {
        return this.village;
    }

    public bool IsVillageOk()
    {
        bool ok = false;
        Placeable[,] placeables = village.GetPlaceables();
        for (int i = 0; i < placeables.GetLength(0) && !ok; i++)
        {
            for (int j = 0; j < placeables.GetLength(1) && !ok; j++)
            {
                if (placeables[i,j]!=null && placeables[i,j].getPlaceableType()==PlaceableType.HOUSE)
                {
                    ok = true;
                }
            }
        }

        return ok;
    }
}
