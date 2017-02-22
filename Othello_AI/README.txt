Chaque groupe fournira une librairie de classe  (au plus tard le 5 mars, de préférence un peu avant)

OTHELLOX.DLL
avec une classe "...Board..." implémentant l'interface IPlayable définie dans un assemblage commun (IPlayable.dll). Pour une intégration dynamique de votre IA, la classe doit posséder un construteur par défaut.
Note: il faut que votre projet référence la DLL existante (l'assembly). Ne pas redéclarer l'interface (retaper le code, la recompiler, la modifier, ...)!


La solution LibrairieOthelloTest contient l'application "ConsoleTestDLLOthello" qui permet de charger dynamiquement deux classes IAs et de les faire jouer l'une contre l'autre. Le programme demande à tour de rôle à chaque IA 

- son prochain coup (GetNextMove)
- vérifie la validité du coup
- joue le coup sur un jeu de référence
- fait jouer à chaque IA le coup sur son board (en lui passant l'état du jeu de référence)
- demande l'état du board à chaque IA
- vérifie que l'état des boards des IAs est correct.

jusqu'à ce que les deux IAs doivent passer (fin de partie)

Les DLL et codes sources sont disponibles dans l'archive DOTNET_OTHELLOv_1.02.zip


OHU


