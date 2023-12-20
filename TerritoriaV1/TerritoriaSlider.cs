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
		slider.MaxValue = value;
	}

	public int GetSliderValue()
	{
		return (int)slider.Value;
	}

	public int GetMaxSliderValue(){
		return MaxStartValue;
	}
	
	private void OnValueSliderValueChanged(double value)
	{
		
		valueLabel.Text = value.ToString();
		EmitSignal(SignalName.ValueChanged);
	}
	[Signal] public delegate void ValueChangedEventHandler();
}

