using _Project.Scripts.Model.StarSystem;
using UnityEngine;

namespace _Project.Scripts.Model
{
    [System.Serializable]
    public class StarConnection
    {
        public Star StarA;
        public Star StarB;
        public GameObject ConnectionLine;

        public StarConnection(Star a, Star b, GameObject line)
        {
            StarA = a;
            StarB = b;
            ConnectionLine = line;
        }
    }
}