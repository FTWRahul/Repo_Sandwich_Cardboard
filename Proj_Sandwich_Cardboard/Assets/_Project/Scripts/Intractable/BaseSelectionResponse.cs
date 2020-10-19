using Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Intractable
{
    //The base intractable class that exposes events and encapsulates common functionality
    public class BaseSelectionResponse : MonoBehaviour , ISelectionResponse, IClickResponse
    {
        public virtual void OnEnter()
        {
            Debug.Log("Entered : " + gameObject.name , this);
        }
        
        public virtual void OnExit()
        {
            Debug.Log("Exited : "  + gameObject.name, this);
        }

        public virtual void OnDown()
        {
            Debug.Log("Pressed : " + gameObject.name ,  this);
        }

        // public virtual void OnHold()
        // {
        //     Debug.Log("Holding : " + gameObject.name , this);
        // }

        public virtual void OnUp()
        {
            Debug.Log("Released : " + gameObject.name , this);
        }
    }
}