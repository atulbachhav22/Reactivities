﻿using AutoMapper;
using Domain;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity { get; set; }

        }
        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                this._context = context;
                this._mapper = mapper;
            }

            public DataContext Context { get; }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                
               Activity activity =  await _context.Activities.FindAsync(request.Activity.Id);

               _mapper.Map(request.Activity, activity);
                await _context.SaveChangesAsync();

                return Unit.Value;

            }
        }
    }
}
