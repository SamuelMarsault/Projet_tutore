using Godot.Collections;

namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;

    private int[] ressources;
    private int[] neededRessources;    
    private int[] NBPlaceables;
    private BuildingStrategyFactory factory = new BuildingStrategyFactory();

    public void DetermineStrategy()
    {

        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();

        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && this.NBPlaceables[(int)PlaceableType.FIELD] == 0 && NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0)  // cette condition ne sera jamais remplit
        {
            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
        }
        else if(ressources[(int)ResourceType.MONEY]>10000)
        {
            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
        }
        else
        {
            village.SetBuildingStrategy(factory.createPrimaryStrategy(village.GetPlaceables(),village.GetTiles()));
        }
    }

    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}
