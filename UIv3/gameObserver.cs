using System;

public partial interface gameObserver
{
    public void reactToRessourcesUpdate(RESSOURCES ressource, int newValue);
}