namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;

    /*
    private Village ancienVillage;

    g gardé l'ancienne version en commentaire au cas ou
    
    public void determineStrategy()
    {
        village.setBuildingStrategy(new BuildingGrowthStrategy(village.getTiles()));

        Dictionary<ResourceType, int> availableResources = village.getAvailableResources(); // je me rend compte qu'il fallait peut etre pas utiliser getAvailableRessources, mais plutot les ressources collecté par le joueur
        Dictionary<ResourceType, int> ancienAvailableResources = ancienVillage.getAvailableResources();

        foreach(var (type,value) in availableResources) // plutot que de se baser sur les ressources qu'on a, extrapoler sur la production de ce tour et la demande actuelle
        {
            if(value > ancienAvailableResources[type])  // mettre un flat cap aussi avec un ET
            {
                // on a plus de cette ressources qu'avant -> stratégie de préservation / destruction si surplus ( + de X quantité)
            }
            else
            {
                // on essaye d'ameliorer notre production de cette resource / on panique et brule des maisons
            }

            nbPlaceables = village.getNbPlaceables();

            foreach(var (type,value) in nbPlaceables)   // peut etre verifier que certain types plutot que tous, permet de faire des tests customs sur chacun
            {
                if(value > ) // on a deja trop de ce batiement 
                {

                }
                else    // le nombre est correct / inssuffisant
                {

                }
            }

            // checker le nombre total de batiment pour savoir si il faut raser des trucs
        }
    }*/

    public void determineStrategy() // les conditions de passage sont des sugestions
    {
        BuildingStrategyFactory factory;

        Dictionary<Placeable,int> nbPlaceables =  village.getNbPlaceables()

        if(nbPlaceables[PlaceableType.SAWMILL]  == 0 and nbPlaceables[PlaceableType.FIELD] == 0 and nbPlaceables[PlaceableType.ICE_USINE]  == 0)    // passe au tertiaire quand : plus aucune scierie, usine a glacon et champ
        {
            village.setBuildingStrategy(factory.createTertiaryStrategy());
        }
        else if()   // passe au secondaire quand : le joueur a fait 2 tour de jeu
        {
            village.setBuildingStrategy(factory.createSecondaryStrategy());
        }
        else
        {
            village.setBuildingStrategy(factory.createPrimaryStrategy());
        }
    }

    public void determineNiveauVillage(Dictionary<Placeable,int> nbPlaceables, ressources ) // fonction independante de l'avancement du joueurs, qui permet de check si on doit changer de strategie global : primaire, secondaire, tertiaire
    {
        // executer cette fonction seulement si le joueur s'en sort bien, ne pas faire changer de phase à un joueur en difficulté

        // mettre en place un menu pour informer le joueur du changement de tour 

        // si on a X champ : on passe a la territorialisation phase export : on debloque un nouveau systeme de transport : et on previent le joueur qu'on devrait vendre notre surplus de biere a l"international

        // 
    }
}  