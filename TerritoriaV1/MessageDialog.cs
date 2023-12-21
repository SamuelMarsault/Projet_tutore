using Godot;

public partial class MessageDialog : AcceptDialog
{

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetErrorMessage(string errorMessage, bool end)
	{
		DialogText = errorMessage;
	}
}
