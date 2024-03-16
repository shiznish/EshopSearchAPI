using Application.Core.Abstractions.Common;

namespace Infrastructure.Common;
internal sealed class MachineDateTime : IDateTime
{
    /// <inheritdoc />
    public DateTime UtcNow => DateTime.UtcNow;
}