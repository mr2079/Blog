namespace BuildingBlocks.Results;

public sealed record Error(string Code, string Message)
{
    public static Error None = new(string.Empty, string.Empty);
    public static Error NullValue = new("Error.NullValue", "مقدار وارد شده خالی می باشد");
}