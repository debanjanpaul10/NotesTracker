// *********************************************************************************
//	<copyright file="UsersController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Users Controller API Class.</summary>
// *********************************************************************************

namespace NotesTracker.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NotesTracker.Business.Contracts;
    using NotesTracker.Shared.Constants;
    using NotesTracker.Shared.DTO;

    /// <summary>
    /// The Users Controller API Class.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    /// <param name="usersService">The users service.</param>
    /// <param name="logger">The logger.</param>
    [ApiController]
    [Route(RouteConstants.ApiRoutePrefix)]
    public class UsersController(IUsersService usersService, ILogger<UsersController> logger) : BaseController
    {
        /// <summary>
        /// The users service.
        /// </summary>
        private readonly IUsersService _usersService = usersService;

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<UsersController> _logger = logger;

        /// <summary>
        /// Logins the current user.
        /// </summary>
        /// <param name="userLogin">The user login data dto.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.GetUser_ApiRoute)]
        public async Task<ResponseDTO> GetUserAsync(UserLoginDTO userLogin)
        {
            try
            {
                var loggedInUser = await this._usersService.GetUserAsync(userLogin);
                if (loggedInUser is not null)
                {
                    return this.PrepareSuccessResponse(loggedInUser);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesNotExistsMessageConstant);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="newUser">The user register data dto.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.AddNewUser_ApiRoute)]
        public async Task<ResponseDTO> AddNewUserAsync(UserRegisterDTO newUser)
        {
            try
            {
                var isUserRegistered = await this._usersService.AddNewUserAsync(newUser);
                if (isUserRegistered)
                {
                    return this.PrepareSuccessResponse(isUserRegistered);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserAdditionFailedException);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Deletes an existing user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The response dto.</returns>
        [HttpPost]
        [Route(RouteConstants.DeleteUser_ApiRoute)]
        public async Task<ResponseDTO> DeleteUserAsync([FromBody]int userId)
        {
            try
            {
                var isUserDeleted = await this._usersService.DeleteUserAsync(userId);
                if (isUserDeleted)
                {
                    return this.PrepareSuccessResponse(isUserDeleted);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserDoesNotExistsMessageConstant);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, ex.Message);
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
