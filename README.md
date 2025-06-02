# 2025_AR_Thomas

# Dragon Thermique AR – Jeu en Réalité Augmentée

## Description du projet

**Objectif** : Développer un jeu interactif en réalité augmentée où une mascotte virtuelle (un serpent thermique) évolue dans l’espace réel en réagissant à des sources de chaleur ou de froid. L’utilisateur doit guider le serpent vers des points chauds pour le faire grandir, tout en évitant qu’il ne rétrécisse au contact de sources froides ou à cause du refroidissement ambiant.

Ce projet a été réalisé undividuellement dans le cadre d’un module sur la réalité augmentée à l’ENSIM. Il combine technologie AR, détection d’images, mécanique de jeu en temps réel et interface utilisateur.

---

##  Fonctionnalités principales

- **Détection d’images en réalité augmentée** : Utilisation de marqueurs (feu, radiateur, flocon, fenêtre) reconnus par la caméra via AR Foundation.
- **Température dynamique** : Chaque marqueur influence la température interne du serpent :
  -  Feu / Radiateur : +20°C  
  - Flocon / Fenêtre : -15°C
- **Gameplay thermique** :
  - Température évoluant entre -10°C et +80°C
  - Taille du serpent proportionnelle à sa température (0.3 à 2.0)
  - Refroidissement ambiant : -1°C par seconde
  - Durée du jeu : 90 secondes
- **Conditions de fin** :
  -  Victoire : taille ≥ 1.8
  -  Défaite : taille ≤ 0.3 ou temps écoulé
- **Interface utilisateur (UI)** :
  - Affichage en temps réel de la température, la taille du serpent et le temps restant

---

##  Technologies utilisées

- **Unity 2022.3.3 LTS** : Moteur de développement du jeu
- **AR Foundation 5.1** : Framework Unity pour la réalité augmentée multiplateforme
- **C#** : Langage de script pour la logique du jeu (DragonGameManager & DragonController)
- **Android** : Plateforme cible pour le déploiement mobile
- **Images cibles (ARTrackedImageManager)** : Pour la reconnaissance des marqueurs physiques

---

##  Problèmes rencontrés

- Difficulté à lier le JDK sur mon ordinateur personnel → développement uniquement sur les machines de l’école
- Détection des images fonctionnelle, mais déplacement du serpent non opérationnel
- Limitations dans les tests des interactions thermiques à cause du déplacement inactif
- Problèmes de reconnaissance des marqueurs en conditions de faible luminosité

---

##  Améliorations futures

- Activer le déplacement autonome du serpent
- Ajouter des modèles 3D et animations (remplacer les formes primitives)
- Ajouter des effets visuels (particules, sons) pour feu et glace
- Ajouter plusieurs niveaux, un système de score, ou différentes créatures thermiques
- Rendre le jeu accessible à différents environnements de luminosité

---

##  Installation

### Prérequis
- Unity 2022.3.3 LTS
- AR Foundation 5.1
- Un smartphone Android avec ARCore supporté
- Un environnement lumineux pour la reconnaissance des marqueurs

### Étapes d’installation

1. Cloner ce dépôt :
   ```bash
   git clone https://github.com/GaiaBathea/2025_AR_Thomas.git
