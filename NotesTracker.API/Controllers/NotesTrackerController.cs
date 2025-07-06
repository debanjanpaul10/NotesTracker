// *********************************************************************************
//	<copyright file="NotesTrackerController.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Notes Tracker Controller API Class.</summary>
// *********************************************************************************

namespace NotesTracker.API.Controllers
{
	using System.Globalization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using NotesTracker.Business.Contracts;
	using NotesTracker.Shared.Constants;
	using NotesTracker.Shared.DTO;

	/// <summary>
	/// The Notes Tracker Controller API Class.
	/// </summary>
	/// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
	/// <param name="notesTrackerService">The Notes Tracker Service.</param>
	/// <param name="logger">The logger.</param>
	/// <param name="httpContextAccessor">The Http Context accessor.</param>
	[ApiController]
	[Route(RouteConstants.ApiRoutePrefix)]
	public class NotesTrackerController(INotesTrackerService notesTrackerService, ILogger<NotesTrackerController> logger, IHttpContextAccessor httpContextAccessor) : BaseController(httpContextAccessor)
	{
		/// <summary>
		/// Gets the about us data asynchronous.
		/// </summary>
		/// <returns>The response dto.</returns>
		[HttpGet]
		[Route(RouteConstants.GetAboutUsData_ApiRoute)]
		[AllowAnonymous]
		public async Task<ResponseDTO> GetAboutUsDataAsync()
		{
			try
			{
				logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodStartedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, base.UserName));

				var aboutUsData = await notesTrackerService.GetAboutUsDataAsync(base.UserName);
				if (aboutUsData is not null && aboutUsData.Count > 0)
				{
					return this.PrepareSuccessResponse(aboutUsData);
				}

				return this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, ex.Message));
				return this.HandleBadRequestResponse(StatusCodes.Status500InternalServerError, ex.Message);
			}
			finally
			{
				logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodEndedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, base.UserName));
			}
		}

		/// <summary>
		/// Adds the new bug report data asynchronously.
		/// </summary>
		/// <param name="bugReportData">The bug report data.</param>
		/// <returns>The boolean for success/failure</returns>
		[HttpPost]
		[Route(RouteConstants.AddNewBugReport_ApiRoute)]
		public async Task<IActionResult> AddNewBugReportAsync(BugReportDTO bugReportData)
		{
			try
			{
				logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodStartedMessageConstant, nameof(AddNewBugReportAsync), DateTime.UtcNow, base.UserName));
				if (base.IsAuthorized())
				{
					var result = await notesTrackerService.AddNewBugReportDataAsync(bugReportData, userName: base.UserName);
					if (result)
					{
						return this.Ok(this.PrepareSuccessResponse(result));
					}

					return this.BadRequest(this.HandleBadRequestResponse(StatusCodes.Status400BadRequest, ExceptionConstants.SomethingWentWrongMessageConstant));
				}

				return this.Unauthorized();
			}
			catch (Exception ex)
			{
				logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodFailedWithMessageConstant, nameof(AddNewBugReportAsync), DateTime.UtcNow, ex.Message));
				throw;
			}
			finally
			{
				logger.LogInformation(string.Format(CultureInfo.CurrentCulture, ExceptionConstants.MethodEndedMessageConstant, nameof(AddNewBugReportAsync), DateTime.UtcNow, base.UserName));
			}

		}
	}
}