using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.Countries;

namespace HIS.Blazor.Pages.Countries
{
    public partial class Countries
    {
        private CountrySearchDto CountrySearch { get; set; } = new CountrySearchDto();
        private IReadOnlyList<CountryDto> CountryList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private CountryDto CountryDto { get; set; } = new CountryDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public Countries()
        {
            CountryDto = new CountryDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetCountriesAsync();
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

        private async Task GetCountriesAsync()
        {
            GetCountryListDto getCountryListDto = new GetCountryListDto();
            getCountryListDto.MaxResultCount = PageSize;
            getCountryListDto.SkipCount = CurrentPage * PageSize;
            getCountryListDto.Sorting = CurrentSorting;
            getCountryListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getCountryListDto.CountrySearch = CountrySearch;

            var result = await CountryAppService.GetListAsync(getCountryListDto);

            CountryList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<CountryDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var twoLettersIsoCode = e.Columns.FirstOrDefault(i => i.Field == "TwoLettersIsoCode" && i.SearchValue != null);

                var threeLettersIsoCode = e.Columns.FirstOrDefault(i => i.Field == "ThreeLettersIsoCode" && i.SearchValue != null);

                var numericIsoCode = e.Columns.FirstOrDefault(i => i.Field == "NumericIsoCode" && i.SearchValue != null);


                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (name != null)
                {
                    CountrySearch.Name = name.SearchValue.ToString();
                }
                if (twoLettersIsoCode != null)
                {
                    CountrySearch.TwoLettersIsoCode = twoLettersIsoCode.SearchValue.ToString();
                }
                if (threeLettersIsoCode != null)
                {
                    CountrySearch.ThreeLettersIsoCode = threeLettersIsoCode.SearchValue.ToString();
                }
                if (numericIsoCode != null)
                {
                    CountrySearch.NumericIsoCode = numericIsoCode.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    CountrySearch.Sequence = (int)sequence.SearchValue;
                }
               

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetCountriesAsync();

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

            CountryDto = new CountryDto();
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
                await CountryAppService.CreateAsync(CountryDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteCountryAsync(CountryDto Country)
        {
            var confirmMessage = "Are you sure you want to delete Country?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await CountryAppService.DeleteAsync(Country.Id);
            await GetCountriesAsync();
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
