using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.InActiveReasons;
using HIS.Enums;
using Microsoft.JSInterop;

namespace HIS.Blazor.Pages.InActiveReasons
{
    public partial class InActiveReasons
    {
        private InActiveReasonSearchDto InActiveReasonSearch { get; set; } = new InActiveReasonSearchDto();
        private IReadOnlyList<InActiveReasonDto> InActiveReasonList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private InActiveReasonDto InActiveReasonDto { get; set; } = new InActiveReasonDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public InActiveReasons()
        {
            InActiveReasonDto = new InActiveReasonDto();

        }


        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetInActiveReasonsAsync();
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

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            //if (firstRender)
            //{
                await JSRuntime.InvokeVoidAsync("InitSelect2");

            //}
        }
        private async Task GetInActiveReasonsAsync()
        {
            GetInActiveReasonListDto getInActiveReasonListDto = new GetInActiveReasonListDto();
            getInActiveReasonListDto.MaxResultCount = PageSize;
            getInActiveReasonListDto.SkipCount = CurrentPage * PageSize;
            getInActiveReasonListDto.Sorting = CurrentSorting;
            getInActiveReasonListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getInActiveReasonListDto.InActiveReasonSearch = InActiveReasonSearch;

            var result = await InActiveReasonAppService.GetListAsync(getInActiveReasonListDto);

            InActiveReasonList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<InActiveReasonDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var inActiveStatus = e.Columns.FirstOrDefault(i => i.Field == "InActiveStatus" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    InActiveReasonSearch.Description = description.SearchValue.ToString();
                }

                if (inActiveStatus != null)
                {
                    InActiveReasonSearch.InActiveStatus = (InActiveStatusEnum)inActiveStatus.SearchValue;
                }

                if (sequence != null)
                {
                    InActiveReasonSearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    InActiveReasonSearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetInActiveReasonsAsync();

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

            InActiveReasonDto = new InActiveReasonDto();
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
                await InActiveReasonAppService.CreateAsync(InActiveReasonDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteInActiveReasonAsync(InActiveReasonDto InActiveReason)
        {
            var confirmMessage = "Are you sure you want to delete InActiveReason?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await InActiveReasonAppService.DeleteAsync(InActiveReason.Id);
            await GetInActiveReasonsAsync();
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
