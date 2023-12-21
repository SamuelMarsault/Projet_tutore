namespace TerritoriaV1;
/// <summary>
/// les observeurs du village
/// </summary>
public interface VillageObserver
{
    /// <summary>
    /// ce qui se passe quand les ressources du village change
    /// </summary>
    /// <param name="resources">les nouvelles ressources</param>
    public void ReactToResourcesChange(int[] resources);
    /// <summary>
    /// ce qui se passe quand les taux de change, changent
    /// </summary>
    /// <param name="exchangesRates">les nouveaux de taux de changes</param>
    public void ReactToExchangesRatesChange(int[,] exchangesRates);
    /// <summary>
    /// les placeables ( batiments) ont changé
    /// </summary>
    /// <param name="placeables">les nouveaux placeables</param>
    public void ReactToPlaceableChange(Placeable[,] placeables);
    /// <summary>
    /// les tiles ont changés
    /// </summary>
    /// <param name="tiles">le nouveau tableau de tiles</param>
    public void ReactToTilesChange(TileType[,] tiles);
/// <summary>
/// ce qui se passe quand le joueur veut faire une transaction impossible
/// </summary>
/// <param name="missingRessources"></param>
    public void ReactToImpossibleTransaction(int[] missingRessources);
}
