using Godot;
using Godot.Collections;

namespace TerritoriaV1;

public class EvolutionOfVillage
{
    private Village village;

    private int[] ressources;
    private int[] neededRessources;    
    private int[] NBPlaceables;
    private BuildingStrategyFactory factory = new BuildingStrategyFactory();
    private GameManager gameManager;

    bool alreadySecondary = false;
    bool alreadyTertiary = false;

    int turn = 1;

    public EvolutionOfVillage(GameManager gm)
    {
        this.gameManager = gm;
    }

    public void DetermineStrategy() 
    {
        ressources = village.GetResources();
        neededRessources = village.GetNeededRessourcesPublic();
        this.NBPlaceables = village.getNBPlaceables();
        int nbHouse = NBPlaceables[PlaceableType.HOUSE.GetHashCode()];
        
        foreach (Placeable placeable in village.GetPlaceables())
        {
            if (placeable!=null && placeable.getPlaceableType() == PlaceableType.BAR)
            {
                if (nbHouse > 10)
                {
                    placeable.setProductionCapacity(100);
                    nbHouse -= 10;
                }
                else
                {
                    placeable.setProductionCapacity(nbHouse*10);
                    nbHouse = 0;
                }
            }
        }
        
        if(this.NBPlaceables[(int)PlaceableType.SAWMILL] == 0 && 
        this.NBPlaceables[(int)PlaceableType.FIELD] == 0 &&
        NBPlaceables[(int)PlaceableType.ICE_USINE]  == 0
        && alreadySecondary == true && alreadyTertiary == false
        && NBPlaceables[(int)PlaceableType.HOUSE]>20) 
        {
            alreadyTertiary = true;

            village.SetBuildingStrategy(factory.createTertiaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation");
        }
        else if(turn > 8 && alreadyTertiary == false && alreadySecondary == false)
        {
            alreadySecondary = true;

            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.printMessage("Le village a atteint une phase de deterritorialisation: des habitants viennent y vivrent, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaire à son dévellopement");
	
        }
        else if(alreadyTertiary == false && alreadySecondary == false)
        {
            village.SetBuildingStrategy(factory.createPrimaryStrategy(village.GetPlaceables(),village.GetTiles()));
            // garde la stratégie primaire actuelle, pas besoin de la rechanger
        }
        else
        {
            //GD.Print("on ne devrais pas être ici");
        }
        turn++;
    }

    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}
