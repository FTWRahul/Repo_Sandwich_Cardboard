using UnityEngine.Events;

namespace Interfaces
{
    //Allows scripts to have common functionality while interacting, also limits interactions to the ones specified.
    public interface IIntractable
    {
        void OnEnter();
        void OnExit();
        void OnClick();

        //TODO: Figure out if I need to expose these at all!
        UnityEvent OnEnterEvent { get;}
        UnityEvent OnExitEvent { get;}
        UnityEvent OnClickEvent { get;}
    }
}