using UnityEngine.Events;

namespace Interfaces
{
    //Allows scripts to have common functionality while interacting, also limits interactions to the ones specified.
    public interface ISelectionResponse
    {
        void OnEnter();
        void OnExit();
    }

    public interface IClickResponse
    {
        void OnDown();
        void OnUp();
    }

    public interface IHoldResponse
    {
        void OnHold();
    }
}