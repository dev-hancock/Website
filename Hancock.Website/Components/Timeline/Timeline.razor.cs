using Microsoft.AspNetCore.Components;

namespace Hancock.Website.Components.Timeline
{
    public partial class Timeline
    {
        [Parameter]
        public required RenderFragment ChildContent { get; set; }

        private List<TimelineItem> _items = new();

        internal void AddItem(TimelineItem item) => _items.Add(item);

        internal void Collapse()
        {
            foreach (var item in _items)
            {
                item.Collapse();
            }
        }
    }
}
