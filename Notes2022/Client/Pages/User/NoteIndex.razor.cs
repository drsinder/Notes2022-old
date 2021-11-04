﻿using Microsoft.AspNetCore.Components;
using Notes2022.Client.Pages.User.Menus;
using Notes2022.Client.Shared;
using Notes2022.Shared;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System.Net.Http.Json;
using System.Timers;

using Blazored.Modal.Services;
using Blazored.Modal;
using Notes2022.Client.Pages.User.Dialogs;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Navigations;
using EventArgs = Syncfusion.Blazor.Navigations.EventArgs;

namespace Notes2022.Client.Pages.User
{
    public partial class NoteIndex
    {
        [CascadingParameter] public IModalService Modal { get; set; }
        [Parameter] public int NotesfileId { get; set; }

        protected ListMenu MyMenu { get; set; }
        public string NavString { get; set; }

        protected SfTextBox sfTextBox { get; set; }

        protected SfGrid<NoteHeader> sfGrid1 { get; set; }
        protected GridFilterSettings FilterSettings { get; set; }
        protected GridPageSettings PageSettings { get; set; }


        protected int PageSize { get; set; }
        protected int CurPage { get; set; }

        protected bool ShowContent { get; set; }
        protected bool ShowContentR { get; set; }
        protected bool ExpandAll { get; set; }

        protected long Tcount { get; set; } = 0;

        [Inject] HttpClient Http { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] Blazored.SessionStorage.ISessionStorageService sessionStorage { get; set; }
        public NoteIndex()
        {
        }

        public NoteDisplayIndexModel Model { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            Model = await Http.GetFromJsonAsync<NoteDisplayIndexModel>("api/NoteIndex/" + NotesfileId);
            PageSize = Model.UserData.Ipref2;
            CurPage = await sessionStorage.GetItemAsync<int>("IndexPage");
        }

        protected void DisplayIt(RowSelectEventArgs<NoteHeader> args)
        {
            Navigation.NavigateTo("notedisplay/" + args.Data.Id);
        }

        public async void ActionCompleteHandler(ActionEventArgs<NoteHeader> args)
        {
            await sessionStorage.SetItemAsync<int>("IndexPage", sfGrid1.PageSettings.CurrentPage);
        }

        private async Task KeyUpHandler(KeyboardEventArgs args)
        {

            switch (NavString)
            {
                case "L":
                    await ClearNav();
                    await MyMenu.ExecMenu("ListNoteFiles");
                    return;

                case "N":
                    await ClearNav();
                    await MyMenu.ExecMenu("NewBaseNote");
                    return;

                case "X":
                    await ClearNav();
                    await MyMenu.ExecMenu("eXport");
                    return;

                case "J":
                    await ClearNav();
                    await MyMenu.ExecMenu("JsonExport");
                    return;

                case "m":
                    await ClearNav();
                    await MyMenu.ExecMenu("mailFromIndex");
                    return;

                case "P":
                    await ClearNav();
                    await MyMenu.ExecMenu("PrintFile");
                    return;

                case "Z":
                    await ClearNav();
                    Modal.Show<HelpDialog>();
                    return;

                case "H":
                    await ClearNav();
                    await MyMenu.ExecMenu("HtmlFromIndex");
                    return;

                case "h":
                    await ClearNav();
                    await MyMenu.ExecMenu("htmlFromIndex");
                    return;

                case "R":
                    await ClearNav();
                    await MyMenu.ExecMenu("ReloadIndex");
                    return;


                default:
                    break;
            }

            if (args.Key == "Enter" )
            {
                if (!string.IsNullOrEmpty(NavString))
                {

                    string stuff = NavString.Replace(";", "").Replace(" ", "");

                    // parse string for # or #.#

                    string[] parts = stuff.Split('.');
                    if (parts.Length > 2)
                    {
                        ShowMessage("Too many '.'s : " + parts.Length);
                    }
                    int noteNum;
                    if (parts.Length == 1)
                    {
                        if (!int.TryParse(parts[0], out noteNum))
                        {
                            ShowMessage("Could not parse : " + parts[0]);
                        }
                        else
                        {
                            long headerId = await Http.GetFromJsonAsync<long>("api/GetNoteHeaderId/" + NotesfileId + "/" + noteNum + "/0");
                            if (headerId != 0)
                                Navigation.NavigateTo("notedisplay/" + headerId);
                            else
                                ShowMessage("Could not find note : " + stuff);

                        }
                    }
                    else if (parts.Length == 2)
                    {
                        if (!int.TryParse(parts[0], out noteNum))
                        {
                            ShowMessage("Could not parse : " + parts[0]);
                        }
                        int noteRespOrd;
                        if (!int.TryParse(parts[1], out noteRespOrd))
                        {
                            ShowMessage("Could not parse : " + parts[1]);
                        }
                        if (noteNum != 0 && noteRespOrd != 0)
                        {
                            long headerId = await Http.GetFromJsonAsync<long>("api/GetNoteHeaderId/" + NotesfileId + "/" + noteNum + "/" + noteRespOrd);
                            if (headerId != 0)
                                Navigation.NavigateTo("notedisplay/" + headerId);
                            else
                                ShowMessage("Could not find note : " + stuff);
                        }
                    }
                    await ClearNav();
                }
            }

        }

        private async void NavInputHandler(InputEventArgs args)
        {
            NavString = args.Value;
            await Task.CompletedTask;
        }

        private async Task ClearNav()
        {
            NavString = null;

            await Task.CompletedTask;
        }

        private async void ExpandAllChange(Syncfusion.Blazor.Buttons.ChangeEventArgs<bool> args)
        {
            if (ExpandAll)
            {

                await sfGrid1.ExpandAllDetailRowAsync();
            }
            else
            {
                await sfGrid1.CollapseAllDetailRowAsync();
            }
        }

        private void ShowMessage(string message)
        {
            var parameters = new ModalParameters();
            parameters.Add("MessageInput", message);
            Modal.Show<MessageBox>("", parameters);
        }

        //private async Task OnValueChange()
        //{
        //    await sfGrid1.FilterByColumn("IsDeleted", "equal", false); //Perform filtering while check/uncheck the checkbox
        //}

        System.Timers.Timer myTimer { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!firstRender)
            {   // have to wait a bit before putting focus in textbox

                await Task.Delay(300);
                await sfTextBox.FocusAsync();
            }
        }
    }
}
