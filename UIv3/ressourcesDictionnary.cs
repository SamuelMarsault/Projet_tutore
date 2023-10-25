using Godot;
using System;
using System.Collections.Generic;

public partial class ressourcesDictionnary: Node
{
	public static Dictionary<RESSOURCES, int> ressourcesQuantity = new Dictionary<RESSOURCES, int>();

	public override void _Ready()
	{
		
	}

	public static void LoadDictionnary()
	{
		foreach(RESSOURCES resource in Enum.GetValues(typeof(RESSOURCES)))
    	{
        	ressourcesQuantity.Add(resource, 0);
    	}
	}
}
