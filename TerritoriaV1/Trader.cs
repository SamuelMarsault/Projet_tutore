using Godot;
using System;
using System.Collections.Generic;
using TerritoriaV1;

public partial class Trader : Node, VillageObserver
{

	private Node parent;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		parent = GetParent();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

    public void reactToResourcesChange(Godot.Collections.Dictionary<ResourceType, int> resources)
    {

    }

    public void reactToPlaceableChange(List<Placeable> placeables)
    {
        throw new NotImplementedException();
    }

    public void reactToTilesChange(TileType[][] tiles)
    {
        throw new NotImplementedException();
    }

	public void _on_button_pressed(){
		((GameManager)parent).nextTurn();
	}

	public int getNewResource(ResourceType ressource){
		int nbNewRessource = 0;
		if(ressource == ResourceType.WOOD){
			ResourceTradeUnit change = GetNode("./Control/MarginContainer/VBoxContainer/ResourcesContainer/Control") as ResourceTradeUnit;
			nbNewRessource = change.getExportValue() - change.getImportValue();
		}
		else if(ressource == ResourceType.HOP){
			ResourceTradeUnit change = GetNode("./Control/MarginContainer/VBoxContainer/ResourcesContainer/Control2") as ResourceTradeUnit;
			nbNewRessource = change.getExportValue() - change.getImportValue();
		}
		else if(ressource == ResourceType.ICE){
			ResourceTradeUnit change = GetNode("./Control/MarginContainer/VBoxContainer/ResourcesContainer2/Control3") as ResourceTradeUnit;
			nbNewRessource = change.getExportValue() - change.getImportValue();
		}
		else if(ressource == ResourceType.BEER){
			ResourceTradeUnit change = GetNode("./Control/MarginContainer/VBoxContainer/ResourcesContainer2/Control4") as ResourceTradeUnit;
			nbNewRessource = change.getExportValue() - change.getImportValue();
		}
		return nbNewRessource;
	}
}