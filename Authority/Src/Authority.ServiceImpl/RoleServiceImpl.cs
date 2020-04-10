using Authority.DataTransmitModel.Role;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Service;
using Authority.Service.Models.Role;
using Authority.SqliteEFRepository;
using AutoMapper;
using Materal.ConvertHelper;
using MateralProject.Core.Tree;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// 角色服务
    /// </summary>
    public sealed class RoleServiceImpl : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IRoleWebMenuMapRepository _roleWebMenuRepository;
        private readonly IWebMenuRepository _webMenuRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public RoleServiceImpl(IRoleRepository roleRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IRoleWebMenuMapRepository roleWebMenuRepository, IWebMenuRepository webMenuRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _roleWebMenuRepository = roleWebMenuRepository;
            _webMenuRepository = webMenuRepository;
        }
        public async Task AddRoleAsync(AddRoleModel model)
        {
            var role = model.CopyProperties<Role>();
            role.ID = Guid.NewGuid();
            AddRoleWebMenus(role, model.WebMenuIDs);
            _authorityUnitOfWork.RegisterAdd(role);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditRoleAsync(EditRoleModel model)
        {
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(model.ID);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            model.CopyProperties(roleFromDB);
            roleFromDB.UpdateTime = DateTime.Now;
            await EditRoleWebMenus(roleFromDB, model.WebMenuIDs);
            _authorityUnitOfWork.RegisterEdit(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteRoleAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            _authorityUnitOfWork.RegisterDelete(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<RoleDTO> GetRoleInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("角色不存在");
            var result = _mapper.Map<RoleDTO>(roleFromDB);
            List<WebMenu> webMenus = await _webMenuRepository.FindAsync(m => m.SubSystemID == roleFromDB.SubSystemID);
            List<RoleWebMenuMap> roleWebMenuMaps = await _roleWebMenuRepository.FindAsync(m => m.RoleID == id);
            Guid[] roleHasWebMenuID = roleWebMenuMaps.Select(m => m.WebMenuID).ToArray();
            result.WebMenuTreeList = TreeHelper.GetTreeList<RoleWebMenuTreeDTO, WebMenu, Guid>(webMenus, null,
                webMenu =>
                {
                    var temp = webMenu.CopyProperties<RoleWebMenuTreeDTO>();
                    temp.Owned = roleHasWebMenuID.Contains(webMenu.ID);
                    return temp;
                });
            return result;
        }
        public async Task<List<RoleTreeDTO>> GetRoleTreeAsync([Required(ErrorMessage = "子系统唯一标识不能为空")]Guid subSystemID)
        {
            List<Role> roles = await _roleRepository.FindAsync(m => m.SubSystemID == subSystemID);
            roles = roles.OrderBy(m => m.Name).ToList();
            return TreeHelper.GetTreeList<RoleTreeDTO, Role, Guid>(roles);
        }
        public async Task ExchangeRoleParentIDAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id, Guid? parentID)
        {
            if (parentID.HasValue && !await _roleRepository.ExistedAsync(parentID.Value))
            {
                throw new InvalidOperationException("父级唯一标识不存在");
            }
            Role roleFromDB = await _roleRepository.FirstOrDefaultAsync(id);
            if (roleFromDB == null) throw new InvalidOperationException("该角色不存在");
            roleFromDB.ParentID = parentID;
            roleFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(roleFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        #region 私有方法
        /// <summary>
        /// 添加角色网页菜单权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="webMenuIDs"></param>
        private void AddRoleWebMenus(Role role, IEnumerable<Guid> webMenuIDs)
        {
            foreach (Guid id in webMenuIDs)
            {
                _authorityUnitOfWork.RegisterAdd(new RoleWebMenuMap
                {
                    RoleID = role.ID,
                    WebMenuID = id
                });
            }
        }
        /// <summary>
        /// 编辑角色网页菜单权限
        /// </summary>
        /// <param name="role"></param>
        /// <param name="webMenuIDs"></param>
        /// <returns></returns>
        private async Task EditRoleWebMenus(Role role, Guid[] webMenuIDs)
        {
            List<RoleWebMenuMap> roleWebMenus = await _roleWebMenuRepository.FindAsync(m => m.RoleID == role.ID);
            List<Guid> roleWebMenuIDs = roleWebMenus.Select(m => m.WebMenuID).ToList();
            List<Guid> deleteIDs = roleWebMenuIDs.Except(webMenuIDs).ToList();
            List<RoleWebMenuMap> deleteModel = roleWebMenus.Where(m => deleteIDs.Contains(m.WebMenuID)).ToList();
            foreach (RoleWebMenuMap roleWebMenu in deleteModel)
            {
                _authorityUnitOfWork.RegisterDelete(roleWebMenu);
            }
            List<Guid> addIDs = webMenuIDs.Except(roleWebMenuIDs).ToList();
            AddRoleWebMenus(role, addIDs);
        }
        #endregion
    }
}
