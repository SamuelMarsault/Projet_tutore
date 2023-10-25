using Godot;
using System;

public partial class printer : Control,gameObserver
{
	Label wood;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//wood = (Label) this.GetNode("WoodVIew").GetNode("Background").GetNode("Value");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//wood.Text = "hello";
	}

	 public void reactToRessourcesUpdate(RESSOURCES ressource, int newValue)
	 {
		switch (ressource)
		{
			case RESSOURCES.WOOD : Label tempW =  (Label)GetNode("./MarginContainer/HBoxContainer/WoodView/Background/Value");tempW.Text = newValue.ToString(); break;
    		case RESSOURCES.BEER : Label tempB =  (Label)GetNode("./MarginContainer/HBoxContainer/BeerView/Background/Value");tempB.Text = newValue.ToString(); break;
    		case RESSOURCES.HOP : Label tempH =  (Label)GetNode("./MarginContainer/HBoxContainer/HopView/Background/Value");tempH.Text = newValue.ToString(); break;
    		case RESSOURCES.ICE : Label tempI =  (Label)GetNode("./MarginContainer/HBoxContainer/IceView/Background/Value");tempI.Text = newValue.ToString(); break;
    		case RESSOURCES.MONEY : Label tempM =  (Label)GetNode("./MarginContainer/HBoxContainer/MoneyView/Background/Value");tempM.Text = newValue.ToString(); break;
			default: break;
		}
	}
}
