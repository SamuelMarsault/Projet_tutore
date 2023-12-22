## HAN23-T3-A : Territorialisation

### Le jeu du siècle

#### Sujet

Notre sujet était `Re-territorialiser par la matière. Approche du métabolisme urbain à l'échelle d'un quartier. Koenigshoffen-Est à Strasbourg TW : Géographie`.
Ce sujet avait pour but de faire comprendre le principe de métabolisme urbain à travers ces différentes phases que sont la territorialisation la déterritorialisation, la tertiarisation et enfin la reterritorialisation.

#### Notre jeu

Notre jeu est un jeu de gestion dans lequel nous devons gérer les flux de ressources entrants et sortants afin que notre quartier évolue au mieux. Au fil des tours, le quartier verra des infrastructures se créer selon ses ressources disponibles ainsi que la phase de métabolisme urbain dans lequel il se trouve.
Nous pouvons voir sur ces captures d'écran issues du jeu l'évolution du quartier d'un tour à l'autre, ou du moins un exemple d'évolution.
![Capture d'écran 1](screens/ScreenInGame1.png "Quartier au tour 10")
![Capture d'écran 2](screens/ScreenInGame2.png "Quartier au tour 11")

### Autres fichiers : 

#### Cahier des charges : 

https://docs.google.com/document/d/1Y5vt4j5I7eVvQMkLXf5Q6LSS41f13dUV9iAZQtb-zHs/edit?usp=sharing

#### Wiki du jeu : 

[territoria wiki](wikiDescription.md)

### Installations :

#### Pour jouer au jeu : 

##### Sur Windows : 

1. telecharger et deziper [territoriaWindows]((download/Windows/territoriaWindows.zip)
2. cliquer sur 'TerritoriaV1.exe'

##### Sur Linux (testé sur debian et linux MINT) :

installer [territoria_installer_linux](download/Linux/territoria_installer_linux.run)

2. cliquer dessus, et choisir l'option `lancer dans un terminal`. si l'option n'apparait pas, essayer de faire un clique droit sur le fichier et selectionner 'lancer dans un terminal'. additionnellement vous pouvez taper `./territoria_installer_linux.run` dans le dossier du fichier depuis votre terminal.

3. si pour une raison ou une autre, rien ne cela ne marche, telecharger et deziper [territoriaLinux](download/Linux/territoriaLinux.zip)
et cliquer sur `TerritoriaV1.x86_64`.
	
##### Sur MacOS : 

#### Pour éditer le projet : 

1. Faire un fork de ce dépôt (Bouton `Forks` en haut a droite)

2. Créer un clone en local de votre dépôt git (Bouton `Clone` en haut à droite -> Copier l'URL pour `Clone with SSH`)
dans un repertoire git en faisant `git clone <ssh_url>`

3. Installer Godot 4.1.1 : https://godotengine.org/download/archive/4.1.1-stable

4. Utiliser votre editeur de texte préféré qui prend en charge le C#. Une extension pour la syntaxe Godot CS est recommandée.
(Godot prend en charge l'ouverture automatique des fichiers .cs si vous le configurez :
`Editeur` -> `Paramètres de l'éditeur` -> `Dotnet` -> `Editeur` (Sélectionner votre editeur et le lien vers son exécutable))

5. Ouvrir godot, cliquer sur `Importer` et chercher puis ouvrir le fichier `project.godot` dans votre arborescence. 
Godot vous demandera peut-être un mode de rendu, choisissez `compatibility`.

6. Modifier le projet pour votre usage personnel à votre guise.

### acceder à la documentation : 

nous avons un systeme de documentation automatique xml :

[documentation]( TerritoriaV1/bin/Territoria.XML)
