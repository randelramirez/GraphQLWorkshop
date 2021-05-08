using System.Collections.Generic;
using GraphQL.Models;

namespace GraphQL.Sessions
{
    public record AddSessionInput(
        string Title,
        string? Abstract,
        [HotChocolate.Types.Relay.ID(nameof(Speaker))]
        IReadOnlyList<int> SpeakerIds);
}