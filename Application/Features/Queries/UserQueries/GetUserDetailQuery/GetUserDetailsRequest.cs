﻿using Application.DTOs.Token;
using Application.DTOs.User.UserDTO;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.UserQueries.GetUserDetailQuery
{
    public class GetUserDetailsRequest:IRequest<DataResult<UserDTO>>
    {
        public string UserId { get; set; }
    }
}
