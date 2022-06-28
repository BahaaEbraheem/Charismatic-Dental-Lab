using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Abp;
using Abp.AspNetCore.Mvc.Authorization;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Configuration;
using Abp.Configuration.Startup;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Notifications;
using Abp.Runtime.Session;
using Abp.Threading;
using Abp.Timing;
using Abp.UI;
using Abp.Web.Models;
using Abp.Zero.Configuration;
using Charismatic.Authorization;
using Charismatic.Authorization.Users;
using Charismatic.Controllers;
using Charismatic.Identity;
using Charismatic.MultiTenancy;
using Charismatic.Sessions;
using Charismatic.Web.Models.Account;
using Charismatic.Web.Views.Shared.Components.TenantChange;
using Microsoft.AspNetCore.Mvc.Rendering;
using Charismatic.Centers;
using Charismatic.Models;
using Abp.Application.Services.Dto;
using Charismatic.Centers.Dto;
using Charismatic.Doctors;
using Charismatic.Domain.Centers;
using Charismatic.Specialties;
using Charismatic.Doctors.Dto;
using Abp.Domain.Repositories;
using Charismatic.Specialties.Dto;
using Charismatic.DoctorSpecialties;
using Charismatic.DoctorSpecialties.Dto;
using Charismatic.DoctorCenters;
using Charismatic.DoctorCenters.Dto;
using Charismatic.Models.Address;

namespace Charismatic.Web.Controllers
{
    public class AccountController : CharismaticControllerBase
    {
        private readonly IRepository<Country> _countriesRepository;
        private readonly IRepository<State> _statesRepository;
        private readonly IRepository<Specialty> _specialtiesRepository;
       
        private readonly CenterManager _centerManager;
        private readonly IDoctorCentersAppService _doctorCenterAppService;
        private readonly ISpecialtyAppService _specialtyAppService;
        private readonly IDoctorSpecialtyAppService _doctorSpecialtyAppService;
        private readonly ICenterAppService _centerAppService;
        private readonly IDoctorAppService _doctorAppService;
        private readonly UserManager _userManager;
        private readonly TenantManager _tenantManager;
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly AbpLoginResultTypeHelper _abpLoginResultTypeHelper;
        private readonly LogInManager _logInManager;
        private readonly SignInManager _signInManager;
        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly ISessionAppService _sessionAppService;
        private readonly ITenantCache _tenantCache;
        private readonly INotificationPublisher _notificationPublisher;

        public AccountController(
            IRepository<State> statesRepository,
            IDoctorCentersAppService doctorCenterAppService,
            ICenterAppService centerAppService,
            IDoctorSpecialtyAppService doctorSpecialtyAppService,
            IRepository<Country> countriesRepository,
            IRepository<Specialty> specialtiesRepository,
            ISpecialtyAppService specialtyAppService,
            CenterManager centerManager,
            IDoctorAppService doctorAppService,
            UserManager userManager,
            IMultiTenancyConfig multiTenancyConfig,
            TenantManager tenantManager,
            IUnitOfWorkManager unitOfWorkManager,
            AbpLoginResultTypeHelper abpLoginResultTypeHelper,
            LogInManager logInManager,
            SignInManager signInManager,
            UserRegistrationManager userRegistrationManager,
            ISessionAppService sessionAppService,
            ITenantCache tenantCache,
            INotificationPublisher notificationPublisher)
        {
            _statesRepository = statesRepository;
            _doctorCenterAppService = doctorCenterAppService;
            _doctorSpecialtyAppService = doctorSpecialtyAppService;
            _countriesRepository = countriesRepository;
            _specialtiesRepository = specialtiesRepository;
            _specialtyAppService = specialtyAppService;
            _centerManager = centerManager;
            _doctorAppService = doctorAppService;
            _centerAppService = centerAppService;
            _userManager = userManager;
            _multiTenancyConfig = multiTenancyConfig;
            _tenantManager = tenantManager;
            _unitOfWorkManager = unitOfWorkManager;
            _abpLoginResultTypeHelper = abpLoginResultTypeHelper;
            _logInManager = logInManager;
            _signInManager = signInManager;
            _userRegistrationManager = userRegistrationManager;
            _sessionAppService = sessionAppService;
            _tenantCache = tenantCache;
            _notificationPublisher = notificationPublisher;
        }
        #region Login / Logout

