using MateralProject.Domain;
using System;

namespace Authority.Domain
{
    /// <summary>
    /// 用户角色映射
    /// </summary>
    public class UserRoleMap : BaseEntity<Guid>
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }
    }
}
