# Madbox-Test

● How you approached this test: what were your different game making phases?

-> J'ai commencé par implémenter les contrôles au joystick, puis j'ai mis en place la structure nécessaire pour pouvoir porter une arme et en switcher. Ensuite, j'ai réfléchi à comment pouvoir sélectionner des armes facilement et j'ai mis en place un système de loadout, dans lequel on peut drag/drop les armes de l'inventaire vers le loadout. Puis j'ai implémenté les attaques automatiques dès que le joueur ne se déplace pas, le spawn des ennemis, et la recherche de targets. Ensuite, j'ai rajouté quelques effets (particules, animations). Puis j'ai crée une structure de niveaux et un écran de transition entre niveaux. J'ai terminé en ajoutant une seconde animation d'attaque (trouvée sur Mixamo), des coffres à ouvrir, de nouveaux ennemis, de nouvelles armes, et du loot qui tombe des ennemis.

● The time you spent on each phase of the exercise

-> J'ai passé en tout une vingtaine d'heures sur ce test. Je dirais qu'un tiers a été passé sur les contrôles, les attaques et la recherche de targets, un tiers sur le loadout et l'UI en général, et un tiers sur le polish et les quelques choses bonus

● The features that were difficult for you and why

-> Pour le spawn des ennemis, j'ai commencé par faire spawner à des endroits aléatoires de la map, mais j'avais du mal à gérer le fait que les ennemis ne doivent pas être trop proches les uns des autres. J'ai fini par implémenter une solution semi-aléatoire, avec une quinzaine de points de spawns définis par niveau. Les ennemis choisissent des points aléatoires parmi ces points-là, ce qui garanti qu'ils ne se superposent pas.

● The features you think you could do better and how

-> J'ai essayé de garder une architecture propre et modulaire mais on peut toujours pousser les choses encore plus loin dans l'abstraction. J'aurais pu améliorer le côté juicy du jeu, il est assez limité pour l'instant. La transition entre les niveaux est juste un écran basique, et le jeu n'a pas vraiment de clair objectif à part tuer les ennemis. J'aurais aussi pu varier un peu plus le visuel des niveaux et rajouter des props.

● What you would do if you could go a step further on this game

-> De nouveaux ennemis qui peuvent attaquer le joueur, des attaques à distance, des objets à collecter (armes, armures...), et un shop pour utiliser le gold récupéré en combat. On peut aussi imaginer un système pour upgrade/vendre son équipement.


● Any comment you may have
Merci pour ce test, c'était fun !
