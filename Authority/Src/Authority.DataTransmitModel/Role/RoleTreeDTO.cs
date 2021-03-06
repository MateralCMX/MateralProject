﻿using MateralProject.Core.Tree;
using System;
using System.Collections.Generic;

namespace Authority.DataTransmitModel.Role
{
    /// <summary>
    /// 角色树形数据传输模型
    /// </summary>
    public class RoleTreeDTO : ITreeModel<RoleTreeDTO, Guid>
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
        /// 子级
        /// </summary>
        public ICollection<RoleTreeDTO> Child { get; set; }
    }
}
