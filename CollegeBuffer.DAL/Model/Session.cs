namespace CollegeBuffer.DAL.Model
{
    public sealed class Session : AbstractModel
    {
        public string SessionKey { get; set; }

        public User User { get; set; }
    }
}