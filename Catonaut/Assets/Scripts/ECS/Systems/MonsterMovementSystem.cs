using Client;
using UnityEngine;
using UnityEngine.AI;

namespace ECS.Systems
{
    public class MonsterMovementSystem : SystemBase
    {
        private readonly GameSettings _gameSettings; 
    
        public MonsterMovementSystem(GameSettings settings)
        {
            _gameSettings = settings;
        }
        
        public override void Execute()
        {
            for (int i = 0; i < World.Monsters.Count; i++)
            {
                var monsterEntity = World.Monsters.EntityAt(i);
                var monster = monsterEntity.Monster;
                var stop = false; 

                Vector3 targetPoint;

                if (monster.TargetId != 0)
                {
                    var targetTransform = World[monster.TargetId];
                    targetPoint = targetTransform.Transform.Position;
                    if (_gameSettings.AttackMonsterRadius - (targetPoint - monster.Body.transform.position).magnitude < 1f)
                        stop = true; 
                }
                else
                {
                    targetPoint = monster.Body.Points[monster.TargetPoint].position;
                }

                if ((monster.Body.transform.position - targetPoint).sqrMagnitude < 0.25f)
                {
                    monster.TargetPoint = (monster.TargetPoint + 1) % monster.Body.Points.Length; 
                }

                var path = new NavMeshPath();
                bool found = NavMesh.CalculatePath(monster.Body.transform.position, targetPoint,
                    NavMesh.AllAreas, path);

                if (found)
                {
                    var monsterPath = path.corners[1];
                    var direction = (monsterPath - monster.Body.transform.position).normalized;
                    var deltaMove = _gameSettings.MonsterSpeed * direction / TickRate;

                    if (stop)
                    {
                        deltaMove = Vector3.zero; 
                    }

                    monster.Move = !stop;
                    monster.Body.transform.position += deltaMove;
                    monster.Body.transform.rotation = Quaternion.Lerp(Quaternion.LookRotation(direction,
                            Vector3.up), monster.Body.transform.rotation, _gameSettings.PlayerRotationLerp);
                }
            }
        }
    }
}
