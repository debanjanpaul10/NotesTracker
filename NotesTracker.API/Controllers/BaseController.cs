// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller..</summary>
// *********************************************************************************

namespace NotesTracker.API.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;
	using static NotesTracker.Shared.Constants.ConfigurationConstants;

	/// <summary>
	/// The Base Controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	[Authorize]
	public abstract class BaseController : ControllerBase
	{
		/// <summary>
		/// The user name.
		/// </summary>
		protected string UserName = string.Empty;

		/// <summary>
		/// The http context accessor.
		/// </summary>
		private readonly IHttpContextAccessor? _httpContextAccessor;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseController"/> class.
		/// </summary>
		public BaseController(IHttpContextAccessor httpContextAccessor)
		{
			this._httpContextAccessor = httpContextAccessor;
			if (this._httpContextAccessor.HttpContext is not null && this._httpContextAccessor.HttpContext?.User is not null)
			{
				var userName = this._httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(claim => claim.Type.Equals(UserNameClaimConstant))?.Value;
				if(!string.IsNullOrEmpty(userName))
				{
					this.UserName = userName;
				}
				else
				{
					var userIdNotFoundException = new InvalidOperationException(ExceptionConstants.UserIdNotPresentExceptionConstant);
					throw userIdNotFoundException;
				}
			}
		}

		/// <summary>
		/// Prepares the success response.
		/// </summary>
		/// <param name="responseData">The response data.</param>
		/// <returns>The response DTO.</returns>
		protected ResponseDTO PrepareSuccessResponse(object responseData)
		{
			return new ResponseDTO()
			{
				IsSuccess = true,
				ResponseData = responseData,
				StatusCode = StatusCodes.Status200OK,
			};
		}

		/// <summary>
		/// Handles the bad request response.
		/// </summary>
		/// <param name="statusCode">The status code.</param>
		/// <param name="message">The message.</param>
		/// <returns>The response DTO.</returns>
		protected ResponseDTO HandleBadRequestResponse(int statusCode, string message)
		{
			return new ResponseDTO()
			{
				IsSuccess = false,
				ResponseData = message,
				StatusCode = statusCode,
			};
		}
	}
}
