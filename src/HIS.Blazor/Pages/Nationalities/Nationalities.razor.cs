using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.Nationalities;

namespace HIS.Blazor.Pages.Nationalities
{
    public partial class Nationalities
    {
        private NationalitySearchDto NationalitySearch { get; set; } = new NationalitySearchDto();
        private IReadOnlyList<NationalityDto> NationalityList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private NationalityDto NationalityDto { get; set; } = new NationalityDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public Nationalities()
        {
            NationalityDto = new NationalityDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetNationalitiesAsync();
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

        private async Task GetNationalitiesAsync()
        {
            GetNationalityListDto getNationalityListDto = new GetNationalityListDto();
            getNationalityListDto.MaxResultCount = PageSize;
            getNationalityListDto.SkipCount = CurrentPage * PageSize;
            getNationalityListDto.Sorting = CurrentSorting;
            getNationalityListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getNationalityListDto.NationalitySearch = NationalitySearch;

            var result = await NationalityAppService.GetListAsync(getNationalityListDto);

            NationalityList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<NationalityDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                
                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                
                if (sequence != null)
                {
                    NationalitySearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    NationalitySearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetNationalitiesAsync();

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

            NationalityDto = new NationalityDto();
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
                await NationalityAppService.CreateAsync(NationalityDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteNationalityAsync(NationalityDto Nationality)
        {
            var confirmMessage = "Are you sure you want to delete Nationality?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await NationalityAppService.DeleteAsync(Nationality.Id);
            await GetNationalitiesAsync();
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
