using Godot;
using System;
using System.Collections.Generic;

public partial class VillageManager
{
    // Dictionnaire correspondant aux différents bâtiments du village classés par type
    private Dictionary<PlaceableType, List<Building>> placeables;
        private TileMap map;
    private Village village;
    public VillageManager(TileMap map){
        this.map = map;
        village = new Village(map);
    }

    public void askNeededResources() {
    }

    public void nextTurn() {
    }
}
