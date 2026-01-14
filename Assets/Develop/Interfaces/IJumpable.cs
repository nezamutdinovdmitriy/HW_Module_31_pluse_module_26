namespace Interfaces
{
    public interface IJumpable
    {
        public void Jump();
        public bool IsInputLocked {  get; }
    }
}