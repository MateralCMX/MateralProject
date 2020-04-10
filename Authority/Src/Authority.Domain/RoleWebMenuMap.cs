using MateralProject.Domain;
using System;

namespace Authority.Domain
{
    /// <summary>
    /// 角色网页菜单映射
    /// </summary>
    public sealed class RoleWebMenuMap : BaseEntity<Guid>
    {
        /// <summary>
        /// 网页菜单唯一标识
        /// </summary>
        public Guid WebMenuID { get; set; }
        /// <summary>
        /// 角色唯一标识
        /// </summary>
        public Guid RoleID { get; set; }
    }
}
