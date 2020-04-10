using Materal.TTA.Common;
using System;
using MateralProject.Core.Tree;

namespace Authority.Domain.Views
{
    /// <summary>
    /// 用户拥有的网页菜单权限
    /// </summary>
    [ViewEntity]
    public sealed class UserOwnedWebMenuView : IEntity<int>
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        public Guid UserID { get; set; }
        /// <summary>
        /// 子系统唯一标识
        /// </summary>
        public Guid SubSystemID { get; set; }
        /// <summary>
        /// 网页菜单唯一标识
        /// </summary>
        [TreeID]
        public Guid WebMenuID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [TreeName]
        public string Name { get; set; }
        /// <summary>
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 携带数据
        /// </summary>
        public string Data { get; set; }
        /// <summary>
        /// 位序
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// 父级唯一标识
        /// </summary>
        [TreeParentID]
        public Guid? ParentID { get; set; }
        /// <summary>
        /// 子系统代码
        /// </summary>
        public string SystemCode { get; set; }
    }
}
