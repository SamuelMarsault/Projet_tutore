using Godot;
using System;

public partial class ResourceTradeUnit : Control
{
	[Export] private Texture2D icon;
	private TerritoriaSlider exportTerritoriaSlider;
	private TerritoriaSlider importTerritoriaSlider;
	private Label totalValueLabel;
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TextureRect textureRect = FindChild("TextureRect") as TextureRect;
		textureRect.Texture = icon;
		exportTerritoriaSlider =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/ExportSlider") as TerritoriaSlider;
		importTerritoriaSlider =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/ImportSlider") as TerritoriaSlider;
		totalValueLabel =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/HBoxContainer2/TotalValueLabel") as Label;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int value = exportTerritoriaSlider.GetSliderValue() - importTerritoriaSlider.GetSliderValue();
		totalValueLabel.Text = value + " €";
	}
}
