using Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace Intractable
{
    //The base intractable class that exposes events and encapsulates common functionality
    public class BaseIntractable : MonoBehaviour , IIntractable
    {
        [SerializeField] protected UnityEvent onEnterEvent = new UnityEvent();
        [SerializeField] protected UnityEvent onExitEvent = new UnityEvent();
        [SerializeField] protected UnityEvent onClickEvent = new UnityEvent();
        
        public UnityEvent OnEnterEvent => onEnterEvent;
        public UnityEvent OnExitEvent => onExitEvent;
        public UnityEvent OnClickEvent => onClickEvent;
        public virtual void OnEnter()
        {
            onEnterEvent?.Invoke();
        }

        public virtual void OnExit()
        {
            onExitEvent?.Invoke();
        }

        public virtual void OnClick()
        {
            onClickEvent?.Invoke();
        }
    }
}