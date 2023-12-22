## HAN23-T3-A : Territorialisation

### Le jeu du siècle

#### Sujet

Notre sujet était `Re-territorialiser par la matière. Approche du métabolisme urbain à l'échelle d'un quartier. Koenigshoffen-Est à Strasbourg TW : Géographie`.
Ce sujet avait pour but de faire comprendre le principe de métabolisme urbain à travers ces différentes phases que sont la territorialisation la déterritorialisation, la tertiarisation et enfin la reterritorialisation.

#### Notre jeu

Notre jeu est un jeu de gestion dans lequel nous devons gérer les flux de ressources entrants et sortants afin que notre quartier évolue au mieux. Au fil des tours, le quartier verra des infrastructures se créer selon ses ressources disponibles ainsi que la phase de métabolisme urbain dans lequel il se situe.
Nous pouvons voir sur ces captures d'écran issues du jeu l'évolution du quartier d'un tour à l'autre, ou du moins un exemple d'évolution.
![Capture d'écran 1](screens/ScreenInGame1.png "Quartier au tour 10")
![Capture d'écran 2](screens/ScreenInGame2.png "Quartier au tour 11")

### Autres fichiers :

#### Cahier des charges :

https://docs.google.com/document/d/1Y5vt4j5I7eVvQMkLXf5Q6LSS41f13dUV9iAZQtb-zHs/edit?usp=sharing

#### Wiki du jeu :

[Territoria Wiki](wikiDescription.md)

### Installations :

#### Pour jouer au jeu :

##### Sur Windows :

1. Télécharger et dézipper [TerritoriaWindows](download/Windows/territoriaWindows.zip)

2. Cliquer sur 'TerritoriaV1.exe'

##### Sur Linux (testé sur debian et linux MINT) :

1. Installer [Territoria_installer_linux](download/Linux/territoria_installer_linux.run)

2. Cliquer dessus, et choisir l'option `Lancer dans un terminal`. Si l'option n'apparait pas, essayer de faire un clic droit sur le fichier et sélectionner `Lancer dans un terminal`. Additionnellement, vous pouvez taper `./territoria_installer_linux.run` dans le dossier du fichier depuis votre terminal.

3. Si pour une raison ou une autre rien de cela ne marche, télécharger et dézipper [TerritoriaLinux](download/Linux/territoriaLinux.zip)
et cliquer sur `TerritoriaV1.x86_64`.

##### Sur MacOS :

1. Télécharger le dossier [TerritoriaMacOS](download/MacOS/TerritoriaMacOS.zip) et le décompresser.

2. Ouvrir l'exécutable `Territoria.app/Contents/MacOS/Territoria` (il est normal que ça ne fonctionne pas)

3. Aller dans `Préférences système` > `Confidentialité et sécurité`. Dans la catégorie `Sécurité`, cliquer sur `Ouvrir quand même` à côté du nom de l'application.

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

### Accéder à la documentation :

Nous avons un système de documentation automatique XML :
[Documentation](TerritoriaV1/bin/Territoria.XML)

## Crédits

### Dans l'Odyssée Créative de notre Projet

Nous souhaitons exprimer notre gratitude envers deux individus exceptionnels qui ont été les Gardiens de l'Excellence, apportant leur touche unique à notre aventure :

#### Pilou
#### Maestro des Mélodies

    Pilou, la créatrice de magie sonore, a donné une âme au jeu en tissant des notes en harmonie, créant ainsi une symphonie qui résonne dans le coeur de chaque joueur.

#### Juju
#### Artisan des Visuels

    Juju, l'architecte visuel, a sculpté le monde du jeu avec des sprites inspirants, transformant des idées abstraites en une réalité visuelle captivante.

Ensemble, ils ont transcendé les frontières de l'ordinaire, élevant notre projet vers de nouveaux sommets. Les Gardiens de l'Excellence, Pilou et Juju, ont laissé une trace indélébile sur ce projet.