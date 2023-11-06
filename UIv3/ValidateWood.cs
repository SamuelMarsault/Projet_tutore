using Godot;
using System;

public partial class ValidateWood : Button
{
	int tauxDeChangeAchatBois;

	int woodValue;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void _on_h_slider_value_changed(float value)
	{
		tauxDeChangeAchatBois = ressourcesDictionnary.ressourcesTradCost[RESSOURCES.WOOD];
		woodValue = (int)value;
		this.Text = "VALIDER : "+ (woodValue*tauxDeChangeAchatBois).ToString() + " euro";
	}
}

