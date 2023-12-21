using Godot;
using System;

public partial class ResourceTradeUnit : Control
{
	[Export] private Texture2D icon;
	private TerritoriaSlider exportTerritoriaSlider;
	private TerritoriaSlider importTerritoriaSlider;
	private Label totalValueLabel;
	private int[] exchangeRate = new int[2];
	
	
	/// <summary>
	/// Récupère ses enfants pour mettre la bonne icône et connecter les signaux
	/// </summary>
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
	/// <summary>
	/// Actualise le total en fonction des changements de ses fils
	/// </summary>
	public void ValueChanged()
	{
		int total = exportTerritoriaSlider.GetSliderValue() * exchangeRate[1] - importTerritoriaSlider.GetSliderValue() * exchangeRate[0];
		totalValueLabel.Text = total + " €";
		
		if(total==0) this.totalValueLabel.LabelSettings.FontColor = Colors.White;
		else if(total>0) this.totalValueLabel.LabelSettings.FontColor = Colors.Green;
		else this.totalValueLabel.LabelSettings.FontColor = Colors.Red;
	}

	/// <summary>
	/// Getter sur le flux de ressource
	/// </summary>
	/// <returns>Le flux de ressources</returns>
	public int GetTotal()
	{
		return exportTerritoriaSlider.GetSliderValue() - importTerritoriaSlider.GetSliderValue();
	}
	/// <summary>
	/// Getter sur la valeur de l'enfant qui gère l'export
	/// </summary>
	/// <returns>La valeur d'export du slider</returns>
	public int GetExportValue(){
		return exportTerritoriaSlider.GetSliderValue();
	}
	/// <summary>
	/// Getter sur la valeur de l'enfant qui gère l'import
	/// </summary>
	/// <returns>La valeur d'import du slider</returns>
	public int GetImportValue(){
		return importTerritoriaSlider.GetSliderValue();
	}
	/// <summary>
	/// Setter sur la valeur maximal des sliders
	/// </summary>
	/// <param name="max">Le nouveau max des sliders</param>
	public void SetExportMax(int max)
	{
		if (max>exportTerritoriaSlider.GetMaxSliderValue())
		{
			exportTerritoriaSlider.UpdateSliderMax(max);
			importTerritoriaSlider.UpdateSliderMax(max);	
		}
	}

	/// <summary>
	/// Setter sur les taux de change
	/// </summary>
	/// <param name="exchangeRate">Les nouveaux taux de change</param>
	public void SetExchangeRate(int[] exchangeRate)
	{
		this.exchangeRate[0] = exchangeRate[0];
		this.exchangeRate[1] = exchangeRate[1];
	}
	[Signal] public delegate void TotalChangedEventHandler(int total);
}
