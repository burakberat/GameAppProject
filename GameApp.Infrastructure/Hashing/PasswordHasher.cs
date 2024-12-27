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

            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);
            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, algorithmName, Hashsize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
