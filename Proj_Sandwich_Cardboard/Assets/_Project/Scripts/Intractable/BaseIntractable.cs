using Interfaces;
using UnityEngine;

namespace Intractable
{
    //The base intractable class that exposes events and encapsulates common functionality
    public class BaseIntractable : MonoBehaviour , IIntractable
    {
        public virtual void OnEnter()
        {
            Debug.Log("Entered :" , this);
        }

        public virtual void OnExit()
        {
            Debug.Log("Exited :" , this);
        }

        public virtual void OnClick()
        {
            Debug.Log("Clicked :" , this);
        }
    }
}