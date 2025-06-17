using UnityEngine;

namespace _Project.Scripts.Model.Core.Projectiles
{
    public interface IProjectile
    {
        void Init(Team team, float damage, float speed, Vector3 position);
    }
}