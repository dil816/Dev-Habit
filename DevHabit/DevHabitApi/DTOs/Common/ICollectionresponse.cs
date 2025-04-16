namespace DevHabitApi.DTOs.Common;

public interface ICollectionresponse<T>
{
    List<T> Items { get; init; }
}
