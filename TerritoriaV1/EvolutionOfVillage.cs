namespace TerritoriaV1;
/// <summary>
/// permet de choisir quel strategies de développement le village va appliquer
/// </summary>
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

    public EvolutionOfVillage(GameManager gm)
    {
        this.gameManager = gm;
    }

/// <summary>
/// permet de determiner la strategie qu'utilisera le village, parmi la premiere, la deuxieme et la troisième ( cf leurs doc respectives)
/// </summary>
    public void DetermineStrategy(int turn) 
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
                    placeable.setProductionCapacity(50);
                    nbHouse -= 10;
                }
                else
                {
                    placeable.setProductionCapacity(nbHouse*5);
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
		    gameManager.setMessage("Le village a atteint une phase de tertiarisation : il se délaisse de la production et compte sur l'import pour satisfaire la consommation.");
        }
        else if(turn > 8 && alreadyTertiary == false && alreadySecondary == false)
        {
            alreadySecondary = true;
            village.SetBuildingStrategy(factory.createSecondaryStrategy(village.GetPlaceables(),village.GetTiles()));
		    gameManager.setMessage("Le village a atteint une phase de deterritorialisation: des habitants viennent y vivre, et certaines usines de ressources primaires commencent à fermer, au profit de l'import des ressources nécéssaires à son développement.");
	
        }
        else if(alreadyTertiary == false && alreadySecondary == false)
        {
            village.SetBuildingStrategy(factory.createPrimaryStrategy(village.GetPlaceables(),village.GetTiles()));
            // Garde la stratégie primaire actuelle, pas besoin de la rechanger
        }
        else
        {

        }
    }
/// <summary>
/// simple setter pour l'attribut village
/// </summary>
/// <param name="village">le village en question</param>
    public void SetVillage(Village village) 
    {
        this.village = village;
    }
}