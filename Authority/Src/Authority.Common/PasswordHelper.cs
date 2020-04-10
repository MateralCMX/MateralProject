using Materal.ConvertHelper;

namespace Authority.Common
{
    public static class PasswordHelper
    {
        /// <summary>
        /// 加密盐
        /// </summary>
        private const string PasswordSalt = "Materal";
        public static string GetEncodePassword(string password)
        {
            return (PasswordSalt + password + PasswordSalt).ToMd5_32Encode();
        }
    }
}
