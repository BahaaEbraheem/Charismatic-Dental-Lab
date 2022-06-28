using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.Runtime.Session;
using Abp.UI;
using Charismatic.Authorization.Roles;
using Charismatic.Authorization.Users;
using Charismatic.CrudAppServiceBase;
using Charismatic.Localization.SourceFiles;
using Charismatic.Missions.Dto;
using Charismatic.Models;
using ITLand.CMMS.Libs.DevExtreme;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Charismatic.Missions
{
    public class MissionAppService : CharismaticAsyncCrudAppService<Mission, MissionDto, int, CharismaticBaseListInputDto, CreateMissionDto, EditMissionDto>, IMissionAppService

    {
        private readonly IRepository<Mission> _missionRepository;
        private readonly IRepository<MissionMember> _missionMemberRepository;
        private readonly IRepository<Case> _caseRepository;
        private readonly IRepository<CaseType> _caseTypeRepository;
        private readonly IRepository<CaseTypeDepartmentWorkFlow> _caseTypeDepartmentWorkFlowRepository;
        
        private readonly IRepository<Doctor> _doctorRepository;
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Employee> _employeeRepository;

        public MissionAppService(IRepository<Mission> Repository, UserManager userManager, IRepository<Case> caseRepository, IRepository<CaseType> caseTypeRepository, IRepository<Doctor> doctorRepository, IRepository<CaseTypeDepartmentWorkFlow> caseTypeDepartmentWorkFlowRepository, IRepository<Employee> employeeRepository
            , IRepository<MissionMember> missionMemberRepository, RoleManager roleManager) : base(Repository)
        {
            _missionRepository = Repository;
            _userManager = userManager;
            _caseRepository = caseRepository;
            _caseTypeRepository = caseTypeRepository;
            _doctorRepository = doctorRepository;
            _caseTypeDepartmentWorkFlowRepository = caseTypeDepartmentWorkFlowRepository;
            _employeeRepository = employeeRepository;
            _missionMemberRepository = missionMemberRepository;
            _roleManager = roleManager;

        }
        /// <summary>
        /// filtering list params
        /// </summary>
        /// <param name="input">search-filter</param>
        /// <returns>IQueryable of Product</returns>
        protected override IQueryable<Mission> CreateFilteredQuery(CharismaticBaseListInputDto input)
        {
            var data = base.CreateFilteredQuery(input);
            data = data.Include(pr => pr.MissionMembers);
            var employee =  _employeeRepository.FirstOrDefault(u => u.UserId == AbpSession.UserId.Value);
            if (AbpSession.UserId == 1)
            {

            }
            else
            {
                data = data.Where(t => t.MissionMembers.Any(x => x.EmployeeId == employee.Id && x.State != 0));
                //data = data.Where(t => t.MissionMembers.Where(x => x.EmployeeId == employee.Id));
            }
                
          
            
            if (input.HasFilter)
            {
                data = new DataSourceLoaderImpl<Mission>(data, input, default, true).LoadAsync().Result;
            }
            return data;
        }

        public async Task<PagedResultDto<MissionListDto>> GetAllMissionsAsync(CharismaticBaseListInputDto input)
        {
            try
            {
                var data = CreateFilteredQuery(input);
              
                var totalCount = await AsyncQueryableExecuter.CountAsync(data);
                data = ApplySorting(data, input);
                data = ApplyPaging(data, input);
                var list = await AsyncQueryableExecuter.ToListAsync(data);
                var listDto = ObjectMapper.Map<List<MissionListDto>>(list);
                foreach (var item in listDto)
                {
                    //item.StateName = Enum.GetName(typeof(Status), item.MissionStatus);
                    var user = await _userManager.FindByIdAsync(AbpSession.GetUserId().ToString());
                    var case1 = await _caseRepository.FirstOrDefaultAsync(c=>c.Id==item.CaseId);
                    var doctor = await _doctorRepository.FirstOrDefaultAsync(c=>c.Id==case1.DoctorId);
                    item.CreatorUserName = (await _userManager.GetUserByIdAsync(user.Id)).UserName;
                    item.MissionMembersDto = ObjectMapper.Map<List<MissionMemberDto>>(list.Find(x => x.Id == item.Id).MissionMembers);
                    if (AbpSession.UserId != 1)
                    {
                        var employee = _employeeRepository.FirstOrDefault(u => u.UserId == AbpSession.UserId.Value);
                        item.EmployeeStatus = item.MissionMembersDto.FirstOrDefault(x => x.EmployeeId == employee.Id).State;
                        item.CurrentEmployeeId = employee.Id;
                    }
     
                    item.DoctorName = doctor.ResponsipleName;// (await _userManager.GetUserByIdAsync(doctor.UserId)).Name;
                   
                }
                return new PagedResultDto<MissionListDto>(totalCount, listDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public override async Task<MissionDto> CreateAsync(CreateMissionDto input)
        {
         
                try
            {
                var mission = MapToEntity(input);
                var case1 = await _caseRepository.FirstOrDefaultAsync(c => c.Id == mission.CaseId);
                mission.Id = await Repository.InsertAndGetIdAsync(mission);
                var caseType = await _caseTypeRepository.FirstOrDefaultAsync(c => c.Id == case1.TypeId.Value);
                var workflow = await _caseTypeDepartmentWorkFlowRepository.GetAllListAsync(c=>c.CaseTypeId==caseType.Id);

                foreach (var item in workflow)
                {
                    var employee = await _employeeRepository.FirstOrDefaultAsync(c=>c.Id==item.DepartmentId);
                    if(employee != null)
                    {
                        await _missionMemberRepository.InsertAsync(
                          new MissionMember
                          {
                              EmployeeId = employee.Id,
                              MissionId = mission.Id,
                              IsRunning = item.order == 1 ? true : false,
                              OrderEmp = item.order.Value,
                              State = item.order == 1 ? Enums.EmployeeStatus.New : 0


                          }
                         ) ;

                    }
                   
                }

                var missionDto = MapToEntityDto(mission);
             
                    if (missionDto.CreatorUserId.HasValue)
                    {
                    missionDto.CreatorUserName = (await _userManager.GetUserByIdAsync(missionDto.CreatorUserId.Value)).Name;

                    }

                    
                    return missionDto;
            }
            catch (NullReferenceException e)
            {
                throw;

            }
        }
        public async override Task<MissionDto> UpdateAsync(EditMissionDto input)
        {

            var mission = Repository.GetAsync(input.Id).Result;
            if (mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), input.Id));
            return await base.UpdateAsync(input);

        }


        public override async Task<MissionDto> GetAsync(EntityDto<int> input)
        {
            var mission = await Repository.FirstOrDefaultAsync(input.Id);
            if (mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Mission));
            var missionDto = ObjectMapper.Map<MissionDto>(mission);
            missionDto.CreatorUserName = (await _userManager.GetUserByIdAsync(missionDto.CreatorUserId.Value)).Name;

            return missionDto;
        }
        public override async Task DeleteAsync(EntityDto<int> input)
        {
            var Mission = await Repository.FirstOrDefaultAsync(input.Id);
            if (Mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Mission));
            await Repository.DeleteAsync(Mission);
            MapToEntityDto(Mission);

        }


        public  async Task AcceptTask(EntityDto<int> input)
        {
            var mission = await Repository.FirstOrDefaultAsync(input.Id);
            if (mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Mission));
            var employee = _employeeRepository.FirstOrDefault(u => u.UserId == AbpSession.UserId.Value);
            var missionMember = (await _missionMemberRepository.GetAllListAsync(m => m.MissionId == input.Id && m.EmployeeId == employee.Id)).FirstOrDefault();
            if(missionMember!= null)
            {
                missionMember.StartDate = DateTime.Now;
                missionMember.State =Enums.EmployeeStatus.InProgress;
               // missionMember.IsRunning = true;

               await _missionMemberRepository.UpdateAsync(missionMember);

                if (missionMember.OrderEmp == 1)
                {
                    mission.State = Enums.MissionStatus.InProgress;
                    await Repository.UpdateAsync(mission);
                }
            }
            
        }
        public async Task CompleteTask(EntityDto<int> input)
        {
            var Mission = await Repository.FirstOrDefaultAsync(input.Id);
            if (Mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Mission));
            var employee = _employeeRepository.FirstOrDefault(u => u.UserId == AbpSession.UserId.Value);
            var missionMember = await _missionMemberRepository.FirstOrDefaultAsync(m => m.MissionId == input.Id && m.EmployeeId == employee.Id);
            if (missionMember != null)
            {
                missionMember.EndDate = DateTime.Now;
                missionMember.State = Enums.EmployeeStatus.Done;
                missionMember.IsRunning = false;

                await _missionMemberRepository.UpdateAsync(missionMember);

                var nextMember = await _missionMemberRepository.FirstOrDefaultAsync(m => m.MissionId == input.Id && m.OrderEmp == missionMember.OrderEmp + 1);
                if (nextMember != null)
                {
                   // nextMember.EndDate = DateTime.Now;
                    nextMember.State = Enums.EmployeeStatus.New;
                    nextMember.IsRunning = true;
                    // nextMember.IsRunning = false;

                    await _missionMemberRepository.UpdateAsync(nextMember);
                }

            }

         
        }


        public async Task RejectTask(EntityDto<int> input)
        {
            var Mission = await Repository.FirstOrDefaultAsync(input.Id);
            if (Mission == null)
                throw new UserFriendlyException(string.Format((Exceptions.ObjectWasNotFound), Tokens.Mission));
            var employee = _employeeRepository.FirstOrDefault(u => u.UserId == AbpSession.UserId.Value);
            var missionMember = await _missionMemberRepository.FirstOrDefaultAsync(m => m.MissionId == input.Id && m.EmployeeId == employee.Id);
            if (missionMember != null)
            {
               // missionMember.EndDate = DateTime.Now;
                missionMember.State = 0;
                missionMember.IsRunning = false;

                await _missionMemberRepository.UpdateAsync(missionMember);

                if (missionMember.OrderEmp - 1 != 0)
                {
                    var beforMember = await _missionMemberRepository.FirstOrDefaultAsync(m => m.MissionId == input.Id && m.OrderEmp == missionMember.OrderEmp - 1);
                    if (beforMember != null)
                    {
                        // nextMember.EndDate = DateTime.Now;
                        beforMember.State = Enums.EmployeeStatus.New;
                        beforMember.IsRunning = true;
                        // nextMember.IsRunning = false;

                        await _missionMemberRepository.UpdateAsync(beforMember);
                    }
                }
                else
                {
                    throw new UserFriendlyException(string.Format(("it is the first Mission")));
                }
              
            }

        }
    }
}
