using Authority.DataTransmitModel.SubSystem;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Domain.Repositories.Views;
using Authority.Domain.Views;
using Authority.SqliteEFRepository;
using Authority.Service;
using Authority.Service.Models.SubSystem;
using AutoMapper;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    public sealed class SubSystemServiceImpl : ISubSystemService
    {
        private readonly ISubSystemRepository _subSystemRepository;
        private readonly IUserOwnedSubSystemViewRepository _userOwnedSubSystemViewRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public SubSystemServiceImpl(ISubSystemRepository subSystemRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IUserOwnedSubSystemViewRepository userOwnedSubSystemViewRepository)
        {
            _subSystemRepository = subSystemRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _userOwnedSubSystemViewRepository = userOwnedSubSystemViewRepository;
        }
        public async Task AddSubSystemAsync(AddSubSystemModel model)
        {
            SubSystem subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(m => m.Name == model.Name);
            if (subSystemFromDB != null) throw new InvalidOperationException("子系统名称已存在");
            subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(m => m.Code == model.Code);
            if (subSystemFromDB != null) throw new InvalidOperationException("子系统代码已存在");
            subSystemFromDB = model.CopyProperties<SubSystem>();
            subSystemFromDB.Index = _subSystemRepository.GetMaxIndex() + 1;
            _authorityUnitOfWork.RegisterAdd(subSystemFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditSubSystemAsync(EditSubSystemModel model)
        {
            SubSystem subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(m => m.Name == model.Name && m.ID != model.ID);
            if (subSystemFromDB != null) throw new InvalidOperationException("子系统代码已存在");
            subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(m => m.Code == model.Code && m.ID != model.ID);
            if (subSystemFromDB != null) throw new InvalidOperationException("子系统代码已存在");
            subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(model.ID);
            if (subSystemFromDB == null) throw new InvalidOperationException("子系统不存在");
            model.CopyProperties(subSystemFromDB);
            subSystemFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(subSystemFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteSubSystemAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            SubSystem subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(id);
            if (subSystemFromDB == null) throw new InvalidOperationException("子系统不存在");
            _authorityUnitOfWork.RegisterDelete(subSystemFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<SubSystemDTO> GetSubSystemInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            SubSystem subSystemFromDB = await _subSystemRepository.FirstOrDefaultAsync(id);
            if (subSystemFromDB == null) throw new InvalidOperationException("子系统不存在");
            return _mapper.Map<SubSystemDTO>(subSystemFromDB);
        }
        public async Task ExchangeSubSystemIndexAsync([Required(ErrorMessage = "更改唯一标识不能为空")]Guid exchangeID, [Required(ErrorMessage = "目标唯一标识不能为空")]Guid targetID, bool forUnder = true)
        {
            List<SubSystem> subSystems = await GetSubSystemsByIDs(exchangeID, targetID);
            subSystems = await GetSubSystemsByIndex(subSystems[0], subSystems[1]);
            ExchangeIndex(exchangeID, forUnder, subSystems);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<(List<SubSystemListDTO> result, PageModel pageModel)> GetSubSystemListAsync(QuerySubSystemFilterModel filterModel)
        {
            (List<SubSystem> subsystemFormDB, PageModel pageModel) = await _subSystemRepository.PagingAsync(filterModel, m => m.Index);
            var result = _mapper.Map<List<SubSystemListDTO>>(subsystemFormDB);
            return (result, pageModel);
        }
        public async Task<List<SubSystemListDTO>> GetUserHasSubSystemListAsync([Required(ErrorMessage = "用户唯一标识不能为空")]Guid userID)
        {
            List<UserOwnedSubSystemView> userOwnedSubSystems = await _userOwnedSubSystemViewRepository.FindAsync(m => m.UserID == userID, m => m.Index);
            var result = _mapper.Map<List<SubSystemListDTO>>(userOwnedSubSystems);
            return result;
        }
        #region 私有方法
        /// <summary>
        /// 根据ID组获取信息
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        private async Task<List<SubSystem>> GetSubSystemsByIDs(Guid id1, Guid id2, params Guid[] ids)
        {
            Expression<Func<SubSystem, bool>> expression = m => m.ID == id1 || m.ID == id2;
            expression = ids.Aggregate(expression, (current, id) => current.Or(m => m.ID == id));
            List<SubSystem> subSystems = await _subSystemRepository.FindAsync(expression);
            if (subSystems.Count != ids.Length + 2) throw new InvalidOperationException("该网页菜单权限不存在");
            return subSystems;
        }
        /// <summary>
        /// 根据位序获取同级的位序内信息
        /// </summary>
        /// <param name="subSystem1"></param>
        /// <param name="subSystem2"></param>
        /// <returns></returns>
        private async Task<List<SubSystem>> GetSubSystemsByIndex(SubSystem subSystem1, SubSystem subSystem2)
        {
            var subSystems = new List<SubSystem>
            {
                subSystem1,
                subSystem2
            };
            subSystems = subSystems.OrderBy(m => m.Index).ToList();
            SubSystem firstSubSystem = subSystems[0];
            SubSystem lastSubSystem = subSystems[1];
            subSystems.AddRange(await _subSystemRepository.FindAsync(m => m.Index > firstSubSystem.Index && m.Index < lastSubSystem.Index));
            subSystems = subSystems.OrderBy(m => m.Index).ToList();
            return subSystems;
        }
        /// <summary>
        /// 调换位序
        /// </summary>
        /// <param name="exchangeID"></param>
        /// <param name="forUnder"></param>
        /// <param name="subSystems"></param>
        private void ExchangeIndex(Guid exchangeID, bool forUnder, IReadOnlyList<SubSystem> subSystems)
        {
            var count = 0;
            int startIndex;
            int indexTemp;
            if (exchangeID == subSystems[0].ID)
            {
                startIndex = forUnder ? subSystems.Count - 1 : subSystems.Count - 2;
                indexTemp = subSystems[startIndex].Index;
                for (int i = startIndex; i > count; i--)
                {
                    subSystems[i].Index = subSystems[i - 1].Index;
                    subSystems[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(subSystems[i]);
                }
            }
            else
            {
                count = subSystems.Count - 1;
                startIndex = forUnder ? 1 : 0;
                indexTemp = subSystems[startIndex].Index;
                for (int i = startIndex; i < count; i++)
                {
                    subSystems[i].Index = subSystems[i + 1].Index;
                    subSystems[i].UpdateTime = DateTime.Now;
                    _authorityUnitOfWork.RegisterEdit(subSystems[i]);
                }
            }
            subSystems[count].Index = indexTemp;
            subSystems[count].UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(subSystems[count]);
        }
        #endregion
    }
}
