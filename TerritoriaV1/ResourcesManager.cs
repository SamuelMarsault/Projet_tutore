using Godot;
using System;
using System.Collections.Generic;

public partial class ResourcesManager
{
    // Dictionnaire correspondant aux ressources disponibles
    private Dictionary<ResourceType, int> resources;

    public Dictionary<ResourceType, int> collectResources(BuildingType buildingType, int nbBuildings) {
        Dictionary<ResourceType, int> resourcesCollected = new Dictionary<ResourceType, int>();
        switch (buildingType) {
            case (BuildingType.HOUSE) :
                resourcesCollected.Add(ResourceType.MONEY, 100 * nbBuildings);
                break;
            case (BuildingType.SAWMILL) :
                resourcesCollected.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.FOREST) :
                resourcesCollected.Add(ResourceType.WOOD, 100 * nbBuildings);
                break;
            case (BuildingType.BAR) :
                resourcesCollected.Add(ResourceType.MONEY, 200 * nbBuildings);
                break;
            default :
                break;
        }
        return resourcesCollected;
    }

    public Dictionary<ResourceType, int> seeResourceNeeds(BuildingType buildingType, int nbBuildings) {
        Dictionary<ResourceType, int> resourcesNeeded = new Dictionary<ResourceType, int>();
        switch (buildingType) {
            case (BuildingType.HOUSE) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.SAWMILL) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.RAIL) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.FOREST) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.BAR) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            case (BuildingType.TRAIN_STATION) :
                resourcesNeeded.Add(ResourceType.PLANK, 100 * nbBuildings);
                break;
            default :
                break;
        }
        return resourcesNeeded;
    }

    public void nextTurn() {
    }
}