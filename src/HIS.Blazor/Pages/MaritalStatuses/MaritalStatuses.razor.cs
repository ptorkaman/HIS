using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.MaritalStatuses;

namespace HIS.Blazor.Pages.MaritalStatuses
{
    public partial class MaritalStatuses
    {
        private MaritalStatusSearchDto MaritalStatusSearch { get; set; } = new MaritalStatusSearchDto();
        private IReadOnlyList<MaritalStatusDto> MaritalStatusList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private MaritalStatusDto MaritalStatusDto { get; set; } = new MaritalStatusDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public MaritalStatuses()
        {
            MaritalStatusDto = new MaritalStatusDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetMaritalStatusesAsync();
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

        private async Task GetMaritalStatusesAsync()
        {
            GetMaritalStatusListDto getMaritalStatusListDto = new GetMaritalStatusListDto();
            getMaritalStatusListDto.MaxResultCount = PageSize;
            getMaritalStatusListDto.SkipCount = CurrentPage * PageSize;
            getMaritalStatusListDto.Sorting = CurrentSorting;
            getMaritalStatusListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getMaritalStatusListDto.MaritalStatusSearch = MaritalStatusSearch;

            var result = await MaritalStatusAppService.GetListAsync(getMaritalStatusListDto);

            MaritalStatusList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<MaritalStatusDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    MaritalStatusSearch.Description = description.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    MaritalStatusSearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    MaritalStatusSearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetMaritalStatusesAsync();

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

            MaritalStatusDto = new MaritalStatusDto();
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
                await MaritalStatusAppService.CreateAsync(MaritalStatusDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteMaritalStatusAsync(MaritalStatusDto MaritalStatus)
        {
            var confirmMessage = "Are you sure you want to delete MaritalStatus?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await MaritalStatusAppService.DeleteAsync(MaritalStatus.Id);
            await GetMaritalStatusesAsync();
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
