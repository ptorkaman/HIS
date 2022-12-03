using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HIS.Permissions;
using Blazorise;
using Blazorise.DataGrid;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using HIS.Relationships;

namespace HIS.Blazor.Pages.Relationships
{
    public partial class Relationships
    {
        private RelationshipSearchDto RelationshipSearch { get; set; } = new RelationshipSearchDto();
        private IReadOnlyList<RelationshipDto> RelationshipList { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }

        //private bool CanCreateAuthor { get; set; }
        //private bool CanEditAuthor { get; set; }
        //private bool CanDeleteAuthor { get; set; }

        private RelationshipDto RelationshipDto { get; set; } = new RelationshipDto();
        private string FilterValue { get; set; } = string.Empty;

        //private Guid EditingAuthorId { get; set; }

        //private Modal CreateAuthorModal { get; set; }
        //private Modal EditAuthorModal { get; set; }

        private Validations CreateValidationsRef;

        //private Validations EditValidationsRef;

        public Relationships()
        {
            RelationshipDto = new RelationshipDto();

        }

        protected override async Task OnInitializedAsync()
        {
            //await SetPermissionsAsync();
            await GetRelationshipsAsync();
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

        private async Task GetRelationshipsAsync()
        {
            GetRelationshipListDto getRelationshipListDto = new GetRelationshipListDto();
            getRelationshipListDto.MaxResultCount = PageSize;
            getRelationshipListDto.SkipCount = CurrentPage * PageSize;
            getRelationshipListDto.Sorting = CurrentSorting;
            getRelationshipListDto.Filter = !string.IsNullOrEmpty(FilterValue) ? FilterValue : string.Empty;

            getRelationshipListDto.RelationshipSearch = RelationshipSearch;

            var result = await RelationshipAppService.GetListAsync(getRelationshipListDto);

            RelationshipList = result.Items;
            TotalCount = (int)result.TotalCount;
        }



        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<RelationshipDto> e)
        {
            try
            {
                var name = e.Columns.FirstOrDefault(i => i.Field == "Name" && i.SearchValue != null);

                var description = e.Columns.FirstOrDefault(i => i.Field == "Description" && i.SearchValue != null);

                var sequence = e.Columns.FirstOrDefault(i => i.Field == "Sequence" && i.SearchValue != null);

                if (description != null)
                {
                    RelationshipSearch.Description = description.SearchValue.ToString();
                }
                if (sequence != null)
                {
                    RelationshipSearch.Sequence = (int)sequence.SearchValue;
                }
                if(name != null)
                {
                    RelationshipSearch.Name = name.SearchValue.ToString();
                }

                CurrentSorting = e.Columns
                    .Where(c => c.SortDirection != SortDirection.Default)
                    .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                    .JoinAsString(",");

                CurrentPage = e.Page - 1;

                await GetRelationshipsAsync();

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

            RelationshipDto = new RelationshipDto();
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
                await RelationshipAppService.CreateAsync(RelationshipDto);
                //CreateAuthorModal.Hide();
            }
        }

        private async Task DeleteRelationshipAsync(RelationshipDto Relationship)
        {
            var confirmMessage = "Are you sure you want to delete Relationship?";
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await RelationshipAppService.DeleteAsync(Relationship.Id);
            await GetRelationshipsAsync();
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
