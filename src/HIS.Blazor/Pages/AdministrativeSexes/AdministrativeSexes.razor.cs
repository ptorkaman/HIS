using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.AdministrativeSexes;

namespace HIS.Blazor.Pages.AdministrativeSexes
{
    public partial class AdministrativeSexes
    {
        private AdministrativeSexSearchDto AdministrativeSexSearch { get; set; } = new AdministrativeSexSearchDto();
        private IReadOnlyList<AdministrativeSexDto> AdministrativeSexList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private AdministrativeSexDto AdministrativeSexDto { get; set; } = new AdministrativeSexDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public AdministrativeSexes()
        {
            AdministrativeSexDto = new AdministrativeSexDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetAdministrativeSexesAsync();
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

        private async Task GetAdministrativeSexesAsync()
        {
            GetAdministrativeSexListDto getAdministrativeSexListDto = new GetAdministrativeSexListDto();
            getAdministrativeSexListDto.MaxResultCount = PageSize;
            getAdministrativeSexListDto.SkipCount = CurrentPage * PageSize;
            getAdministrativeSexListDto.Sorting = CurrentSorting;
            getAdministrativeSexListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getAdministrativeSexListDto.AdministrativeSearch = AdministrativeSexSearch;

            var result = await AdministrativeSexAppService.GetListAsync(getAdministrativeSexListDto);

            AdministrativeSexList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AdministrativeSexDto> e)
        {
            try
            {
                var longDescription = e.Columns.FirstOrDefault(i => i.Field == "LongDescription" && i.SearchValue != null);

                var shortDescription = e.Columns.FirstOrDefault(i => i.Field == "ShortDescription" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (shortDescription != null)
                {
                    AdministrativeSexSearch.ShortDescription = shortDescription.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    AdministrativeSexSearch.Sequence = (int)sequence.SearchValue;
                }
                if(longDescription != null)
                {
                    AdministrativeSexSearch.LongDescription = longDescription.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetAdministrativeSexesAsync();

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

            AdministrativeSexDto = new AdministrativeSexDto();
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
                await AdministrativeSexAppService.CreateAsync(AdministrativeSexDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteAdministrativeSexAsync(AdministrativeSexDto administrativeSex)
        {
            var confirmMessage = "Are you sure you want to delete administrativesex?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await AdministrativeSexAppService.DeleteAsync(administrativeSex.Id);
            await GetAdministrativeSexesAsync();
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
