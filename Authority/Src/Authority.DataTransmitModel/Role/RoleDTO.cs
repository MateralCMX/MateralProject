using System;
using System.Collections.Generic;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色数据传输模型
    /// </summary>
    public class RoleDTO
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        public Guid SubSystemID { get; set; }
        /// <summary>
        /// 网页菜单权限列表
        /// </summary>
        public ICollection<RoleWebMenuTreeDTO> WebMenuTreeList { get; set; }
    }
}
