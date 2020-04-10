using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.WebMenu
{
    /// <summary>
    /// 获取用户拥有的网页菜单权限树
    /// </summary>
    public class GetUserHasWebMenuTreeRequestModel
    {
        /// <summary>
        /// 子系统唯一标识
        /// </summary>
        [Required]
        public Guid SubSystemID { get; set; }
    }
}
