using GraphQL.Models;

namespace GraphQL.Tracks
{
    public record RenameTrackInput([HotChocolate.Types.Relay.ID(nameof(Track))] int Id, string Name);
}
