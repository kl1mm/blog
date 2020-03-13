namespace kli.Blog.Core.Settings
{
    public class AppSettings
    {
        public AuthenticationSettings Authentication { get; private set; } = new AuthenticationSettings();
    }

    public class AuthenticationSettings
    {
        public string Login { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
    }
}
