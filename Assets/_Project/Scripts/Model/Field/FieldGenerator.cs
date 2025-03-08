using System;
using System.Collections.Generic;
using _Project.Scripts.Model.StarSystem;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Model.Field
{
    public class FieldGenerator : MonoBehaviour
    {
        [Header("Настройки соединения"), SerializeField] private Material _dashedLineMaterial;
        [SerializeField] private float _lineWidth = 0.1f;

        public event Action<List<Star>> StarsSpawned; 
        
        private readonly List<Star> _stars = new();
        private readonly List<StarConnection> _starConnections = new();

        private FieldParameters _fieldParameters;
        private Star.Factory _starFactory;

        [Inject]
        private void Construct(Star.Factory starFactory, FieldParameters fieldParameters)
        {
            _starFactory = starFactory;
            _fieldParameters = fieldParameters;
        }
        
        private void Start()
        {
            GenerateTeamStars();
            GenerateOtherStars();
            ConnectStarsGraph();
            
            StarsSpawned?.Invoke(_stars);
        }

        private void GenerateTeamStars()
        {
            Array teams = Enum.GetValues(typeof(Team));
            int teamCount = teams.Length;
            for (int i = 0; i < teamCount; i++)
            {
                if ((Team)teams.GetValue(i) == Team.Neutral) continue; 
                
                float angle = i * Mathf.PI * 2 / teamCount;
                Vector2 pos = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * _fieldParameters.MapConfig.TeamRadius;
                pos += Vector2.zero; // Центр области

                Star star = _starFactory.Create();
                star.Init((Team)teams.GetValue(i));
                star.transform.position = pos;
                star.transform.SetParent(transform);
                _stars.Add(star);
            }
        }

        private void GenerateOtherStars()
        {
            int starsToGenerate = _fieldParameters.MapConfig.Stars - _stars.Count;
            int generated = 0;
            int maxAttempts = starsToGenerate * 100;
            int attempts = 0;

            while (generated < starsToGenerate && attempts < maxAttempts)
            {
                attempts++;

                Vector2 candidate = new Vector2(
                    Random.Range(-_fieldParameters.MapConfig.Size.x / 2, _fieldParameters.MapConfig.Size.x / 2),
                    Random.Range(-_fieldParameters.MapConfig.Size.y / 2, _fieldParameters.MapConfig.Size.y / 2)
                );

                if (IsValidPosition(candidate))
                {
                    Star star = _starFactory.Create();
                    star.Init(Team.Neutral);
                    star.transform.position = candidate;
                    star.transform.SetParent(transform);
                    _stars.Add(star);
                    generated++;
                }
            }
        }

        private bool IsValidPosition(Vector2 candidate)
        {
            foreach (var star in _stars)
            {
                if (Vector2.Distance(candidate, star.transform.position) < _fieldParameters.MapConfig.DistanceStars)
                    return false;
            }

            return true;
        }

        private void ConnectStarsGraph()
        {
            int n = _stars.Count;
            if (n == 0) return;

            List<Edge> mstEdges = BuildMst();
            List<Edge> allPossibleEdges = new List<Edge>();

            for (int i = 0; i < _stars.Count; i++)
            {
                for (int j = i + 1; j < _stars.Count; j++)
                {
                    var edge = new Edge
                    {
                        From = _stars[i],
                        To = _stars[j],
                        Distance = Vector2.Distance(_stars[i].transform.position, _stars[j].transform.position)
                    };

                    if (mstEdges.Exists(e =>
                            (e.From == edge.From && e.To == edge.To) || (e.From == edge.To && e.To == edge.From)))
                        continue;

                    allPossibleEdges.Add(edge);
                }
            }

            int extraEdgesNeeded = _fieldParameters.MapConfig.CountEdges(_fieldParameters.EdgeAmountType) - mstEdges.Count;
            List<Edge> extraEdges = new List<Edge>();

            allPossibleEdges.Sort((a, b) => a.Distance.CompareTo(b.Distance));

            foreach (Edge edge in allPossibleEdges)
            {
                if (extraEdgesNeeded <= 0)
                    break;
                extraEdges.Add(edge);
                extraEdgesNeeded--;
            }

            List<Edge> finalEdges = new List<Edge>();
            finalEdges.AddRange(mstEdges);
            finalEdges.AddRange(extraEdges);

            foreach (Edge edge in finalEdges)
            {
                if (!edge.From.ConnectedStars.Contains(edge.To))
                    edge.From.ConnectedStars.Add(edge.To);
                if (!edge.To.ConnectedStars.Contains(edge.From))
                    edge.To.ConnectedStars.Add(edge.From);

                GameObject lineObj = CreateDashedLine(edge.From.transform.position, edge.To.transform.position);
                StarConnection connection = new StarConnection(edge.From, edge.To, lineObj);
                _starConnections.Add(connection);
                edge.From.AddConnection(connection);
                edge.To.AddConnection(connection);
            }
        }

        private List<Edge> BuildMst()
        {
            List<Edge> mstEdges = new List<Edge>();
            List<Star> connected = new List<Star>();
            List<Star> notConnected = new List<Star>(_stars);

            connected.Add(notConnected[0]);
            notConnected.RemoveAt(0);

            while (notConnected.Count > 0)
            {
                Edge bestEdge = new Edge { From = null, To = null, Distance = float.MaxValue };

                foreach (var c in connected)
                {
                    foreach (var n in notConnected)
                    {
                        float dist = Vector2.Distance(c.transform.position, n.transform.position);
                        if (dist < bestEdge.Distance)
                        {
                            bestEdge.Distance = dist;
                            bestEdge.From = c;
                            bestEdge.To = n;
                        }
                    }
                }

                if (bestEdge.From != null && bestEdge.To != null)
                {
                    mstEdges.Add(bestEdge);
                    connected.Add(bestEdge.To);
                    notConnected.Remove(bestEdge.To);
                }
                else
                {
                    break;
                }
            }

            return mstEdges;
        }

        // TODO: вынести метод в отображение
        private GameObject CreateDashedLine(Vector3 start, Vector3 end)
        {
            GameObject lineObj = new GameObject()
            {
                transform =
                {
                    parent = transform
                }
            };
            LineRenderer lr = lineObj.AddComponent<LineRenderer>();
            lr.material = _dashedLineMaterial;
            lr.widthMultiplier = _lineWidth;
            lr.positionCount = 2;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);

            lr.textureMode = LineTextureMode.Tile;
            lr.material.mainTextureScale = new Vector2(1f / Vector3.Distance(start, end), 1);
            return lineObj;
        }

        private struct Edge
        {
            public Star From;
            public Star To;
            public float Distance;
        }
    }
}