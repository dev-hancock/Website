using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Hancock.Website.Components.Timeline
{
    public partial class TimelineItem
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Date { get; set; }

        [Parameter]
        public string Company { get; set; }

        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public TimelinePosition Position { get; set; } = TimelinePosition.Left;

        [Parameter]
        public bool IsExpanded { get; set; } = false;

        [CascadingParameter(Name = nameof(Timeline))]
        public required Timeline Timeline { get; set; }

        protected override void OnInitialized()
        {
            Timeline?.AddItem(this);
        }

        internal void Expand()
        {
            IsExpanded = true;

            StateHasChanged();
        }

        internal void Collapse()
        {
            IsExpanded = false;

            StateHasChanged();
        }

        private void Toggle(bool value)
        {
            Timeline.Collapse();

            IsExpanded = value;

            StateHasChanged();
        }
    }
}
