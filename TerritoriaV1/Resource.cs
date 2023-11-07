using Godot;
using System;
using Godot.Collections;
using TerritoriaV1;

public partial class Resource : Placeable
{
    private int resourceQuantities;
    private int maxQuantities;
    private ResourceType resourceType;

    public Resource(ResourceType resource, int resourceQuantities)
    {
        maxQuantities = resourceQuantities;
        this.resourceQuantities = resourceQuantities;
        resourceType = resource;
        
    }
    
    override 
    public void productResources(Godot.Collections.Dictionary<ResourceType, int> availableResources, Godot.Collections.Dictionary<ResourceType, int> neededResources)
    {
        //Si on a pas besoin de la resource de cette source
        if (!neededResources.ContainsKey(resourceType))
        {
            //Alors on ne produit rien
            return;   
        }

        //Si on a encore besoin des ressources présentes dans l'objet
        if (neededResources[resourceType]>0)
        {
            //Si on a besoin de moins de ressources que ce qu'on stocke ici
            if (neededResources[resourceType]<=resourceQuantities)
            {
                //On prélève les ressources de l'objet
                resourceQuantities -= neededResources[resourceType];
                //On ajoute ces ressources aux ressources disponibles
                availableResources[resourceType] += neededResources[resourceType];
                //Et on enlève le besoin de ressources
                neededResources[resourceType] = 0;
            }
            else
            {
                //On ajoute la totalité des ressources de l'objet
                availableResources[resourceType] += resourceQuantities;
                //On soustrait au besoin les ressources ajoutées
                neededResources[resourceType] -= resourceQuantities;
                //On actualise les ressoures de l'objet
                resourceQuantities = 0;
            }
        }
        else //Si on utilise pas les ressources alors la source a le temps de se régénérer
        {
            regenerateResources();
        }
    }

    //Une source naturelle peut régéner ses stocks d'elle-même
    public void regenerateResources()
    {
        //Coefficient de régénération des ressources naturelles
        double regenCoeff = 0.10;
        if ((resourceQuantities + maxQuantities * regenCoeff)>=maxQuantities)
        {
            resourceQuantities = maxQuantities;
        }
        else
        {
            resourceQuantities += (int)(maxQuantities * regenCoeff);
        }
    }

    override 
    //Renvoi la quantité de ressources dont l'objet à besoin, puisque c'est une source naturelle elle n'a besoin de rien
    public Godot.Collections.Dictionary<ResourceType, int> getResourceNeeds()
    {
        //Une source naturelle n'a pas besoin de ressources pour ce développer (pour le moment en tout cas)
        return new Godot.Collections.Dictionary<ResourceType, int>();
    }

    //Renvoi la quantité de ressources disponible dans l'objet
    override 
        public Godot.Collections.Dictionary<ResourceType, int> getAvailableResources()
    {
        //Une ressource naturelle dispose d'une certaine quantité de matériaux
        Dictionary<ResourceType, int> availableResources = new Dictionary<ResourceType, int>();
        availableResources.Add(resourceType,resourceQuantities);
        return availableResources;
    }
}
