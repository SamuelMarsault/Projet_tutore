using System.Collections.Generic;
using Godot;
using TerritoriaV1;

public partial class Building : Placeable
{
    private Vector2 position;
    //Défini les types et la quantité de resssources nécéssaire pour produire n output
    private Godot.Collections.Dictionary<ResourceType, int> input; 
    //Défini les types et la quantité de resssources produite
    private Godot.Collections.Dictionary<ResourceType, int> output;
    //Représente la capacité de production : output * capacite = quantité totale
    private int productionCapacities;
    
    public Building(Godot.Collections.Dictionary<ResourceType, int> input,Godot.Collections.Dictionary<ResourceType, int> output, int productionCapacities)
    {
        this.input = input.Duplicate(true);
        this.output = output.Duplicate(true);
        this.productionCapacities = productionCapacities;
    }
    
    //Produit des ressources en fonction des besoins et ressources disponibles
    public override void productResources(Godot.Collections.Dictionary<ResourceType, int> availableResources, Godot.Collections.Dictionary<ResourceType, int> neededResources)
    {
        int ?min = null;
        foreach (var (type,value) in input)
        {
            if (!availableResources.ContainsKey(type))
            {
                return ;
            }
            if (!min.HasValue || availableResources[type]/value<min.Value)
            {
                min = availableResources[type]/value;
            }
        }
        
        if (min>productionCapacities)
        {
            min = productionCapacities;
        }

        foreach (var (type,value) in input)
        {
            int usedResources = min.Value * value;
            availableResources[type] -= usedResources;
            neededResources[type] += usedResources;
        }
    }

    //Renvoi la quantité de ressources dont l'objet à besoin
    override 
    public Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds()
    {
        return input.Duplicate(true);
    }

    //Renvoi la quantité de ressources disponible dans l'objet, ici vide parce que le bâtiment ne stocke rien
    override 
        public Godot.Collections.Dictionary<ResourceType, int> getAvailableResources()
    {
        return new Godot.Collections.Dictionary<ResourceType, int>();
    }
}
