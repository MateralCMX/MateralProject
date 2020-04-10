using Authority.DataTransmitModel.User;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Service;
using Authority.Service.Modelss.User;
using Authority.SqliteEFRepository;
using AutoMapper;
using Materal.ConfigCenter;
using Materal.ConvertHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Authority.Common;

namespace Authority.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        private readonly IMapper _mapper;

        public UserServiceImpl(IUserRepository userRepository, IAuthorityUnitOfWork authorityUnitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _authorityUnitOfWork = authorityUnitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserAsync(AddUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account))) throw new MateralConfigCenterException("账号已存在");
            var user = model.CopyProperties<User>();
            user.Password = PasswordHelper.GetEncodePassword(string.IsNullOrEmpty(model.Password) ? "123456" : user.Password);
            _authorityUnitOfWork.RegisterAdd(user);
            await _authorityUnitOfWork.CommitAsync();
        }

        public async Task EditUserAsync(EditUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account) && m.ID != model.ID)) throw new MateralConfigCenterException("账号已存在");
            User userFromDb = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            string oldPassword = userFromDb.Password;
            model.CopyProperties(userFromDb);
            userFromDb.Password = string.IsNullOrEmpty(model.Password) ? oldPassword : PasswordHelper.GetEncodePassword(model.Password);
            userFromDb.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(userFromDb);
            await _authorityUnitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            _authorityUnitOfWork.RegisterDelete(userFromDb);
            await _authorityUnitOfWork.CommitAsync();
        }

        public async Task<UserDTO> GetUserInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            var result = _mapper.Map<UserDTO>(userFromDb);
            return result;
        }

        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel)
        {
            (List<User> usersFromDb, PageModel pageModel) = await _userRepository.PagingAsync(filterModel);
            var result = _mapper.Map<List<UserListDTO>>(usersFromDb);
            return (result, pageModel);
        }

        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (userFromDb == null) throw new MateralConfigCenterException("用户名或密码错误");
            model.Password = PasswordHelper.GetEncodePassword(model.Password);
            if (!userFromDb.Password.Equals(model.Password)) throw new MateralConfigCenterException("用户名或密码错误");
            var result = _mapper.Map<UserDTO>(userFromDb);
            return result;
        }
    }
}
