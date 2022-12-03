using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.Facilities;

namespace HIS.Blazor.Pages.Facilities
{
    public partial class Facilities
    {
        private FacilitySearchDto FacilitySearch { get; set; } = new FacilitySearchDto();
        private IReadOnlyList<FacilityDto> FacilityList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private FacilityDto FacilityDto { get; set; } = new FacilityDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public Facilities()
        {
            FacilityDto = new FacilityDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetFacilitiesAsync();
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

        private async Task GetFacilitiesAsync()
        {
            GetFacilityListDto getFacilityListDto = new GetFacilityListDto();
            getFacilityListDto.MaxResultCount = PageSize;
            getFacilityListDto.SkipCount = CurrentPage * PageSize;
            getFacilityListDto.Sorting = CurrentSorting;
            getFacilityListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getFacilityListDto.FacilitySearch = FacilitySearch;

            var result = await FacilityAppService.GetListAsync(getFacilityListDto);

            FacilityList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<FacilityDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    FacilitySearch.Description = description.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    FacilitySearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    FacilitySearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetFacilitiesAsync();

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

            FacilityDto = new FacilityDto();
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
                await FacilityAppService.CreateAsync(FacilityDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteFacilityAsync(FacilityDto Facility)
        {
            var confirmMessage = "Are you sure you want to delete Facility?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await FacilityAppService.DeleteAsync(Facility.Id);
            await GetFacilitiesAsync();
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
