

using BlogApi.Application.Queries;
using BlogApi.Core.Entities;
using BlogApi.Core.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogApi.Application.Handlers
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, Blog>
    {

        private readonly IBlogRepository _blogRepository;
        public GetBlogByIdQueryHandler(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<Blog> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            return await _blogRepository.GetByIdAsync(request.Id);
        }
    }
}