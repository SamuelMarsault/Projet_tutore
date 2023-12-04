using Godot;
using System;
using System.Runtime.InteropServices;

public partial class BuildingStrategyFactory
{
    public BuildingStrategy Primary() 
    {
        return new PrimaryStrat();
    }

    public BuildingStrategy Secondary() 
    {
        return new SecondaryStrat();
    }

    public BuildingStrategy Tertiary() 
    {
        return new TertiaryStrat();
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
