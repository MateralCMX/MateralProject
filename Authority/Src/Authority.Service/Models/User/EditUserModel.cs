﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Authority.Service.Modelss.User
{
    public class EditUserModel: AddUserModel
    {
        /// <summary>
        /// 用户唯一标识
        /// </summary>
        [Required(ErrorMessage = "唯一标识不能为空")]
        public Guid ID { get; set; }
    }
}
