using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Authors;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;

namespace HIS.Blazor.Pages.Authors
{
    public partial class Authors
    {
        private IReadOnlyList<AuthorDto> AuthorList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private AuthorDto NewAuthor { get; set; } = new AuthorDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public Authors()
        {
            NewAuthor = new AuthorDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetAuthorsAsync();
        }

        //private async Task SetPermissionsAsync()
        //{
        //    CanCreateAuthor = await AuthorizationService
        //        .IsGrantedAsync(BookStorePermissions.Authors.Create);

        //    CanEditAuthor = await AuthorizationService
        //        .IsGrantedAsync(BookStorePermissions.Authors.Edit);

        //    CanDeleteAuthor = await AuthorizationService
        //        .IsGrantedAsync(BookStorePermissions.Authors.Delete);
        //}

        private async Task GetAuthorsAsync()
        {
            GetAuthorListDto getAuthorListDto = new GetAuthorListDto();
            getAuthorListDto.MaxResultCount = PageSize;
            getAuthorListDto.SkipCount = CurrentPage * PageSize;
            getAuthorListDto.Sorting = CurrentSorting;
            getAuthorListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            var result = await AuthorAppService.GetListAsync(getAuthorListDto);

            AuthorList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AuthorDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetAuthorsAsync();

            await InvokeAsync(StateHasChanged);
        }


        private void OpenCreateAuthorModal()
        {
            CreateValidationsRef.ClearAll();

            NewAuthor = new AuthorDto();
            //CreateAuthorModal.Show();
        }

        private void CloseCreateAuthorModal()
        {
            //CreateAuthorModal.Hide();
        }

        //private void OpenEditAuthorModal(AuthorDto author)
        //{
        //    EditValidationsRef.ClearAll();

        //    EditingAuthorId = author.Id;
        //    EditingAuthor = ObjectMapper.Map<AuthorDto, UpdateAuthorDto>(author);
        //    EditAuthorModal.Show();
        //}

        //private async Task DeleteAuthorAsync(AuthorDto author)
        //{
        //    var confirmMessage = L["AuthorDeletionConfirmationMessage", author.Name];
        //    if (!await Message.Confirm(confirmMessage))
        //    {
        //        return;
        //    }

        //    await AuthorAppService.DeleteAsync(author.Id);
        //    await GetAuthorsAsync();
        //}

        //private void CloseEditAuthorModal()
        //{
        //    EditAuthorModal.Hide();
        //}

        private async Task CreateAuthorAsync()
        {
            if (await CreateValidationsRef.ValidateAll())
            {
                await AuthorAppService.CreateAsync(NewAuthor);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteAuthorAsync(AuthorDto author)
        {
            var confirmMessage = "Are you sure you want to delete author?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await AuthorAppService.DeleteAsync(author.Id);
            await GetAuthorsAsync();
        }

        //private async Task UpdateAuthorAsync()
        //{
        //    if (EditValidationsRef.ValidateAll())
        //    {
        //        await AuthorAppService.UpdateAsync(EditingAuthorId, EditingAuthor);
        //        await GetAuthorsAsync();
        //        EditAuthorModal.Hide();
        //    }
        //}

        private bool OnDateCustomFilter(object itemValue, object searchValue)
        {
            if (searchValue is DateTime dateFilter)
            {
                return dateFilter == (DateTime)itemValue;
            }

            return true;
        }
    }
}
