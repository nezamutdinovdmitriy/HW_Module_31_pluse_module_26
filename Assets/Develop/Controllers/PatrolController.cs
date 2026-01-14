using Develop.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Develop.Controllers
{
    public class PatrolController : Controller
    {
        private readonly IMovable _movable;

        private float _minDistanceToTarget = 0.5f;

        private Queue<Transform> _targets;
        private Transform _currentTarget;

        public PatrolController(IMovable movable, float minDistanceToTarget, Transform[] targets)
        {
            _movable = movable;
            _minDistanceToTarget = minDistanceToTarget;

            _targets = new Queue<Transform>();

            foreach (Transform target in targets)
                _targets.Enqueue(target);

            UpdateTarget();
        }

        protected override void UpdateLogic(float deltaTime)
        {
            if (_currentTarget == null)
                return;

            float distanceX = _currentTarget.position.x - _movable.Transform.position.x;

            if (Mathf.Abs(distanceX) <= _minDistanceToTarget)
            {
                UpdateTarget();
                return;
            }

            float directionX = Mathf.Sign(distanceX);

            _movable.Move(new Vector2(directionX, 0));
        }

        private void UpdateTarget()
        {
            _currentTarget = _targets.Dequeue();
            _targets.Enqueue(_currentTarget);
        }
    }
}