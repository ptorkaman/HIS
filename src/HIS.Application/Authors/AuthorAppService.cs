using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;

namespace HIS.Authors
{
    public class AuthorAppService : HISAppService, IAuthorAppService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorAppService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<AuthorDto> CreateAsync(AuthorDto input)
        {
            var author = ObjectMapper.Map<AuthorDto, Author>(input);

            var createdAuthor = await _authorRepository.InsertAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(createdAuthor);
        }

        public async Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
            {
                input.Sorting = nameof(Author.Name);
            }

            var authorQueryable = await _authorRepository.GetQueryableAsync();

            if (!string.IsNullOrEmpty(input.Filter))
                authorQueryable = authorQueryable.Where(i => i.Name.Contains(input.Filter));

            var authors = authorQueryable
                .OrderBy(input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var totalCount = input.Filter == null
                ? await _authorRepository.CountAsync()
                : await _authorRepository.CountAsync(
                    author => author.Name.Contains(input.Filter));

            return new PagedResultDto<AuthorDto>(
                totalCount,
                ObjectMapper.Map<List<Author>, List<AuthorDto>>(authors)
            );
        }

        public async Task<AuthorDto> GetAsync(Guid id)
        {
            var author = await _authorRepository.GetAsync(id);

            return ObjectMapper.Map<Author, AuthorDto>(author);
        }

        public async Task<AuthorDto> UpdateAsync(Guid id, AuthorDto model)
        {
            var author = await _authorRepository.GetAsync(id);

            author.Name = model.Name;
            author.BirthDate = model.BirthDate;
            author.ShortBio = model.ShortBio;

            var updatedAuthor = await _authorRepository.UpdateAsync(author);

            return ObjectMapper.Map<Author, AuthorDto>(updatedAuthor);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _authorRepository.DeleteAsync(id);
        }

    }
}
