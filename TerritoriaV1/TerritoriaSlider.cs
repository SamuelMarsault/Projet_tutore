using Godot;
using System;
/// <summary>
/// gere les sliders du trader
/// </summary>
public partial class TerritoriaSlider : Control
{
	private Slider slider;

	[Export] private String actionString;
	private Label valueLabel;

	private int MaxStartValue;
	
	public override void _Ready()
	{
		slider = FindChild("ValueSlider", true, true) as Slider;
		Label actionLabel = FindChild("ActionLabel", true, true) as Label;
		actionLabel.Text = actionString;
		valueLabel = FindChild("ValueLabel", true, true) as Label;
		MaxStartValue = (int)slider.MaxValue;
	}

	public override void _Process(double delta)
	{

	}

/// <summary>
/// met a jour le max du slider
/// </summary>
/// <param name="value">la nouvelle valeur</param>
	public void UpdateSliderMax(int value)
	{
		slider.MaxValue = value;
	}
/// <summary>
/// gette la valeur actuelle du slider
/// </summary>
/// <returns>la valeur</returns>
	public int GetSliderValue()
	{
		return (int)slider.Value;
	}

/// <summary>
/// renvoie la valeur max du slider
/// </summary>
/// <returns>la valeur max</returns>
	public int GetMaxSliderValue(){
		return MaxStartValue;
	}
/// <summary>
/// ce qui se passe quand la valeur du slider change
/// </summary>
/// <param name="value">la nouvelle valeur du slider</param>	
	private void OnValueSliderValueChanged(double value)
	{
		
		valueLabel.Text = value.ToString();
		EmitSignal(SignalName.ValueChanged);
	}
	[Signal] public delegate void ValueChangedEventHandler();
}

