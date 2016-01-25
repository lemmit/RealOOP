namespace RealOOP.Example.Messages
{
    public class DrawSquareMessage : Message<int>
    {
        public DrawSquareMessage(int side) : base(side)
        {
        }
    }
}