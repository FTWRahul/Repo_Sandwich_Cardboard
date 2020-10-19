namespace Interfaces
{
    //Allows scripts to have common functionality while interacting, also limits interactions to the ones specified.
    public interface ISelectionResponse
    {
        void OnEnter();
        void OnExit();
    }

    //Interface segregation 
    public interface IClickResponse
    {
        void OnDown();
        void OnUp();
    }
}