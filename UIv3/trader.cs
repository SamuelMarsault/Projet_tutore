using Godot;
using System;

public partial class trader : Node,gameObserver
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void reactToRessourcesUpdate(RESSOURCES ressource, int newValue)
	{
		switch (ressource)
		{
			case RESSOURCES.WOOD : break;
    		case RESSOURCES.BEER : break;
    		case RESSOURCES.HOP : break;
    		case RESSOURCES.ICE : break;
    		case RESSOURCES.MONEY : break;
			default: break;
		}
	}
}
