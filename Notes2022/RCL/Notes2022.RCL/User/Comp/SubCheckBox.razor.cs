using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Notes2022.Shared;

namespace Notes2022.RCL.User.Comp
{
    public partial class SubCheckBox
    {
        [Parameter]
        public int fileId { get; set; }

        [Parameter]
        public bool isChecked { get; set; }

        public SCheckModel Model { get; set; }
        protected override void OnParametersSet()
        {
            Model = new SCheckModel
            {
                isChecked = isChecked,
                fileId = fileId
            };
        }

        public async Task OnClick()
        {
            isChecked = !isChecked;

            if (isChecked) // create item
            {
                await Http.PostAsJsonAsync("api/Subscription", Model);
            }
            else // delete it
            {
                await Http.DeleteAsync("api/Subscription/" + fileId);
            }

            StateHasChanged();
        }
    }
}