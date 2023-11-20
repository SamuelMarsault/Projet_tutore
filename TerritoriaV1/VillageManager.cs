using Godot;
using System;
using System.Collections.Generic;

public partial class VillageManager
{
    // Dictionnaire correspondant aux différents bâtiments du village classés par type
    private Dictionary<PlaceableType, List<Building>> placeables;

    public Vector2 seekCompatibleTiles(Vector2 position, PlaceableType type) {
        return position;
    }

    public void askNeededResources() {
    }
    

    public void nextTurn() {
    }
}
