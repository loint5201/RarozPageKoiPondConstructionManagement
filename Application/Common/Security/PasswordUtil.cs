namespace Application.Common.Security
{
    public static class PasswordUtil
    {
        /// <summary>
        /// Encrypt password using BCrypt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string EncryptPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// Verify password using BCrypt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
