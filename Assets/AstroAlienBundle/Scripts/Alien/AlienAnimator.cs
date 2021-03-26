using UnityEngine;
using UnityEngine.Events;

namespace Alien {
    
    public class AlienAnimator : MonoBehaviour {
        private static readonly int TriggerDisappear = Animator.StringToHash("Disappear");
        
        [Header("Config")]
        [SerializeField] private Animator animator;
        
        [Header("Events")]
        [SerializeField] private UnityEvent onDisappeared;

        public UnityEvent OnDisappeared => onDisappeared;
        
        public void Disappear() {
            animator.SetTrigger(TriggerDisappear);
        }

        private void Disappered() {
            OnDisappeared.Invoke();
        }
    }
    
}