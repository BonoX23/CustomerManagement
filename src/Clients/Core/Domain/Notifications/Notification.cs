namespace Domain.Notifications
{
    public class Notification
    {
        public string Error { get; set; }

        public Notification(string erro)
        {
            Error = erro;
        }
    }
}
