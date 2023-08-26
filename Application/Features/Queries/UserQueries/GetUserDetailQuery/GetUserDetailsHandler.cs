using Application.DTOs.User.UpdateUserDTO;
using Application.Results;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.UserQueries.GetUserDetailQuery
{
    public class GetUserDetailsHandler : IRequestHandler<GetUserDetailsRequest, DataResult<UserDTO>>
    {
        private readonly UserManager<AppUser> _userManager;

        public GetUserDetailsHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DataResult<UserDTO>> Handle(GetUserDetailsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return new ErrorDataResult<UserDTO>("Kullanıcı Bulunamadı");

            var userDTO = new UserDTO()
            {
                Email = user.Email,
                NameSurname = user.NameSurname,
                PhoneNumber = user.PhoneNumber,
            };

            return new SuccessDataResult<UserDTO>(userDTO);
        }
    }
}
