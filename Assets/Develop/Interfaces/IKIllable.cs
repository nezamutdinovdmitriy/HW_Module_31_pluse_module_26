namespace Develop.Interfaces
{
    public interface IKillable
    {
        public bool IsDied { get; }
        public void Kill();
    }
}