namespace CheckersCSharp.Models
{
    public enum EPlayer
    {
        None,
        White,
        Black
    }

    public static class PlayerExtensions
    {
        public static EPlayer Opponent(this EPlayer player)
        {
            switch(player)
            {
                case EPlayer.White:
                    return EPlayer.Black;
                case EPlayer.Black:
                    return EPlayer.White;
                default:
                    return EPlayer.None;
            }
        }
    }
}
