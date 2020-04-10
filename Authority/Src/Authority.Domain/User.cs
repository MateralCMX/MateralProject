using MateralProject.Domain;
using System;

namespace Authority.Domain
{
    /// <summary>
    /// 用户
    /// </summary>
    public sealed class User : BaseEntity<Guid>
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
    }
}
