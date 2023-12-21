using Godot;
using System;

public partial class ResourcePrintUnit : Control
{
	private Label valueLabel;
	[Export] private Texture2D icon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		valueLabel = this.FindChild("ResourceLabel", true, true) as Label;
		valueLabel.Text = 500.ToString();
		TextureRect textureRect = this.FindChild("ResourceTexture", true, true) as TextureRect;
		textureRect.Texture = icon;

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void SetValue(int value){
		valueLabel.Text = value.ToString();
	}

	[Signal] public delegate void TotalChangedEventHandler(int total);

	public void SetNewRessources(int newValue){
		valueLabel.Text = newValue.ToString();
	}

	public int GetRessources(){
		return int.Parse(valueLabel.Text);
	}
	
}
