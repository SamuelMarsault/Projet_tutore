using Godot;
using System;

public partial class BuildingStrategyFactory
{
    public void createGrowthStrategy() {
    }

    public void createDecayStrategy() {
    }

    public BuildingStrategy createPrimaryStrategy()
    {
        return new PrimaryStrat();
    }

    public BuildingStrategy createSecondaryStrategy()
    {
        return new SecondaryStrat();
    }

    public BuildingStrategy createTertiaryStrategy()
    {
        return new TertiaryStrat();
    }
}
