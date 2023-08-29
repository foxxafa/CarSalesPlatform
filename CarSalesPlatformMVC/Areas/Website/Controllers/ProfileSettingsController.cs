﻿using Application.Features.Commands.AppUserCommands.DeleteUserImageCommand;
using Application.Features.Commands.AppUserCommands.UpdateUserCommand;
using Application.Features.Commands.AppUserCommands.UpdateUserImageCommand;
using Application.Features.Queries.UserQueries.GetProfileImageQuery;
using Application.Features.Queries.UserQueries.GetUserDetailQuery;
using Application.Results;
using CarSalesPlatformMVC.Areas.Website.Attributes;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    [Authorize]
    public class ProfileSettingsController : Controller
    {
        readonly IMediator _mediator;

        public ProfileSettingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(GetUserDetailsRequest request)
        {
            var userId = HttpContext.Items["UserId"] as Guid?;
            if (!userId.HasValue)
                return View(new ErrorResult("Kullanıcı kimliği çözümlenemedi"));

            request.UserId = userId.Value.ToString();
            var result = await _mediator.Send(request);

            var user = new UserVM()
            {
                NameSurname = result.Data.NameSurname,
                PhoneNumber = result.Data.PhoneNumber,
                Email = result.Data.Email,
            };

            return View(user);
        }

        [HttpGet("[controller]/[action]")]
        public async Task<Result> GetImage(GetProfileImageQueryRequest request)
        {
            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
            request.UserId = userId.ToString();
            var result = await _mediator.Send(request);
            return result;
        }

        [HttpPost("[controller]/[action]")]
        public async Task<Result> SetProfileImage(UpdateUserImageCommandRequest request)
        {
            request.UserId= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _mediator.Send(request);
            return response;

        }

        [HttpDelete("[controller]/[action]")]
        public async Task<Result> DeleteProfileImage(DeleteUserImageCommandRequest request)
        {
            request.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var response = await _mediator.Send(request);
            return response;

        }

        [HttpPost("[controller]/[action]")]
        [ValidateModel]
        public async Task<IActionResult> UpdateUserDetails(UpdateUserCommandRequest updateUserCommandRequest)
        {
            updateUserCommandRequest.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            Result response = await _mediator.Send(updateUserCommandRequest);
            return Ok(response);
        }
    }
}