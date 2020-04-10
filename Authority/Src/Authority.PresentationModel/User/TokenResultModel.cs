namespace Authority.PresentationModel.User
{
    public class TokenResultModel
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public uint ExpiresSecond { get; set; }
    }
}
