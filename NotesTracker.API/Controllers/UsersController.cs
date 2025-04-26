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
        /// The _logger.
        /// </summary>
        private readonly ILogger<UsersController> _logger = logger;

        /// <summary>
        /// Gets user async.
        /// </summary>
        /// <param name="userLogin">The user login.</param>
        /// <exception cref="Exception">Exception error.</exception>
        [HttpPost]
        [Route(RouteConstants.GetUser_ApiRoute)]
        public async Task<ResponseDTO> GetUserAsync(UserLoginDTO userLogin)
        {
            try
            {
                var user = await this._usersService.GetUserAsync(userLogin);
                if (user is not null && user.UserId > 0)
                {
                    return this.PrepareSuccessResponse(user);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserNotExistsException);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Adds new user async.
        /// </summary>
        /// <param name="newUser">The new user.</param>
        /// <exception cref="Exception">Exception error.</exception>
        [HttpPost]
        [Route(RouteConstants.AddNewUser_ApiRoute)]
        public async Task<ResponseDTO> AddNewUserAsync(UserRegisterDTO newUser)
        {
            try
            {
                var result = await this._usersService.AddNewUserAsync(newUser);
                if (result)
                {
                    return this.PrepareSuccessResponse(result);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserAdditionFailedException);
            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        /// <summary>
        /// Deletes user asynchronously.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <exception cref="Exception">Exception error.</exception>
        [HttpPost]
        [Route(RouteConstants.DeleteUser_ApiRoute)]
        public async Task<ResponseDTO> DeleteUserAsync([FromBody] int userId)
        {
            try
            {
                var result = await this._usersService.DeleteUserAsync(userId);
                if (result)
                {
                    return this.PrepareSuccessResponse(result);
                }

                return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.UserDeletionFailedMessageConstant);

            }
            catch (Exception ex)
            {
                return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
