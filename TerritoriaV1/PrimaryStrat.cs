using Godot;
using System;

public partial class PrimaryStrat : BuildingStrategy
{
      // recuperer la ressource stocké par le joueur + la ressource qui a été produit se tout ci ( ou alors ca le collecte deja avant l'execution de cette fonction donc c'est bon ?)

        Village village; // recuperer se gars à un moment

    public override void executeStrategie()
    {
        Godot.Collections.Dictionary<ResourceType, int> availablesRessources = village.getResources();
        Godot.Collections.Dictionary<ResourceType, int> neededRessources = village.publicGetNeededRessources();

        //je me rend compte que c'est pseudo-recursif vu que si on as besoin de bois c'est qu'on veut construire un autre truc car on a besoin d'un autre mat
        if(availablesRessources[ResourceType.WOOD] > neededRessources[ResourceType.WOOD])
        {
            // on a besoin de plus de bois pour le prochain tour

            // construire scierie
        }
        
        if(availablesRessources[ResourceType.HOP] > neededRessources[ResourceType.HOP])
        {
            // construire un champ
        }

        if(availablesRessources[ResourceType.ICE] > neededRessources[ResourceType.ICE])
        {
            // construire une usine a glacon
        }

        if(availablesRessources[ResourceType.BEER] > neededRessources[ResourceType.BEER])
        {
            // techniquement il se passe rien vu que le jeu doit automatiquement convertir la glace et le houblon en bière à la fin du tour non ?
            // est ce que ca prend toute les ressources, un ratio ? est ce qu'on veut en construire plus pour plus de production ou une seul toute la parti ??
        }
        // meme question que plus haut pour le bar

        // faudrait que les methodes qui construisent renvoie un boolean si le batiment a bien ete construit ou si pas assez de ressource pour adpater l'algo non ?
        // genre tu veux de la glace, mais pas assez de bois pour faire une usine, donc poof tu rajoute du bois au besoin ? ou bien ca le prend deja en compte dans 
        // la fonction qui fait le tableau neededRessources ?

        // tant de question sans réponse
    }

    
}
