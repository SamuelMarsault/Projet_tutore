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
}
