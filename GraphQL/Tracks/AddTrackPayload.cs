﻿using System.Collections.Generic;
using GraphQL.Common;
using GraphQL.Models;

namespace GraphQL.Tracks
{
    public class AddTrackPayload : TrackPayloadBase
    {
        public AddTrackPayload(Track track) 
            : base(track)
        {
        }

        public AddTrackPayload(IReadOnlyList<UserError> errors) 
            : base(errors)
        {
        }
    }
}