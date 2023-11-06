using Godot;
using System;
using System.Collections.Generic;

public partial class ressourcesDictionnary: Node
{
	public static Dictionary<RESSOURCES, int> ressourcesQuantity = new Dictionary<RESSOURCES, int>();
	public static Dictionary<RESSOURCES, int> ressourcesTradCost = new Dictionary<RESSOURCES, int>();

	public override void _Ready()
	{
		
	}
}
