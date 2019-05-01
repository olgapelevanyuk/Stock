﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OlympicFlights.DataAccess.ApplicationUserManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationUserDb = OlympicFlights.DataAccess.ApplicationUserManagement.ApplicationUser;

namespace OlympicFlights.Service.ApplicationUsersManagement
{
    public class ApplicationUserService
    {
        private readonly UserManager<ApplicationUserDb> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public ApplicationUserService(UserManager<ApplicationUserDb> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task<ApplicationUser> CreateApplicationUserAsync(UpdateApplicationUserRequest userSignUpRequestModel)
        {
            var applicationUserToCreateMapped = _mapper.Map<ApplicationUserDb>(userSignUpRequestModel);
            
            try
            {
                var identityResultOfCreatingUser = await _userManager.CreateAsync(applicationUserToCreateMapped, userSignUpRequestModel.UserPassword);

                if (identityResultOfCreatingUser.Succeeded)
                {
                    var identityResultOfAddingRoleToCreatedUser = await _userManager.AddToRoleAsync(applicationUserToCreateMapped,userSignUpRequestModel.UserRole);

                    if (identityResultOfAddingRoleToCreatedUser.Succeeded)
                    {
                        var appUserDb = await _userManager.FindByNameAsync(userSignUpRequestModel.UserName);
                        var role = (await _userManager.GetRolesAsync(appUserDb)).First();

                        var result = _mapper.Map<ApplicationUser>(appUserDb);

                        result.ApplicationUserPassword = userSignUpRequestModel.UserPassword;
                        result.ApplicationUserRole = role;

                        return result;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public async Task<ApplicationUser> SignInUserAsync(UpdateApplicationUserRequest userSingInRequestModel)
        {
            var applicationUserDb = _mapper.Map<ApplicationUserDb>(userSingInRequestModel);

            var userCheck = await _userManager.FindByNameAsync(applicationUserDb?.UserName);

            if(userCheck is null)
            {
                return null;
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(userCheck, userSingInRequestModel.UserPassword);

            if (!passwordCheck)
            {
                throw new Exception($"Incorrect password {passwordCheck} , {userSingInRequestModel.UserPassword}");
            }

            var result = _mapper.Map<ApplicationUser>(userCheck);

            result.ApplicationUserPassword = userSingInRequestModel.UserPassword;
            result.ApplicationUserRole = (await _userManager.GetRolesAsync(userCheck)).First();

            return result;
        }
    }
}
