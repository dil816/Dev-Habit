﻿namespace DevHabitApi.DTOs.Common;

public class LinkDto
{
    public required string Href { get; init; }
    public required string Rel { get; init; }
    public required string Method { get; init; }
}
