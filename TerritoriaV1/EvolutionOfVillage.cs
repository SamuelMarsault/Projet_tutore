using Godot.Collections;

namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;

    private int[] ressources;
    private int[] neededRessources;    
    private int[] NBPlaceables;

    public void DetermineStrategy()
    {
        BuildingStrategyFactory factory = new BuildingStrategyFactory();

        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();

        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && this.NBPlaceables[(int)PlaceableType.FIELD] == 0 && NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0)
        {
            village.SetBuildingStrategy(factory.Tertiary());
        }
        else if(ressources[(int)ResourceType.MONEY]>10)  // 10 euro, peut etre augmenté
        {
            village.SetBuildingStrategy(factory.Secondary());
        }
        else
        {
            village.SetBuildingStrategy(factory.Primary());
        }
    }

    public void SetVillage(Village village)
    {
        this.village = village;
    }
}
