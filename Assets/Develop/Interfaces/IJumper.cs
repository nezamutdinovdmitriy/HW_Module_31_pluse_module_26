namespace Interface
{
    public interface IJumper
    {
        public float JumpForce { get; }
        public bool IsGrounded();
        public bool IsCeilinged();
    }
}