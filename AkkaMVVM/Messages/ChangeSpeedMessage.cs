namespace AkkaMvvm.Messages
{
    public class ChangeSpeedMessage
    {
        public int Speed { get; }

        public ChangeSpeedMessage(int speed)
        {
            Speed = speed;
        }
    }
}