        public ActionResult Login(string userNameOrEmailAddress = "", string returnUrl = "", string successMessage = "")
        {
            
            if (string.IsNullOrWhiteSpace(returnUrl))
            {
                returnUrl = GetAppHomeUrl();
            }

            return View(new LoginFormViewModel
            {
                ReturnUrl = returnUrl,
                IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled,
                IsSelfRegistrationAllowed = IsSelfRegistrationEnabled(),
                MultiTenancySide = AbpSession.MultiTenancySide
            });
        }

        [HttpPost]
        [UnitOfWork]
        public virtual async Task<JsonResult> Login(LoginViewModel loginModel, string returnUrl = "", string returnUrlHash = "")
        {
            returnUrl = NormalizeReturnUrl(returnUrl);
            if (!string.IsNullOrWhiteSpace(returnUrlHash))
            {
                returnUrl = returnUrl + returnUrlHash;
            }
            var loginResult = await GetLoginResultAsync(loginModel.UsernameOrEmailAddress, loginModel.Password, GetTenancyNameOrNull());

            await _signInManager.SignInAsync(loginResult.Identity, loginModel.RememberMe);

            await UnitOfWorkManager.Current.SaveChangesAsync();

            return Json(new AjaxResponse { TargetUrl = returnUrl });
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }

        private async Task<AbpLoginResult<Tenant, User>> GetLoginResultAsync(string usernameOrEmailAddress, string password, string tenancyName)
        {
            var loginResult = await _logInManager.LoginAsync(usernameOrEmailAddress, password, tenancyName);
            
            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:

                    return loginResult;
                case AbpLoginResultType.UserIsNotActive:
                    
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(loginResult.Result, usernameOrEmailAddress, tenancyName);
            }
        }

        #endregion
        #region Register
        public ActionResult Register()
        {
            return RegisterView(new RegisterViewModel());
        }
        private ActionResult RegisterView(RegisterViewModel model)
        {
            try
            {
                var centers = _centerAppService.GetAll().Result;
                var list_Centers = new List<Center>(ObjectMapper.Map<List<Center>>(centers));
                SelectList CenterList = new SelectList(list_Centers, "Id", "Name");
                model.CenterList = list_Centers;

                var cities= _statesRepository.GetAllList();
                SelectList CitiesList = new SelectList(cities, "Id", "Name");
                ViewBag.CitiesList = CitiesList;


                var countries = _countriesRepository.GetAllList();
                SelectList CountryList = new SelectList(countries, "Id", "Name");
                ViewBag.CountryList = CountryList;
                var specialties = _specialtiesRepository.GetAllList();
                MultiSelectList specialtiesList = new MultiSelectList(specialties, "Id", "Name");
                ViewBag.SpecialtyList = specialtiesList;
            }
            catch (NullReferenceException e)
            {
                //Code to do something with e
            }
            ViewBag.IsMultiTenancyEnabled = _multiTenancyConfig.IsEnabled;
            return View("Register", model);
        }




