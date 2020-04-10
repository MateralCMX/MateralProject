using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.PresentationModel.User
{
    public class EditUserRequestModel : AddUserRequestModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
