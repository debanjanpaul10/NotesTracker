// *********************************************************************************
//	<copyright file="BaseController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Base Controller..</summary>
// *********************************************************************************

namespace NotesTracker.API.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Base Controller.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	public abstract class BaseController : ControllerBase
	{
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
