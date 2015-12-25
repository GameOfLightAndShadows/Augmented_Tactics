using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Managers
{
    public class GameObjectSpawmer
    {
        public List<CharacterObservable> Humans;
        public List<CharacterObservable> Enemies;
        private AStarPathfinder _pathfinder;
        private HealthManager _healthManager;
        private GameManager _gameManager;
        public GameMap _map;
        public GameObject spawmer;

        public GameMap GenerateGameMap()
        {
            throw new NotImplementedException();
        }

        public List<CharacterObservable> GenerateGameCharacters()
        {
            var gameCharacters = new List<CharacterObservable>();
            gameCharacters
                .AddRange(GenerateHumans());
            gameCharacters
                .AddRange(GenerateEnemies());
            return gameCharacters;
        }

        private List<CharacterObservable> GenerateHumans()
        {
            throw new NotImplementedException();
        }

        private List<CharacterObservable> GenerateEnemies()
        {
            throw new NotImplementedException();
        }
    }
}