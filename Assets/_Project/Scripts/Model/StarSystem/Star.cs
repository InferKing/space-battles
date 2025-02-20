using System.Collections.Generic;
using _Project.Scripts.Model;
using _Project.Scripts.Model.Core;
using UnityEngine;
using Zenject;

public class Star : MonoBehaviour
{
    public Team Team { get; set; }
    public List<Star> ConnectedStars { get; private set; } = new List<Star>();
    public List<StarConnection> Connections { get; private set; } = new List<StarConnection>();

    private Ship.Factory _shipFactory;
    
    private void Start()
    {
        var sr = GetComponent<SpriteRenderer>();
        
        if (sr != null)
        {
            sr.color = Team switch
            {
                Team.Red => Color.red,
                Team.Green => Color.green,
                Team.Blue => Color.blue,
                Team.Yellow => Color.yellow,
                _ => Color.white
            };
        }
    }

    [Inject]
    private void Construct(Ship.Factory shipFactory)
    {
        _shipFactory = shipFactory;
    }
    
    public class Factory : PlaceholderFactory<Star> {}
}