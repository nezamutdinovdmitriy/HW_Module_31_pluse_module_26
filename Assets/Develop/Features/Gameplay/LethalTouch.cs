using Develop.Interfaces;
using UnityEngine;

namespace Develop.Features.Gameplay
{
    public class LethalTouch : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<IKillable>(out IKillable kIllable))
                kIllable.Kill();
        }
    }
}