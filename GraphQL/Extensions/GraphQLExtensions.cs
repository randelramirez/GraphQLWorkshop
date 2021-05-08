using GraphQL.Attendees;
using GraphQL.Common;
using GraphQL.DataLoader;
using GraphQL.Sessions;
using GraphQL.Speakers;
using GraphQL.Tracks;
using HotChocolate.Execution.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQL.Extensions
{
    public static class RequestExecutorBuilderExtensions
    {
        public static IRequestExecutorBuilder RegisterApplicationQueries(this IRequestExecutorBuilder builder)
        {
            return builder.AddQueryType(d => d.Name(Constants.QUERY))
                    .AddTypeExtension<SpeakerQueries>()
                    .AddTypeExtension<SessionQueries>()
                    .AddTypeExtension<TrackQueries>();
        }
        
        public static IRequestExecutorBuilder RegisterApplicationMutations(this IRequestExecutorBuilder builder)
        {
            return builder.AddMutationType(d => d.Name(Constants.MUTATION))
                    .AddType<AttendeeMutations>()
                    .AddTypeExtension<SessionMutations>()
                    .AddTypeExtension<SpeakerMutations>()
                    .AddTypeExtension<TrackMutations>();
        }
        
        public static IRequestExecutorBuilder RegisterApplicationSubscriptions(this IRequestExecutorBuilder builder)
        {
            return builder.AddSubscriptionType(s => s.Name(Constants.SUBSCRIPTION))
                    .AddTypeExtension<AttendeeSubscriptions>()
                    .AddTypeExtension<SessionSubscriptions>();
        }
        
        public static IRequestExecutorBuilder RegisterApplicationTypes(this IRequestExecutorBuilder builder)
        {
            return builder.AddType<AttendeeType>()
                    .AddType<SessionType>()
                    .AddType<SpeakerType>()
                    .AddType<TrackType>();
        }
        
        public static IRequestExecutorBuilder RegisterApplicationDataLoaders(this IRequestExecutorBuilder builder)
        {
          return builder.AddDataLoader<SpeakerByIdDataLoader>()
                    .AddDataLoader<SessionByIdDataLoader>();
        }
    }
}

