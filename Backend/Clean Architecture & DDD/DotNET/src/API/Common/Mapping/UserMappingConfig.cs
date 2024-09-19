using Application.Users.Commands.UpdateUser;
using Application.Users.Common;
using Contracts.Users;
using Domain.UserAggregate;
using Mapster;
using SharedKernel.Services;

namespace API.Common.Mapping;

public class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(Guid id, UpdateUserRequest request), UpdateUserCommand>()
            .Map(dest => dest.Id, src => src.id)
            .Map(dest => dest, src => src.request);

        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.Id, src => StronglyTypeIdProvider.ConvertToGuid(src.Id))
            .Map(dest => dest.Email, src => src.Email.Value)
            .Map(dest => dest.PhoneNumber, src => src.PhoneNumber.Value);

        config.NewConfig<UserResult, UserWithRolesResponse>()
            .Map(dest => dest.Id, src => StronglyTypeIdProvider.ConvertToGuid(src.User.Id))
            .Map(dest => dest.Email, src => src.User.Email.Value)
            .Map(dest => dest.PhoneNumber, src => src.User.PhoneNumber.Value)
            .Map(dest => dest, src => src.User);
    }
}
