namespace Develop.Interfaces
{
    public interface IKIllable
    {
        public bool IsDied { get; }
        public void Kill();
    }
}