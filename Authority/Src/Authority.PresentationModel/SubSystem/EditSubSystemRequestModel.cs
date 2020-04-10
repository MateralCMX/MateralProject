using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.SubSystem
{
    /// <summary>
    /// 修改子系统请求模型
    /// </summary>
    public class EditSubSystemRequestModel : AddSubSystemRequestModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不可以为空")]
        public Guid ID { get; set; }
    }
}
