using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.Service.Models.SubSystem
{
    public class EditSubSystemModel : AddSubSystemModel
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
