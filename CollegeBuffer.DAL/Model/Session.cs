namespace CollegeBuffer.DAL.Model
{
    public class Session : AbstractModel
    {
        public string SessionKey { get; set; }

        public virtual User User { get; set; }
    }
}