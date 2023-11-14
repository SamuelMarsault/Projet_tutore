namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;
    public void determineStrategy()
    {
        village.setBuildingStrategy(new BuildingGrowthStrategy(village.getTiles()));
    }
}