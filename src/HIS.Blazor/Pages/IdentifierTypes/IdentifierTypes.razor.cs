using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.IdentifierTypes;

namespace HIS.Blazor.Pages.IdentifierTypes
{
    public partial class IdentifierTypes
    {
        private IdentifierTypeSearchDto IdentifierTypeSearch { get; set; } = new IdentifierTypeSearchDto();
        private IReadOnlyList<IdentifierTypeDto> IdentifierTypeList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private IdentifierTypeDto IdentifierTypeDto { get; set; } = new IdentifierTypeDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public IdentifierTypes()
        {
            IdentifierTypeDto = new IdentifierTypeDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetIdentifierTypesAsync();
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

        private async Task GetIdentifierTypesAsync()
        {
            GetIdentifierTypeListDto getIdentifierTypeListDto = new GetIdentifierTypeListDto();
            getIdentifierTypeListDto.MaxResultCount = PageSize;
            getIdentifierTypeListDto.SkipCount = CurrentPage * PageSize;
            getIdentifierTypeListDto.Sorting = CurrentSorting;
            getIdentifierTypeListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getIdentifierTypeListDto.IdentifierTypeSearch = IdentifierTypeSearch;

            var result = await IdentifierTypeAppService.GetListAsync(getIdentifierTypeListDto);

            IdentifierTypeList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<IdentifierTypeDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var regex = e.Columns.FirstOrDefault(i => i.Field == "Regex" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    IdentifierTypeSearch.Description = description.SearchValue.ToString();
                }

                if (regex != null)
                {
                    IdentifierTypeSearch.Regex = regex.SearchValue.ToString();
                }

                if (sequence != null)
                {
                    IdentifierTypeSearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    IdentifierTypeSearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetIdentifierTypesAsync();

                await InvokeAsync(StateHasChanged);
            }
            catch (Exception ex)
            {

                throw;
            }
        
        }


        private void OpenCreateAuthorModal()
        {
            CreateValidationsRef.ClearAll();

            IdentifierTypeDto = new IdentifierTypeDto();
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
                await IdentifierTypeAppService.CreateAsync(IdentifierTypeDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteIdentifierTypeAsync(IdentifierTypeDto IdentifierType)
        {
            var confirmMessage = "Are you sure you want to delete IdentifierType?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await IdentifierTypeAppService.DeleteAsync(IdentifierType.Id);
            await GetIdentifierTypesAsync();
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


    }
}
