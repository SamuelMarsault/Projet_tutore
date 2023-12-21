using Godot;
using System.Collections.Generic;
using TerritoriaV1;

public class VillageManager
{
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

        village.StartVillage(); }

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
        
    }

    public void applyNextTurn(bool confirm)
    {   
        village.continueNextTurn(confirm);
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

    public int getNumberCitizen()
    {
        int nbCitizen = 0;

        foreach(Placeable placeable in village.GetPlaceables())
        {
            if(placeable != null && placeable.getPlaceableType() == PlaceableType.HOUSE)
            {
                nbCitizen+=10;
            }
        }

        return nbCitizen;
    }

      public void setMessage(bool display){
        village.setMessageNeedResources(display);
    }

    public Placeable getPlaceable(int x, int y){
        return village.getPlaceable(x,y);
    }
}
