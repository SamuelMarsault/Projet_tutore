using Godot;
using System;

public partial class TerritoriaSlider : Control
{
	private Slider slider;

	[Export] private String actionString;
	private Label valueLabel;

	private int MaxStartValue;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		slider = FindChild("ValueSlider", true, true) as Slider;
		Label actionLabel = FindChild("ActionLabel", true, true) as Label;
		actionLabel.Text = actionString;
		valueLabel = FindChild("ValueLabel", true, true) as Label;
		MaxStartValue = (int)slider.MaxValue;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void UpdateSliderMax(int value)
	{
		GD.Print("##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n##########\n");
		slider.MaxValue = value;
		int scale = 0;
		while (value > 100)
		{
			scale++;
			value /= 10;
		}

		slider.Step = 1;
		for (int i = 0; i < scale; i++)
			slider.Step *= 10;
		GD.Print(slider.MaxValue+" "+slider.Step);
		this.ForceUpdateTransform();
	}

	public int GetSliderValue()
	{
		return (int)slider.Value;
	}

	public int GetMaxSliderValue(){
		return (int)slider.MaxValue;
	}
	
	private void OnValueSliderValueChanged(double value)
	{
		
		valueLabel.Text = value.ToString();
		EmitSignal(SignalName.ValueChanged);
	}
	[Signal] public delegate void ValueChangedEventHandler();
}

