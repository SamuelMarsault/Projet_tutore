using Godot;
using System;

public partial class trader : Node,gameObserver
{
	gameManager GM;	

	int hWoodValue;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GM = (gameManager) this.GetParent();
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

	public void _on_validate_pressed()
	{
		GM.funcname(RESSOURCES.WOOD,hWoodValue);
	}

	public void _on_h_slider_value_changed(float value)
	{
		hWoodValue =  (int)value;
	}
}
