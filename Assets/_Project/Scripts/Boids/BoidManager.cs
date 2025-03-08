using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Model;
using _Project.Scripts.Model.Core;
using _Project.Scripts.Model.Field;
using _Project.Scripts.Model.StarSystem;

namespace _Project.Scripts.Boids
{
    public class BoidManager : IDisposable
    {
        // TODO: удалять агентов при деспавне
        private List<Star> _allStars;
        private Dictionary<Team, List<Ship>> _matchTeammates;
        private FieldGenerator _fieldGenerator;
        
        public BoidManager(FieldGenerator fieldGenerator)
        {
            _fieldGenerator = fieldGenerator;
            
            _fieldGenerator.StarsSpawned += OnStarsSpawned;
        }

        private void OnStarsSpawned(List<Star> stars)
        {
            _allStars = stars;
            _allStars.ForEach(star => star.ShipSpawned += OnShipSpawned);
            _matchTeammates = new Dictionary<Team, List<Ship>>()
            {
                { Team.Neutral, new List<Ship>() },
                { Team.Red, new List<Ship>() },
                { Team.Green, new List<Ship>() },
                { Team.Blue, new List<Ship>() },
                { Team.Yellow, new List<Ship>() },
            };
        }

        public void Dispose()
        {
            _allStars.ForEach(star => star.ShipSpawned -= OnShipSpawned);
            _fieldGenerator.StarsSpawned += OnStarsSpawned;
        }
        
        public List<Ship> GetTeammates(Team team)
        {
            return _matchTeammates[team];
        }
        
        private void OnShipSpawned(Ship ship)
        {
            _matchTeammates[ship.Team].Add(ship);
        }
    }
}
