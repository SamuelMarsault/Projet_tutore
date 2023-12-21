using Godot;
public partial class end_screen : Panel
{
	public void setText(string text, Color color)
	{
		Label label = (Label)GetNode("Label");
		label.Text = text;
		label.LabelSettings.FontColor = color;
	}

	public void _on_button_pressed()
	{
		GetTree().ReloadCurrentScene();
	}
}
