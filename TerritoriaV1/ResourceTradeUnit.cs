using Godot;
using System;

public partial class ResourceTradeUnit : Control
{
	[Export] private Texture2D icon;
	private TerritoriaSlider exportTerritoriaSlider;
	private TerritoriaSlider importTerritoriaSlider;
	private Label totalValueLabel;
	private int[] exchangeRate = new int[2];
	
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		TextureRect textureRect = FindChild("TextureRect") as TextureRect;
		textureRect.Texture = icon;
		exportTerritoriaSlider =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/ExportSlider") as TerritoriaSlider;
		Action myAction = () => {ValueChanged(); };
		exportTerritoriaSlider.Connect(TerritoriaSlider.SignalName.ValueChanged,Callable.From(myAction));
		importTerritoriaSlider =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/ImportSlider") as TerritoriaSlider;
		importTerritoriaSlider.Connect(TerritoriaSlider.SignalName.ValueChanged,Callable.From(myAction));
		totalValueLabel =
			GetNode("./PanelContainer/MarginContainer/VBoxContainer/HBoxContainer2/TotalValueLabel") as Label;
			int size =  totalValueLabel.LabelSettings.FontSize;
			totalValueLabel.LabelSettings = new LabelSettings();
			totalValueLabel.LabelSettings.FontSize = size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void ValueChanged()
	{
		int total = exportTerritoriaSlider.GetSliderValue() * exchangeRate[1] - importTerritoriaSlider.GetSliderValue() * exchangeRate[0];
		totalValueLabel.Text = total + " €";
		if(total==0) this.totalValueLabel.LabelSettings.FontColor = Colors.White;
		else if(total>0) this.totalValueLabel.LabelSettings.FontColor = Colors.Green;
		else this.totalValueLabel.LabelSettings.FontColor = Colors.Red;
	}

	public int GetTotal()
	{
		return exportTerritoriaSlider.GetSliderValue() - importTerritoriaSlider.GetSliderValue();
	}
	public int GetExportValue(){
		return exportTerritoriaSlider.GetSliderValue();
	}
	public int GetImportValue(){
		return importTerritoriaSlider.GetSliderValue();
	}
	public void SetExportMax(int max)
	{
		if(exportTerritoriaSlider.GetSliderValue()<=max) exportTerritoriaSlider.UpdateSliderMax(max);
	}

	public void SetExchangeRate(int[] exchangeRate)
	{
		this.exchangeRate[0] = exchangeRate[0];
		this.exchangeRate[1] = exchangeRate[1];
	}
	[Signal] public delegate void TotalChangedEventHandler(int total);
}
