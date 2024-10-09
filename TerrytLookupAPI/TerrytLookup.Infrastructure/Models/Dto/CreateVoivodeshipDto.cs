﻿using System.Collections.Concurrent;

namespace TerrytLookup.Infrastructure.Models.Dto;

public class CreateVoivodeshipDto
{
    public required int TerrytId { get; init; }

    public required string Name { get; init; }

    public ConcurrentBag<CreateTownDto> Towns { get; init; } = [];
}