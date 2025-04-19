using DevHabitApi.DTOs.Auth;
using DevHabitApi.Entities;

namespace DevHabitApi.DTOs.Users;

public static class UserMappings
{
    public static User ToEntity(this RegisterUserDto dto)
    {
        return new User
        {
            Id = $"u_{Guid.CreateVersion7()}",
            Name = dto.Name,
            Email = dto.Email,
            CreatedAtUtc = DateTime.UtcNow,
        };
    }
}
