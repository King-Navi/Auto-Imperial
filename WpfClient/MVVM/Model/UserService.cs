namespace WpfClient.MVVM.Model
{
    public class UserService
    {
        private UserModel currentUser;

        public UserModel CurrentUser => currentUser;

        public bool IsAuthenticated => currentUser != null;

        public void SaveUser(string name, string password, string role, int id)
        {
            currentUser = new UserModel(name, password, role, id);
        }

        public void CloseSesion()
        {
            currentUser = null;
        }
    }
}
