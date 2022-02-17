namespace Dummyy.Models.DTOs
{
    public class User
    {
        private string _id;
        private string _name;
        private string _password;

        public string Id
        {
            get { return _id; }
           private set { _id = value; }
        }

        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public User(string password, string name, int id)
        {
            _id = new Guid().ToString();
            Name = name;
            Password = password;
        }
        public User() { }
       

    }
}
