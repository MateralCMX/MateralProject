using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.Service.Models.WebMenu
{
    /// <summary>
    /// 网页菜单权限添加模型
    /// </summary>
    public class AddWebMenuModel
    {
        /// <summary>
        /// 代码
        /// </summary>
        [Required(ErrorMessage = "代码不能为空"), StringLength(100, ErrorMessage = "代码长度不能超过100")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required(ErrorMessage = "名称不能为空"), StringLength(100, ErrorMessage = "名称长度不能超过100")]
        public string Name { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModeCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 所属子系统唯一标识
        /// </summary>
        [Required(ErrorMessage = "子系统唯一标识不能为空")]
        public Guid SubSystemID { get; set; }
    }
}
