using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.User
{
    public class AddUserRequestModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "账号不能为空"), StringLength(100, ErrorMessage = "账号长度不能超过100")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; }
    }
}
