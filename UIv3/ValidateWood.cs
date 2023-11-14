using Godot;
using System;

public partial class ValidateWood : Button
{

	int woodValue;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_h_slider_wood_value_changed(float value)
	{	
		int temp = ((int)value*ressourcesDictionnary.ressourcesTradCost[RESSOURCES.WOOD]);

		if(value > 0)
		{
			this.Text = "VALIDER : Payer "+ temp.ToString() + " euro(s)";
		}
		else
		{
			this.Text = "VALIDER : Recevoir "+ (-1*(temp/2)).ToString() + " euro(s)";
		}
	}
}

