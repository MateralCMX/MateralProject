using MateralProject.Core.Tree;
using System;
using System.Collections.Generic;

namespace Authority.DataTransmitModel.WebMenu
{
    /// <summary>
    /// 网页菜单权限树形数据传输模型
    /// </summary>
    public class WebMenuOwnedTreeDTO : ITreeModel<WebMenuOwnedTreeDTO, Guid>
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
        /// 样式
        /// </summary>
        public string Style { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        public string ModeCode { get; set; }
        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 是否拥有
        /// </summary>
        public bool Owned { get; set; }
        /// <summary>
        /// 子级
        /// </summary>
        public ICollection<WebMenuOwnedTreeDTO> Child { get; set; }
    }
}
