using Godot;
using System;

public partial class ResourcePrintUnit : Control
{
	private Label valueLabel;
	[Export] private Texture2D icon;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		valueLabel = this.FindChild("ResourceLabel") as Label;
		valueLabel.Text = 0.ToString();
		TextureRect textureRect = this.FindChild("ResourceTexture") as TextureRect;
		textureRect.Texture = icon;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
	
	public void SetValue(int value){
		valueLabel.Text = value.ToString();
	}
	
}