        private bool IsSelfRegistrationEnabled()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return false; // No registration enabled for host users!
            }
            return true;
        }
        [HttpPost]
        [UnitOfWork]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            try
            {
                ExternalLoginInfo externalLoginInfo = null;
                if (model.IsExternalLogin)
                {
                    externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
                    if (externalLoginInfo == null)
                    {
                        throw new Exception("Can not external login!");
                    }
                    model.UserName = model.EmailAddress;
                    model.Password = Authorization.Users.User.CreateRandomPassword();
                }
                else
                {
                    if (model.UserName.IsNullOrEmpty() || model.Password.IsNullOrEmpty())
                    {
                        throw new UserFriendlyException(L("FormIsNotValidMessage"));
                    }
                }
            
                var user = await _userRegistrationManager.RegisterAsync(
                    model.Name,
                    model.Surname,
                    model.EmailAddress,
                    model.UserName,
                    model.Password,
                    true // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
                );

                //  Add registration user to doctor table
                Doctor inputDoctor = new Doctor() {
                 UserId =user.Id,
                PhoneNumber=model.Doctor.PhoneNumber,
                ClinicEmail=model.Doctor.ClinicEmail,
                ClinicPhone=model.Doctor.ClinicPhone,
                ResponsipleName=model.Doctor.ResponsipleName,
                ResponsiplePhone=model.Doctor.ResponsiplePhone
                };
                var createdoctordto = ObjectMapper.Map<CreateDoctorDto>(inputDoctor);
              var doctor=await _doctorAppService.CreateAsync(createdoctordto);
                // Add Specialty & Doctor To Specialty, DoctorSpecialty Table 
                int id;
                foreach (var item in model.SpecialtyList)
                {
                    if (int.TryParse(item, out id))
                    {
                        await _doctorSpecialtyAppService.CreateAsync(
                            ObjectMapper.Map<CreateDoctorSpecialtyDto>(new DoctorSpecialty()
                        {
                            DoctorId = doctor.Id,
                            SpecialtyId = id,
                        }));
                    }
                    else
                    {
                        var specialtyDto =  await  _specialtyAppService.CreateAsync(
                        ObjectMapper.Map<CreateSpecialtyDto>(new Specialty() { Name = item }));

                     var doctorSpecialtyDto=   await _doctorSpecialtyAppService.CreateAsync(
                         ObjectMapper.Map<CreateDoctorSpecialtyDto>(new DoctorSpecialty(){
                         DoctorId = doctor.Id,
                         SpecialtyId = specialtyDto.Id,
                     }));

                    }
                }

               // Add Doctor And Center to tables
                List<CreateCenterDto> centers = new List<CreateCenterDto>(ObjectMapper.Map<List<CreateCenterDto>>(model.CenterList));
                foreach (var item in centers)
                {
                    if (item.StateId == 0)
                        item.StateId = null;
                   var center= await _centerAppService.CreateAsync(item);
                    var doctorCenterDto = await _doctorCenterAppService.CreateAsync(
                ObjectMapper.Map<CreateDoctorCenterDto>(new DoctorCenter()
                {
                    DoctorId = doctor.Id,
                    CenterId = center.Id
                })
                );}

                // Getting tenant-specific settings
                var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

                if (model.IsExternalLogin)
                {
                    Debug.Assert(externalLoginInfo != null);

                    if (string.Equals(externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email), model.EmailAddress, StringComparison.OrdinalIgnoreCase))
                    {
                        user.IsEmailConfirmed = true;
                    }

                    user.Logins = new List<UserLogin>
                    {
                        new UserLogin
                        {
                            LoginProvider = externalLoginInfo.LoginProvider,
                            ProviderKey = externalLoginInfo.ProviderKey,
                            TenantId = user.TenantId
                        }
                    };
                }

               await  _unitOfWorkManager.Current.SaveChangesAsync();

                Debug.Assert(user.TenantId != null);

                var tenant =  _tenantManager.GetById(user.TenantId.Value);

                // Directly login if possible
                if (user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin))
                {
                    AbpLoginResult<Tenant, User> loginResult;
                    if (externalLoginInfo != null)
                    {
                        loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenant.TenancyName);
                    }
                    else
                    {
                        loginResult = await GetLoginResultAsync(user.UserName, model.Password, tenant.TenancyName);
                    }
                    if (loginResult.Result == AbpLoginResultType.Success)
                    {
                        await _signInManager.SignInAsync(loginResult.Identity, false);
                        return Redirect(GetAppHomeUrl());
                    }
                    Logger.Warn("New registered user could not be login. This should not be normally. login result: " + loginResult.Result);
                }

                return View("RegisterResult", new RegisterResultViewModel
                {
                    TenancyName = tenant.TenancyName,
                    NameAndSurname = user.Name + " " + user.Surname,
                    UserName = user.UserName,
                    EmailAddress = user.EmailAddress,
                    IsEmailConfirmed = user.IsEmailConfirmed,
                    IsActive = user.IsActive,
                    IsEmailConfirmationRequiredForLogin = isEmailConfirmationRequiredForLogin
                });
            }
            catch (UserFriendlyException ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Register", model);
            }
        }

        #endregion

        #region External Login

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action(
                "ExternalLoginCallback",
                "Account",
                new
                {
                    ReturnUrl = returnUrl
                });

            return Challenge(
                // TODO: ...?
                // new Microsoft.AspNetCore.Http.Authentication.AuthenticationProperties
                // {
                //     Items = { { "LoginProvider", provider } },
                //     RedirectUri = redirectUrl
                // },
                provider
            );
        }

        [UnitOfWork]
        public virtual async Task<ActionResult> ExternalLoginCallback(string returnUrl, string remoteError = null)
        {
            returnUrl = NormalizeReturnUrl(returnUrl);

            if (remoteError != null)
            {
                Logger.Error("Remote Error in ExternalLoginCallback: " + remoteError);
                throw new UserFriendlyException(L("CouldNotCompleteLoginOperation"));
            }

            var externalLoginInfo = await _signInManager.GetExternalLoginInfoAsync();
            if (externalLoginInfo == null)
            {
                Logger.Warn("Could not get information from external login.");
                return RedirectToAction(nameof(Login));
            }

            await _signInManager.SignOutAsync();

            var tenancyName = GetTenancyNameOrNull();

            var loginResult = await _logInManager.LoginAsync(externalLoginInfo, tenancyName);

            switch (loginResult.Result)
            {
                case AbpLoginResultType.Success:
                    await _signInManager.SignInAsync(loginResult.Identity, false);
                    return Redirect(returnUrl);
                case AbpLoginResultType.UnknownExternalLogin:
                    return await RegisterForExternalLogin(externalLoginInfo);
                default:
                    throw _abpLoginResultTypeHelper.CreateExceptionForFailedLoginAttempt(
                        loginResult.Result,
                        externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email) ?? externalLoginInfo.ProviderKey,
                        tenancyName
                    );
            }
        }

        private async Task<ActionResult> RegisterForExternalLogin(ExternalLoginInfo externalLoginInfo)
        {
            var email = externalLoginInfo.Principal.FindFirstValue(ClaimTypes.Email);
            var nameinfo = ExternalLoginInfoHelper.GetNameAndSurnameFromClaims(externalLoginInfo.Principal.Claims.ToList());

            var viewModel = new RegisterViewModel
            {
                EmailAddress = email,
                Name = nameinfo.name,
                Surname = nameinfo.surname,
                IsExternalLogin = true,
                ExternalLoginAuthSchema = externalLoginInfo.LoginProvider
            };

            if (nameinfo.name != null &&
                nameinfo.surname != null &&
                email != null)
            {
                return await Register(viewModel);
            }

            return  RegisterView(viewModel);
        }

        [UnitOfWork]
        protected virtual async Task<List<Tenant>> FindPossibleTenantsOfUserAsync(UserLoginInfo login)
        {
            List<User> allUsers;
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.MayHaveTenant))
            {
                allUsers = await _userManager.FindAllAsync(login);
            }

            return allUsers
                .Where(u => u.TenantId != null)
                .Select(u => AsyncHelper.RunSync(() => _tenantManager.FindByIdAsync(u.TenantId.Value)))
                .ToList();
        }

        #endregion

        #region Helpers

        public ActionResult RedirectToAppHome()
        {
            return RedirectToAction("Index", "Home");
        }

        public string GetAppHomeUrl()
        {
            return Url.Action("Index", "About");
        }

        #endregion

        #region Change Tenant

        public async Task<ActionResult> TenantChangeModal()
        {
            var loginInfo = await _sessionAppService.GetCurrentLoginInformations();
            return View("/Views/Shared/Components/TenantChange/_ChangeModal.cshtml", new ChangeModalViewModel
            {
                TenancyName = loginInfo.Tenant?.TenancyName
            });
        }

        #endregion

        #region Common

        private string GetTenancyNameOrNull()
        {
            if (!AbpSession.TenantId.HasValue)
            {
                return null;
            }

            return _tenantCache.GetOrNull(AbpSession.TenantId.Value)?.TenancyName;
        }

        private string NormalizeReturnUrl(string returnUrl, Func<string> defaultValueBuilder = null)
        {
            if (defaultValueBuilder == null)
            {
                defaultValueBuilder = GetAppHomeUrl;
            }

            if (returnUrl.IsNullOrEmpty())
            {
                return defaultValueBuilder();
            }

            if (Url.IsLocalUrl(returnUrl))
            {
                return returnUrl;
            }

            return defaultValueBuilder();
        }

        #endregion

        #region Etc

        /// <summary>
        /// This is a demo code to demonstrate sending notification to default tenant admin and host admin uers.
        /// Don't use this code in production !!!
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [AbpMvcAuthorize]
        public async Task<ActionResult> TestNotification(string message = "")
        {
            if (message.IsNullOrEmpty())
            {
                message = "This is a test notification, created at " + Clock.Now;
            }

            var defaultTenantAdmin = new UserIdentifier(1, 2);
            var hostAdmin = new UserIdentifier(null, 1);

            await _notificationPublisher.PublishAsync(
                    "App.SimpleMessage",
                    new MessageNotificationData(message),
                    severity: NotificationSeverity.Info,
                    userIds: new[] { defaultTenantAdmin, hostAdmin }
                 );

            return Content("Sent notification: " + message);
        }

        #endregion
    }
}
