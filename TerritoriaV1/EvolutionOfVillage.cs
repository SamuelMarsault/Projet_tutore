namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;
    public void DetermineStrategy()
    {
        village.SetBuildingStrategy(new BuildingGrowthStrategy(village.GetTiles()));
    }

    public void SetVillage(Village village)
    {
        this.village = village;
    }
}