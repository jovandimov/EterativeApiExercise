using MediatR;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using BlogApi.Application.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogApi.Application.Handlers
{
    public class GetAllBlogsQueryHandler : IRequestHandler<GetAllBlogsQuery, IEnumerable<Blog>>
    {
        private readonly IBlogRepository _blogRepository;

        public GetAllBlogsQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<IEnumerable<Blog>> Handle(GetAllBlogsQuery request, CancellationToken cancellationToken)
        {
            return await _blogRepository.GetAllAsync();
        }
    }
}