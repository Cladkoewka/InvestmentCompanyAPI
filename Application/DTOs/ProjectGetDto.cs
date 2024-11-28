namespace Application.DTOs;

public record ProjectGetDto(
    int Id,
    string Name,
    string Status,
    decimal Profit,
    decimal Cost,
    DateTime Deadline,
    int CustomerId,
    int EditorId
);

public record ProjectCreateDto(
    string Name,
    string Status,
    decimal Profit,
    decimal Cost,
    DateTime Deadline,
    int CustomerId,
    int EditorId
);

public record ProjectUpdateDto(
    string Name,
    string Status,
    decimal Profit,
    decimal Cost,
    DateTime Deadline,
    int CustomerId,
    int EditorId
);