using UnityEngine.Events;

namespace Interfaces
{
    //Allows scripts to have common functionality while interacting, also limits interactions to the ones specified.
    public interface IIntractable
    {
        void OnEnter();
        void OnExit();
        void OnClick();
        
    }
}