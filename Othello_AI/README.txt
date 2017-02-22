Chaque groupe fournira une librairie de classe  (au plus tard le 5 mars, de pr�f�rence un peu avant)

OTHELLOX.DLL
avec une classe "...Board..." impl�mentant l'interface IPlayable d�finie dans un assemblage commun (IPlayable.dll). Pour une int�gration dynamique de votre IA, la classe doit poss�der un construteur par d�faut.
Note: il faut que votre projet r�f�rence la DLL existante (l'assembly). Ne pas red�clarer l'interface (retaper le code, la recompiler, la modifier, ...)!


La solution LibrairieOthelloTest contient l'application "ConsoleTestDLLOthello" qui permet de charger dynamiquement deux classes IAs et de les faire jouer l'une contre l'autre. Le programme demande � tour de r�le � chaque IA 

- son prochain coup (GetNextMove)
- v�rifie la validit� du coup
- joue le coup sur un jeu de r�f�rence
- fait jouer � chaque IA le coup sur son board (en lui passant l'�tat du jeu de r�f�rence)
- demande l'�tat du board � chaque IA
- v�rifie que l'�tat des boards des IAs est correct.

jusqu'� ce que les deux IAs doivent passer (fin de partie)

Les DLL et codes sources sont disponibles dans l'archive DOTNET_OTHELLOv_1.02.zip


OHU


