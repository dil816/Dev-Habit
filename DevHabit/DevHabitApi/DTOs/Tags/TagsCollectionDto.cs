using DevHabitApi.DTOs.Common;

namespace DevHabit.Api.DTOs.Tags;

public sealed record TagsCollectionDto : ICollectionresponse<TagDto>
{
    public List<TagDto> Items { get; init; }
}
