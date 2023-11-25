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
			int size =  totalValueLabel.LabelSettings.FontSize;
			totalValueLabel.LabelSettings = new LabelSettings();
			totalValueLabel.LabelSettings.FontSize = size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		int total = exportTerritoriaSlider.GetSliderValue() - importTerritoriaSlider.GetSliderValue();
		totalValueLabel.Text = total + " €";
		if(total==0) this.totalValueLabel.LabelSettings.FontColor = Colors.White;
		else if(total>0) this.totalValueLabel.LabelSettings.FontColor = Colors.Green;
		else this.totalValueLabel.LabelSettings.FontColor = Colors.Red;
		//exportTerritoriaSlider.UpdateSliderMax((exportTerritoriaSlider.GetMaxSliderValue())-(importTerritoriaSlider.GetSliderValue()));
		//importTerritoriaSlider.UpdateSliderMax((importTerritoriaSlider.GetMaxSliderValue())-(exportTerritoriaSlider.GetSliderValue()));
	}

	public int getExportValue(){
		return exportTerritoriaSlider.GetSliderValue();
	}

	public int getImportValue(){
		return importTerritoriaSlider.GetSliderValue();
	}
}
