using System.Security.Cryptography;

namespace GameApp.Infrastructure.Hashing
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private const int Saltsize = 16;
        private const int Hashsize = 32;
        private const int Iterations = 100000;

        private static readonly HashAlgorithmName algorithmName = HashAlgorithmName.SHA512;
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(Saltsize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithmName, Hashsize);

            return $"{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
        }
    }
}
