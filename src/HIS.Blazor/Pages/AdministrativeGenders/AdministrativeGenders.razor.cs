using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.AdministrativeGenders;

namespace HIS.Blazor.Pages.AdministrativeGenders
{
    public partial class AdministrativeGenders
    {
        private AdministrativeGenderSearchDto AdministrativeSearch { get; set; } = new AdministrativeGenderSearchDto();
        private IReadOnlyList<AdministrativeGenderDto> AdministrativeGenderList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private AdministrativeGenderDto AdministrativeGenderDto { get; set; } = new AdministrativeGenderDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public AdministrativeGenders()
        {
            AdministrativeGenderDto = new AdministrativeGenderDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetAdministrativeGenderesAsync();
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

        private async Task GetAdministrativeGenderesAsync()
        {
            GetAdministrativeGenderListDto getAdministrativeGenderListDto = new GetAdministrativeGenderListDto();
            getAdministrativeGenderListDto.MaxResultCount = PageSize;
            getAdministrativeGenderListDto.SkipCount = CurrentPage * PageSize;
            getAdministrativeGenderListDto.Sorting = CurrentSorting;
            getAdministrativeGenderListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getAdministrativeGenderListDto.AdministrativeSearch = AdministrativeSearch;

            var result = await AdministrativeGenderAppService.GetListAsync(getAdministrativeGenderListDto);

            AdministrativeGenderList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<AdministrativeGenderDto> e)
        {
            try
            {
                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var comments = e.Columns.FirstOrDefault(i => i.Field == "Comments" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    AdministrativeSearch.Description = description.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    AdministrativeSearch.Sequence = (int)sequence.SearchValue;
                }
             
                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetAdministrativeGenderesAsync();

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

            AdministrativeGenderDto = new AdministrativeGenderDto();
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
                await AdministrativeGenderAppService.CreateAsync(AdministrativeGenderDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteAdministrativeGenderAsync(AdministrativeGenderDto AdministrativeGender)
        {
            var confirmMessage = "Are you sure you want to delete administrativegender?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await AdministrativeGenderAppService.DeleteAsync(AdministrativeGender.Id);
            await GetAdministrativeGenderesAsync();
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
