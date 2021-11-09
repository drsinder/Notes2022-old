using Microsoft.AspNetCore.Components;
using Notes2022.Shared;
using System.Net.Http.Json;

namespace Notes2022.Client.Pages.User.Dialogs
{
    public partial class AccessCheckBox
    {
        [Parameter]
        public AccessItem Model { get; set; }

        protected async Task OnClick()
        {
            Model.isChecked = !Model.isChecked;
            switch (Model.which)
            {
                case AccessX.ReadAccess:
                    {
                        Model.Item.ReadAccess = Model.isChecked;
                        break;
                    }

                case AccessX.Respond:
                    {
                        Model.Item.Respond = Model.isChecked;
                        break;
                    }

                case AccessX.Write:
                    {
                        Model.Item.Write = Model.isChecked;
                        break;
                    }

                case AccessX.DeleteEdit:
                    {
                        Model.Item.DeleteEdit = Model.isChecked;
                        break;
                    }

                case AccessX.SetTag:
                    {
                        Model.Item.SetTag = Model.isChecked;
                        break;
                    }

                case AccessX.ViewAccess:
                    {
                        Model.Item.ViewAccess = Model.isChecked;
                        break;
                    }

                case AccessX.EditAccess:
                    {
                        Model.Item.EditAccess = Model.isChecked;
                        break;
                    }

                default:
                    break;
            }

            await Http.PutAsJsonAsync("api/accesslist/", Model.Item);
        }
    }
}